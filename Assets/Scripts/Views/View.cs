using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views.
    /// Handles visibility and optional one-time initialization.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// Called once before the view is shown for the first time.
        /// Override to perform setup like event binding or data prep.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Shows the view.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
