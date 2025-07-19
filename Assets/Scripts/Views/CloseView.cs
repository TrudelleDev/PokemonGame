using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Closes the current view when any assigned key is pressed.
    /// Can also be triggered manually.
    /// </summary>
    public class CloseView : MonoBehaviour
    {
        [Tooltip("List of keys that will trigger the view to close.")]
        [SerializeField] private KeyCode[] closeKeys;

        private void Update()
        {
            for (int i = 0; i < closeKeys.Length; i++)
            {
                if (Input.GetKeyDown(closeKeys[i]))
                {
                    Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Closes the current view using the ViewManager.
        /// </summary>
        public void Close()
        {
            ViewManager.Instance.GoToPreviousView();
        }
    }
}
