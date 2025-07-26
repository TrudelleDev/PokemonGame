using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Closes the current view when any of the assigned keys are pressed.
    /// </summary>
    public class CloseView : MonoBehaviour
    {
        [Tooltip("Keys that trigger this view to close.")]
        [SerializeField] private KeyCode[] closeKeys = { KeyBind.Cancel };

        private void Update()
        {
            if (closeKeys == null || closeKeys.Length == 0)
                return;

            foreach (KeyCode key in closeKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Closes the current view by returning to the previous view via the ViewManager.
        /// </summary>
        public void Close()
        {
            ViewManager.Instance.GoToPreviousView();
        }
    }
}
