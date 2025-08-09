using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Manages view transitions and view history stack.
    /// Handles showing, hiding, and returning to previous views.
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {    
        [SerializeField, Required]
        [Tooltip("All views managed by the ViewManager.")]
        private View[] views;

        private View currentView;

        /// <summary>
        /// Stack used to keep track of navigation history.
        /// </summary>
        private readonly Stack<View> history = new();

        /// <summary>
        /// Prevents overlapping transitions.
        /// </summary>
        private bool isTransitioning = false;

        public void Initialize()
        {
            foreach (View view in views)
            {
                view.Initialize();
                view.Hide();
            }
        }

        /// <summary>
        /// Shows the requested view and pushes the previous view to history.
        /// </summary>
        /// <typeparam name="T">Type of view to show.</typeparam>
        /// <param name="remember">Whether to remember the current view in history.</param>
        public void Show<T>(bool remember = true) where T : View
        {
            if (isTransitioning) return;

            foreach (View view in views)
            {
                if (view is T target)
                {
                    StartCoroutine(ShowWithTransition(target, remember));
                    return;
                }
            }
        }

        /// <summary>
        /// Returns to the previous view from history.
        /// </summary>
        public void GoToPreviousView()
        {
            if (isTransitioning || history.Count == 0) return;

            View previous = history.Pop();

            if (previous == currentView)
            {
                StartCoroutine(CloseCurrentView());
            }
            else
            {
                StartCoroutine(ShowPreviousViewWithTransition(previous));
            }
        }

        /// <summary>
        /// Whether any views are stored in history.
        /// </summary>
        public bool IsHistoryEmpty() => history.Count == 0;

        /// <summary>
        /// Shows a view with transition, optionally remembering the current view.
        /// </summary>
        private IEnumerator ShowWithTransition(View target, bool remember)
        {
            if (target == null || target == currentView)
                yield break;

            isTransitioning = true;
            View previous = currentView;

            if (remember)
            {
                if (previous != null)
                {
                    history.Push(previous);
                }
                else
                {
                    history.Push(target); // Add first view to history
                }                  
            }

            if (previous != null)
            {
                yield return previous.HandleTransition(target);
            }
               
            target.Show();
            currentView = target;
            isTransitioning = false;
        }

        /// <summary>
        /// Closes the current view with transition and clears the current reference.
        /// </summary>
        private IEnumerator CloseCurrentView()
        {
            isTransitioning = true;

            if (currentView != null)
            {
                yield return currentView.HandleTransition(null);
                currentView = null;
            }

            isTransitioning = false;
        }

        /// <summary>
        /// Transitions to a previously stored view.
        /// </summary>
        private IEnumerator ShowPreviousViewWithTransition(View previous)
        {
            isTransitioning = true;

            if (currentView != null)
            {
                yield return currentView.HandleTransition(previous);
            }
                
            currentView = previous;
            isTransitioning = false;
        }
    }
}
