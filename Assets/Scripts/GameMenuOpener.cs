using PokemonGame.Characters.States;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Handles the opening of the game menu when no other views are active.
    /// </summary>
    public class GameMenuOpener : MonoBehaviour
    {
        [Tooltip("The view for the game menu.")]
        [SerializeField] private GameMenuView gameMenuView;

        [Tooltip("The player state controller.")]
        [SerializeField] private CharacterStateController playerStateController;


        private void Update()
        {
            if (playerStateController.TileMover.IsMoving) return;

            if (DialogueBox.Instance.IsOpen) return;

            if (Input.GetKeyDown(KeyBind.Start))
            {
                TryOpenMenu();
            }
        }

        /// <summary>
        /// Opens the game menu if no other views are currently open.
        /// </summary>
        private void TryOpenMenu()
        {
            if (ViewManager.Instance.IsHistoryEmpty())
            {
                ViewManager.Instance.Show<GameMenuView>();
            }
        }
    }
}
