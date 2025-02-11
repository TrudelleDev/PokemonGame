using PokemonGame.Characters;
using PokemonGame.GameState;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class GameMenuToggler : MonoBehaviour
    {
        [SerializeField] private GameMenuView gameMenuView;
        [SerializeField] private PlayerController playerController;

        private void Update()
        {
            if (!gameMenuView.gameObject.activeInHierarchy && ViewManager.Instance.IsHistoryEmpty())
            {
                if (playerController.IsMoving == false)
                {
                    if (Input.GetKeyDown(Keybind.StartKey))
                    {
                        ViewManager.Instance.Show<GameMenuView>();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(Keybind.StartKey))
                {
                    ViewManager.Instance.ShowLast();
                }
            }
        }
    }
}
