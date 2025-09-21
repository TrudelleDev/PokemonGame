using PokemonGame.Audio;
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
        [Title("References")]
        [SerializeField, Required]
        [Tooltip("Reference to the player's state controller. Used to block opening the menu while moving.")]
        private CharacterStateController playerStateController;

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect played when the game menu is successfully opened.")]
        private AudioClip openSound;

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
        /// Attempts to open the game menu if no other views are currently active.
        /// Plays the assigned open sound on success.
        /// </summary>
        private void TryOpenMenu()
        {
            if (!ViewManager.Instance.HasActiveView)
            {
                AudioManager.Instance.PlaySFX(openSound);
                ViewManager.Instance.Show<GameMenuView>();
            }
        }
    }
}
