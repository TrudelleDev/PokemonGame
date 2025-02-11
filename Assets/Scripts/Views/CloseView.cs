using PokemonGame.GameState;
using UnityEngine;

namespace PokemonGame.Views
{
    public class CloseView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(Keybind.CancelKey))
            {
                ViewManager.Instance.ShowLast();
                GameStateManager.Instance.SetState(GameState.GameState.Resume);
            }
        }
    }
}
