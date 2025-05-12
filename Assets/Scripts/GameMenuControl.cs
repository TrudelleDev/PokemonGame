using PokemonGame.Characters;
using PokemonGame.Characters.States;
using PokemonGame.Dialogues;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class GameMenuControl : MonoBehaviour
    {
        [SerializeField] private GameMenuView gameMenuView;
        [SerializeField] private CharacterStateController playerMovement;

        private void Update()
        {
            if (playerMovement.TileMover.IsMoving)
                return;

            if (DialogueBoxController.Instance.IsDialogueBoxOpen())
                return;

            if (Input.GetKeyDown(KeyBind.Start))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            if (ViewManager.Instance.IsHistoryEmpty())
            {
                ViewManager.Instance.Show<GameMenuView>();
            }
            else
            {
                ViewManager.Instance.ShowLast();
            }
        }
    }
}
