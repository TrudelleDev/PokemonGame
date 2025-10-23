using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokemonGame.Pause;
using PokemonGame.Transitions;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Manages UI view transitions and navigation history.
    /// Supports main views (with transitions) and overlays (which freeze the underlying main view).
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {
        [SerializeField, Required]
        [Tooltip("Extra hold time while screen is black during transitions. 0 = no hold.")]
        private float blackScreenHoldDuration = 1f;

        [SerializeField, Required]
        [Tooltip("All views managed by this ViewManager.")]
        private View[] views;

        [SerializeField]
        [Tooltip("Print debug logs for active views and history.")]
        private bool enableDebugLogs = false;

        private View currentView;                     // Active main view
        private readonly Stack<View> history = new(); // Back stack of main views
        private readonly List<View> overlayViews = new(); // Overlay views stacked on top

        public bool IsTransitioning { get; private set; }

        /// <summary>
        /// Gets a value indicating whether there is at least one active view
        /// (either a main view or one or more overlays).
        /// </summary>
        public bool HasActiveView => currentView != null || overlayViews.Count > 0;

        protected override void Awake()
        {
            base.Awake();

            foreach (View view in views)
            {
                view.Preload();
                view.Hide();
            }
        }

        /// <summary>
        /// Retrieves a registered view of the given type without showing it.
        /// Returns <c>null</c> if the view is not found.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="View"/> to retrieve.</typeparam>
        public T Get<T>() where T : View
        {
            foreach (View view in views)
            {
                if (view is T target)
                {
                    return target;
                }
            }
            return null;
        }

        /// <summary>
        /// Shows the specified view type. 
        /// If it is an overlay, it is added on top of the current view and the current view is frozen. 
        /// If it is a main view, transitions are applied and the history is updated.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="View"/> to show.</typeparam>
        /// <returns>The instance of the view shown, or <c>null</c> if not found.</returns
        public T Show<T>() where T : View
        {
            if (IsTransitioning)
            {
                return null;
            }

            foreach (View view in views)
            {
                if (view is not T target)
                {
                    continue;
                }

                if (target.IsOverlay)
                {
                    if(currentView != null)
                    {
                        currentView.Freeze();
                    }
                    
                    target.Show();
                    overlayViews.Add(target);

                    UpdatePauseState();
                }
                else
                {
                    foreach (View overlayView in overlayViews)
                    {
                        overlayView.Hide();
                    }

                    overlayViews.Clear();

                    if(currentView != null)
                    {
                        currentView.Unfreeze();
                    }

                    StartCoroutine(SwitchBetweenViews(currentView, target, false));
                }

                DebugHistory();
                return target;
            }

            return null;
        }

        /// <summary>
        /// Closes the top-most active view.
        /// If the top-most view is an overlay, it will be hidden and the main view is unfrozen
        /// when the last overlay is closed. If the top-most view is a main view, the manager
        /// transitions back to the previous one in history, or closes it entirely if no history remains.
        /// </summary>
        public void CloseCurrentView()
        {
            if (IsTransitioning)
            {
                return;
            }

            if (overlayViews.Count > 0)
            {
                // Close top overlay
                int lastIndex = overlayViews.Count - 1;
                View overlayView = overlayViews[lastIndex];
                overlayViews.RemoveAt(lastIndex);
                overlayView.Hide();

                if (overlayViews.Count == 0)
                {
                    if(currentView != null)
                    {
                        currentView.Unfreeze();
                    }
                }

                UpdatePauseState();
            }
            else if (currentView != null)
            {
                IsTransitioning = true;
                UpdatePauseState();

                if (history.Count > 0)
                {
                    View previousView = history.Pop();
                    StartCoroutine(SwitchBetweenViews(currentView, previousView, true));
                }
                else
                {
                    // No previous view — close with transition to "null"
                    StartCoroutine(SwitchBetweenViews(currentView, null, true));
                }
            }

            DebugHistory();
        }

        private IEnumerator SwitchBetweenViews(View fromView, View toView, bool isBackNavigation)
        {
            IsTransitioning = true;
            UpdatePauseState();

            if (!isBackNavigation && fromView != null)
            {
                history.Push(fromView);
            }

            // Choose which transition type based on navigation direction
            TransitionType transitionType;

            if (isBackNavigation && fromView != null)
            {
                // Use the outgoing view’s transition for back navigation
                transitionType = fromView.TransitionType;
            }
            else
            {
                // Use the incoming view’s transition for forward navigation
                transitionType = toView.TransitionType;
            }

            Transition transition = TransitionLibrary.Instance.Resolve(transitionType);

            if (transition != null)
            {
                yield return RunViewTransition(transition, fromView, toView);
            }
            else
            {
                SwapViews(fromView, toView);
            }

            currentView = toView;
            IsTransitioning = false;

            UpdatePauseState();
            DebugHistory();
        }

        /// <summary>
        /// Executes a full transition sequence: 
        /// fade in, swap views, optional black screen hold, then fade out.
        /// </summary>
        /// <param name="transition">The transition effect to perform.</param>
        /// <param name="fromView">The currently active view being replaced.</param>
        /// <param name="toView">The new view being shown.</param>
        private IEnumerator RunViewTransition(Transition transition, View fromView, View toView)
        {
            yield return transition.FadeInCoroutine();
            SwapViews(fromView, toView);

            if (blackScreenHoldDuration > 0f)
            {
                yield return new WaitForSecondsRealtime(blackScreenHoldDuration);
            }

            yield return transition.FadeOutCoroutine();
        }

        /// <summary>
        /// Swaps one view for another without transition effects.
        /// </summary>
        /// <param name="fromView">The currently active view to hide. Can be null.</param>
        /// <param name="toView">The new view to show. Can be null.</param>
        private void SwapViews(View fromView, View toView)
        {
            if (fromView != null)
            {
                fromView.Hide();
            }

            if (toView != null)
            {
                toView.Show();
            }
        }

        /// <summary>
        /// Updates the pause state depending on whether any views are active or a transition is running.
        /// </summary>
        private void UpdatePauseState()
        {
            bool shouldPause = HasActiveView || IsTransitioning;
            PauseManager.SetPaused(shouldPause);
        }

        /// <summary>
        /// Prints debug information about the current view, overlays, and navigation history.
        /// </summary>
        private void DebugHistory()
        {
            if (!enableDebugLogs)
            {
                return;
            }

            string current = currentView != null ? currentView.GetType().Name : "None";
            string overlayNames = overlayViews.Count > 0 ? string.Join(", ", overlayViews.Select(v => v.GetType().Name)) : "None";
            string stack = history.Count > 0 ? string.Join(" -> ", history.Select(v => v?.GetType().Name ?? "Null")) : "Empty";

            Log.Info(nameof(ViewManager), $"Current: {current}, Overlays: [{overlayNames}] | History: {stack}");
        }
    }
}
