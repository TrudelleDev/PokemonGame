using PokemonGame.GameState;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class PauseController : MonoBehaviour
    {
        private void Update()
        {
            if (ViewManager.Instance.IsHistoryEmpty())
            {
                GameStateManager.Instance.SetState(GameState.GameState.Resume);
            }
            else
            {
                GameStateManager.Instance.SetState(GameState.GameState.Pause);
            }
        }
    }
}
