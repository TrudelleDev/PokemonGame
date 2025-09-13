using PokemonGame.Characters.Inputs;
using PokemonGame.Characters.States;
using PokemonGame.Pause;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Handles the opening of the game menu when no other views are active.
    /// </summary>
    public class GameMenuOpener : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("The player state controller.")]
        private CharacterStateController playerStateController;

        private void Update()
        {
            if (PauseManager.IsPaused || playerStateController.TileMover.IsMoving)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Menu))
            {
                TryOpenMenu();
            }
        }

        /// <summary>
        /// Opens the game menu if no other views are currently open.
        /// </summary>
        private void TryOpenMenu()
        {
            if (!ViewManager.Instance.HasActiveView)
            {
                ViewManager.Instance.Show<GameMenuView>();
            }
        }
    }
}
