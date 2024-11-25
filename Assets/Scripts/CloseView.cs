using UnityEngine;

namespace PokemonGame
{
    public class CloseView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(Keybind.CancelKey))
            {
                ViewManager.Instance.ShowLast();
            }
        }
    }
}
