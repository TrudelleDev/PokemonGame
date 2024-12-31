using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Views
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private View startingView;
        [SerializeField] private View[] views;

        public static ViewManager Instance { get; private set; }

        private View currentView;
        private readonly Stack<View> history = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

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

        public void Show<T>(bool remember = true) where T : View
        {
            foreach (View view in views)
            {
                if (view is T)
                {
                    // If this is not the first view
                    if (currentView != null)
                    {
                        if (remember)
                        {
                            history.Push(currentView);
                        }

                        currentView.Hide();
                    }

                    // If this is the first view
                    if (currentView == null)
                    {
                        history.Push(view);
                    }

                    view.Show();
                    currentView = view;
                }
            }
        }

        private void Show(View view, bool remember = true)
        {
            // If this is not the first view
            if (currentView != null)
            {
                if (remember)
                {
                    history.Push(currentView);
                }

                currentView.Hide();
            }

            // If the view has been changed
            if (currentView != view)
            {
                view.Show();
                currentView = view;
            }
        }

        public void ShowLast()
        {
            if (history.Count != 0)
            {
                Show(history.Pop(), false);
            }
        }

        public bool IsHistoryEmpty()
        {
            if (history.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
