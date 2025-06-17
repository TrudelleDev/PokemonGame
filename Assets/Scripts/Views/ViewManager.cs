using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Manages the display and navigation of UI views in the game.
    /// Supports view transitions, view history, and a starting view.
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {
        [Tooltip("The view that should be shown when the scene loads.")]
        [SerializeField] private View startingView;

        [Tooltip("All views available in the scene that the manager can show.")]
        [SerializeField] private View[] views;

        private View currentView;
        private readonly Stack<View> history = new();

        protected override void Awake()
        {
            base.Awake();

            foreach (View view in views)
            {
                view.Initialize();
                view.Hide();
            }

            if (startingView != null)
            {
                Show(startingView);
            }
        }

        /// <summary>
        /// Shows a view of the specified type.
        /// Optionally remembers the current view in history.
        /// </summary>
        /// <typeparam name="T">The type of view to show.</typeparam>
        /// <param name="remember">If true, saves the current view to history.</param>
        public void Show<T>(bool remember = true) where T : View
        {
            foreach (View view in views)
            {
                if (view is T)
                {          
                    PushCurrentView(remember);

                    // If there is no current view
                    if (currentView == null)
                    {
                        history.Push(view);
                    }

                    view.Show();
                    currentView = view;
                }
            }
        }

        private void PushCurrentView(bool remember)
        {
            if (currentView != null)
            {
                if (remember)
                {
                    history.Push(currentView);
                }

                currentView.Hide();
            }
        }

        private void Show(View view, bool remember = true)
        {
            PushCurrentView(remember);

            // If the view has been changed
            if (currentView != view)
            {
                view.Show();
                currentView = view;
            }
        }

        /// <summary>
        /// Shows the previous view from the history stack.
        /// </summary>
        public void GoToPreviousView()
        {
            if (history.Count != 0)
            {
                Show(history.Pop(), false);
            }
        }

        /// <summary>
        /// Returns true if there are no views in the history.
        /// </summary>
        public bool IsHistoryEmpty() => history.Count == 0;
    }
}
