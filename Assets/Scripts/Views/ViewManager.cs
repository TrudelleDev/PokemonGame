using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokemonGame.Pause;
using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Manages UI view transitions and navigation history.
    /// Responsible for showing, hiding, and returning to previous views,
    /// applying transitions defined per <see cref="View"/>.
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {
        [Title("Settings")]
        [SerializeField]
        [Tooltip("Extra hold time while screen is black during transitions. 0 = no hold.")]
        private float blackScreenHoldDuration = 1f;

        [Title("Views")]
        [SerializeField, Required]
        [Tooltip("All views managed by this ViewManager.")]
        private View[] views;

        [Title("Debug")]
        [Tooltip("All views managed by this ViewManager.")]
        [SerializeField] 
        private bool enableDebugLogs = false;

        private bool isTransitioning;
        private View currentView;
        private readonly Stack<View> history = new();

        /// <summary>
        /// Returns whether the history stack is empty.
        /// </summary>
        public bool HasActiveView => history.Count > 0;

        /// <summary>
        /// Preloads all registered views and hides them at startup.
        /// </summary>
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
        /// Shows the requested view. If another view is active,
        /// it is pushed onto history before switching.
        /// </summary>
        public void Show<T>() where T : View
        {
            if (isTransitioning)
            {
                return;
            }

            foreach (View view in views)
            {
                if (view is not T target)
                {
                    continue;
                }

                if (currentView == null)
                {
                    StartCoroutine(OpenFirstView(target));
                }
                else
                {
                    StartCoroutine(SwitchBetweenViews(currentView, target, false));
                }

                break;
            }
        }

        /// <summary>
        /// Retrieves a registered view of the given type.
        /// Returns null if the view is not found.
        /// </summary>
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
        /// Opens the first view without transition.
        /// </summary>
        private IEnumerator OpenFirstView(View toView)
        {
            isTransitioning = true;
            toView.Show();
            currentView = toView;
            history.Push(currentView);
            isTransitioning = false;
            UpdatePauseState();
            DebugHistory();
            yield break;
        }


        /// <summary>
        /// Closes the current active view and removes it from history.
        /// </summary>
        public void CloseCurrentView()
        {
            if (isTransitioning || currentView == null)
                return;

            // Pop if it's in history
            if (history.Count > 0 && history.Peek() == currentView)
            {
                history.Pop();
            }

            StartCoroutine(CloseLastView(currentView));
        }

        /// <summary>
        /// Closes the last active view without transition.
        /// </summary>
        private IEnumerator CloseLastView(View fromView)
        {
            isTransitioning = true;
            fromView.Hide();
            currentView = null;
            isTransitioning = false;
            UpdatePauseState();
            DebugHistory();
            yield break;
        }

        /// <summary>
        /// Returns to the previous view. If no history remains,
        /// the current view is closed instead.
        /// </summary>
        public void GoToPreviousView()
        {
            if (isTransitioning || currentView == null)
            {
                return;
            }

            if (history.Count > 0)
            {
                View previous = history.Pop();

                if (previous != null && previous != currentView)
                {
                    StartCoroutine(SwitchBetweenViews(currentView, previous, true));
                }
                else
                {
                    StartCoroutine(CloseLastView(currentView));
                }
            }
            else
            {
                StartCoroutine(CloseLastView(currentView));
            }
        }

        /// <summary>
        /// Switches between two views, applying transition if available.
        /// </summary>
        private IEnumerator SwitchBetweenViews(View fromView, View toView, bool isBackNavigation)
        {
            isTransitioning = true;

            if (!isBackNavigation)
            {
                history.Push(fromView);
            }

            Transition transition = TransitionLibrary.Instance.Resolve(fromView.TransitionType);

            if (transition != null)
            {
                yield return RunViewTransition(transition, fromView, toView);
            }
            else
            {
                SwapViews(fromView, toView);
            }

            toView.Show();
            currentView = toView;
            UpdatePauseState();
            DebugHistory();
            isTransitioning = false;
        }

        /// <summary>
        /// Executes a fade transition between two views.
        /// </summary>
        private IEnumerator RunViewTransition(Transition transition, View fromView, View toView)
        {
            if (transition == null)
            {
                SwapViews(fromView, toView);
                yield break;
            }

            yield return transition.FadeInCoroutine();
            SwapViews(fromView, toView);

            if (blackScreenHoldDuration > 0f)
            {
                yield return new WaitForSecondsRealtime(blackScreenHoldDuration);
            }

            yield return transition.FadeOutCoroutine();
        }

        /// <summary>
        /// Utility to hide the old view and show the new one instantly.
        /// </summary>
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
        /// Updates the pause state based on whether there are active views.
        /// </summary>
        private void UpdatePauseState()
        {
            PauseManager.SetPaused(history.Count > 0);
        }

        /// <summary>
        /// Prints the current and history stack of views to the console for debugging.
        /// </summary>
        private void DebugHistory()
        {
            if (history.Count == 0)
            {
                Log.Info(nameof(ViewManager), "History is empty. No active views.");
                return;
            }

            string[] names = history.Select(v => v != null ? v.GetType().Name : "Null").ToArray();
            string stack = string.Join(" -> ", names);

            Log.Info(nameof(ViewManager), $" Current: {(currentView != null ? currentView.GetType().Name : "None")} | History: {stack}");
        }
    }
}
