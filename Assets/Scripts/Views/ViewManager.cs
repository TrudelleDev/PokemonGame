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
    /// Manages a stacked overlay system for <see cref="View"/> instances.
    /// Each new view overlays on top of the previous one, which remains active but frozen.
    /// Both open and close transitions are supported.
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {
        [SerializeField, Required]
        [Tooltip("Extra hold time while the screen is black during transitions. 0 = no hold.")]
        private float blackScreenHoldDuration = 1f;

        [SerializeField, Required]
        [Tooltip("All views managed by this ViewManager.")]
        private View[] views;

        [SerializeField]
        [Tooltip("Print debug logs for active views and stack states.")]
        private bool enableDebugLogs = false;

        private readonly List<View> activeViews = new(); // Stack of active overlay views (bottom → top)

        public bool IsTransitioning { get; private set; }
        internal View CurrentView => activeViews.Count > 0 ? activeViews[^1] : null;
        public bool HasActiveView => activeViews.Count > 0;

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
        /// Retrieves a registered view of the specified type without showing it.
        /// </summary>
        internal T Get<T>() where T : View
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
        /// Displays the specified view as an overlay on top of all existing ones.
        /// The previous top view is frozen, and the new one becomes the active focus.
        /// </summary>
        internal T Show<T>() where T : View
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

                View previous = CurrentView;

                if (previous != null)
                {
                    previous.Freeze();
                }
                  
                StartCoroutine(ShowAsOverlay(target));
                return target;
            }

            return null;
        }

        internal void Close<T>() where T : View
        {
            if (IsTransitioning)
                return;

            // Find the instance in active views
            View target = activeViews.FirstOrDefault(v => v is T);
            if (target == null)
                return;

            StartCoroutine(CloseSpecificViewCoroutine(target));
        }

        private IEnumerator CloseSpecificViewCoroutine(View target)
        {
            IsTransitioning = true;
            UpdatePauseState();

            Transition transition = TransitionLibrary.Instance.Resolve(target.CloseTransition);

            // Run close transition
            if (transition != null)
            {
                yield return transition.FadeInCoroutine();
                target.Hide();

                if (blackScreenHoldDuration > 0f)
                    yield return new WaitForSecondsRealtime(blackScreenHoldDuration);

                yield return transition.FadeOutCoroutine();
            }
            else
            {
                target.Hide();
            }

            activeViews.Remove(target);

            // Unfreeze the new top
            if (CurrentView != null)
            {
                CurrentView.Unfreeze();
            }

            IsTransitioning = false;
            UpdatePauseState();
            DebugHistory();
        }

        private IEnumerator ShowAsOverlay(View target)
        {
            IsTransitioning = true;
            UpdatePauseState();

            Transition transition = TransitionLibrary.Instance.Resolve(target.OpenTransition);

            if (transition != null)
            {
                yield return transition.FadeInCoroutine();
                target.Show();

                if (blackScreenHoldDuration > 0f)
                {
                    yield return new WaitForSecondsRealtime(blackScreenHoldDuration);
                }
                  
                yield return transition.FadeOutCoroutine();
            }
            else
            {
                target.Show();
            }

            activeViews.Add(target);
            IsTransitioning = false;
            UpdatePauseState();
            DebugHistory();
        }

        /// <summary>
        /// Closes the top-most overlay view, running its CloseTransition, then unfreezes the one below.
        /// </summary>
        public void CloseTopView()
        {
            if (activeViews.Count == 0 || IsTransitioning)
            {
                return;
            }
               
            StartCoroutine(CloseTopViewCoroutine());
        }

        private IEnumerator CloseTopViewCoroutine()
        {
            IsTransitioning = true;
            UpdatePauseState();

            View topView = activeViews[^1];

            Transition transition = TransitionLibrary.Instance.Resolve(topView.CloseTransition);

            // Run the close transition if available
            if (transition != null)
            {
                yield return transition.FadeInCoroutine();
                topView.Hide();

                if (blackScreenHoldDuration > 0f)
                {
                    yield return new WaitForSecondsRealtime(blackScreenHoldDuration);
                }
                  
                yield return transition.FadeOutCoroutine();
            }
            else
            {
                topView.Hide();
            }

            activeViews.RemoveAt(activeViews.Count - 1);

            // Unfreeze the view below (if any)
            if (CurrentView != null)
            {
                CurrentView.Unfreeze();
            }

            
            IsTransitioning = false;
            UpdatePauseState();
            DebugHistory();
        }

        private void UpdatePauseState()
        {
            bool shouldPause = HasActiveView || IsTransitioning;
            PauseManager.SetPaused(shouldPause);
        }

        public void CloseAll(params System.Type[] types)
        {
            foreach (var type in types)
            {
                // Find the view in the active stack
                View target = activeViews.FirstOrDefault(v => v.GetType() == type);
                if (target != null)
                {
                    target.Hide();
                    target.Unfreeze();// immediately hide
                    activeViews.Remove(target);
                }
            }

            // Unfreeze the new top view if any
            if (CurrentView != null)
            {
                CurrentView.Unfreeze();
            }

            UpdatePauseState();
        }

        private void DebugHistory()
        {
            if (!enableDebugLogs)
                return;

            string stack = activeViews.Count > 0
                ? string.Join(" -> ", activeViews.Select(v => v.GetType().Name))
                : "Empty";

            Log.Info(nameof(ViewManager), $"View Stack: {stack}");
        }
    }
}
