using UnityEngine;

namespace PokemonGame.Views
{
    public class CloseView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Cancel))
            {
                // Close active view.
                ViewManager.Instance.GoToPreviousView();
            }
        }
    }
}
