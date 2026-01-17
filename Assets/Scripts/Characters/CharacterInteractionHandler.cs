using PokemonGame.Characters.Directions;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Pause;
using PokemonGame.Raycasting;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Handles interactable checks in front of the character based on input.
    /// Triggers objects like NPCs, signs, and items.
    /// </summary>
    public sealed class CharacterInteractionHandler
    {
        private readonly Character character;
        private readonly RaycastSettings raycastSettings;
        private readonly Transform transform;

        /// <summary>
        /// Initializes a new instance of <see cref="CharacterInteractionHandler"/>.
        /// Sets up the character reference and raycast settings for interaction checks.
        /// </summary>
        /// <param name="character">The character that will perform interactions.</param>
        /// <param name="raycastSettings">Configuration for raycasting to detect interactables.</param>
        public CharacterInteractionHandler(Character character, RaycastSettings raycastSettings)
        {
            this.character = character;
            this.raycastSettings = raycastSettings;
            this.transform = character.transform;
        }

        public void Tick()
        {
            if (PauseManager.IsPaused || !character.StateController.Input.InteractPressed)
                return;

            Vector2 direction = character.StateController.FacingDirection.ToVector2Int();

            if (TryInteract(direction))
            {
                character.StateController.CancelToIdle();
            }
        }

        /// <summary>
        /// Executes all interactables in the specified direction.
        /// </summary>
        /// <param name="direction">Direction to check.</param>
        /// <returns>True if at least one interactable was triggered.</returns>
        private bool TryInteract(Vector2 direction)
        {
            bool interacted = false;

            RaycastUtility.RaycastAndCall<IInteractable>(direction, raycastSettings, transform, interactable =>
            {
                interactable.Interact(character);
                interacted = true;
                return false; // continue checking other interactables
            });

            return interacted;
        }
    }
}
