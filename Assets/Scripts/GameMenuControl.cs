using PokemonGame.Characters.States;
using PokemonGame.Systems.Dialogue;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Handles the toggling and display of the game menu.
    /// </summary>
    public class GameMenuControl : MonoBehaviour
    {
        [Header("Dependencies")]

        [Tooltip("The view for the game menu.")]
        [SerializeField] private GameMenuView gameMenuView;

        [Tooltip("The player state controller.")]
        [SerializeField] private CharacterStateController playerStateController;

        private void Update()
        {
            if (playerStateController.TileMover.IsMoving) return;

            if (DialogueBox.Instance.IsOpen()) return;

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
                ViewManager.Instance.GoToPreviousView();
            }
        }
    }
}
