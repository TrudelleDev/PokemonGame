using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Closes the current view when the cancel key is pressed.
    /// </summary>
    public class CloseView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Cancel))
            {
                ViewManager.Instance.GoToPreviousView();
            }
        }
    }
}
