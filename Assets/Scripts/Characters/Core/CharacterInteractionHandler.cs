using PokemonGame.Characters.Direction;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Pause;
using PokemonGame.Raycasting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Handles interaction checks in front of a character.
    /// Uses the character's input to trigger interactables
    /// like NPCs, signs, and objects.
    /// </summary>
    [DisallowMultipleComponent]
    public class CharacterInteractionHandler : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Raycast configuration that defines how far ahead the player can interact.")]
        private RaycastSettings raycastSettings;

        private Character character;

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        private void Update()
        {
            if (PauseManager.IsPaused)
            {
                return;
            }

            if (!character.StateController.Input.InteractPressed)
                return;

            Vector2 dir = character.StateController.FacingDirection.ToVector2Int();

            if (CheckForInteractables(dir))
            {
                // Only cancel to Idle if something was actually interacted with
                character.StateController.CancelToIdle();
            }
        }

        /// <summary>
        /// Executes all interactables in front of the character (e.g., NPCs, signs).
        /// Returns true if at least one was interacted with.
        /// </summary>
        /// <param name="direction">Direction to check for interactables.</param>
        private bool CheckForInteractables(Vector2 direction)
        {
            bool interacted = false;

            RaycastUtility.RaycastAndCall<IInteractable>(direction, raycastSettings, transform, interactable =>
            {
                interactable.Interact(character);
                interacted = true;
                return false; // keep checking other interactables
            });

            return interacted;
        }
    }
}
