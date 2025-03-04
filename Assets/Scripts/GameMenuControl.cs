using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class GameMenuControl : MonoBehaviour
    {
        [SerializeField] private GameMenuView gameMenuView;
        [SerializeField] private Characters.CharacterController playerController;

        private void Update()
        {
            // Only open the game menu if the player is not moving.
            if (playerController.IsMoving)
                return;

            if (Input.GetKeyDown(KeyBind.Start))
            {
                // No view are curently open.
                if (ViewManager.Instance.IsHistoryEmpty())
                {
                    ViewManager.Instance.Show<GameMenuView>();
                }

                // Only the game view is curently open.
                else if (gameMenuView.gameObject.activeInHierarchy)
                {
                    ViewManager.Instance.ShowLast();
                }
            }
        }
    }
}
