using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Pause;
using MonsterTamer.Raycasting;
using UnityEngine;

namespace MonsterTamer.Characters.Core
{
    /// <summary>
    /// Handles character interactions with objects in front of them (NPCs, signs, items).
    /// Checks input each tick and triggers interactables.
    /// </summary>
    internal sealed class CharacterInteractionHandler
    {
        private readonly Character character;
        private readonly RaycastSettings raycastSettings;
        private readonly Transform transform;

        internal CharacterInteractionHandler(Character character, RaycastSettings raycastSettings) =>
             (this.character, this.raycastSettings, this.transform) = (character, raycastSettings, character.transform);

        /// <summary>
        /// Called each frame to check for interaction input and trigger interactables.
        /// </summary>
        internal void Tick()
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
        /// Attempts to trigger all interactables in the given direction.
        /// </summary>
        /// <param name="direction">Direction to check for interactables.</param>
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
