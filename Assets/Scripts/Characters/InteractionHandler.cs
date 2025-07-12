using PokemonGame.Characters.Interfaces;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Handles interaction by raycasting in the player's facing direction
    /// and triggering OnInteract on any valid object directly in front of the player.
    /// </summary>
    public class InteractionHandler : MonoBehaviour
    {
        private const float RAYCAST_DISTANCE = 1f; // Distance the raycast will check
        private const float OFFSET_Y = 0.5f;       // Vertical offset to align with character's center

        private Character character;
        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[5];

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Accept))
                TryInteract();
        }

        /// <summary>
        /// Casts a ray in the player's facing direction and interacts with any valid target.
        /// </summary>
        private void TryInteract()
        {
            // Calculate direction and origin of the raycast
            Vector2 playerFacingDirection = character.CurrentDirection.ToVector2();
            Vector3 rayOrigin = transform.position + Vector3.up * OFFSET_Y;

            // Draw debug ray in Scene view for visual reference
            Debug.DrawRay(rayOrigin, playerFacingDirection * RAYCAST_DISTANCE, Color.green, 0.5f);

            // Perform raycast to detect interactable objects
            int hitCount = Physics2D.RaycastNonAlloc(rayOrigin, playerFacingDirection, hitBuffer, RAYCAST_DISTANCE);

            for (int i = 0; i < hitCount; i++)
            {
                Collider2D collider = hitBuffer[i].collider;

                if (collider == null)
                    continue; // Skip if nothing was hit

                // Call Interact on all components implementing IInteract
                IInteract[] interactables = collider.GetComponents<IInteract>();
                foreach (IInteract interactable in interactables)
                {
                    interactable.Interact(character);
                }
            }
        }
    }
}
