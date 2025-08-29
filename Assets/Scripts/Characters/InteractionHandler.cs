using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Handles interaction raycasts in front of a character.
    /// Supports both manual interactables (e.g., NPCs, signs)
    /// and automatic triggers (e.g., warp tiles, cutscenes).
    /// </summary>
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private float raycastDistance = 1f;

        [SerializeField] private float offsetY = 0.5f;

        [SerializeField] private LayerMask interactionMask = Physics2D.DefaultRaycastLayers;

        private Character character;
        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[5];

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        /// <summary>
        /// Executes all interactables in front of the character.
        /// Returns true if at least one was triggered.
        /// </summary>
        public bool CheckForInteractables(Vector2 direction)
        {
            return RaycastAndCall<IInteract>(direction, interactable =>
            {
                interactable.Interact(character);
                return false; // don't stop — run all interactables
            });
        }

        /// <summary>
        /// Executes the first trigger found in front of the character.
        /// Returns true if a trigger was activated.
        /// </summary>
        public bool CheckForTriggers(Vector2 direction)
        {
            return RaycastAndCall<ITrigger>(direction, trigger =>
            {
                trigger.Trigger(character);
                return true; // stop after the first trigger
            });
        }

        private bool RaycastAndCall<T>(Vector2 direction, System.Func<T, bool> callback)
        {
            if (direction == Vector2.zero) return false;

            Vector3 origin = transform.position + Vector3.up * offsetY;
            int hitCount = Physics2D.RaycastNonAlloc(
                origin,
                direction,
                hitBuffer,
                raycastDistance,
                interactionMask
            );

            bool anyTriggered = false;

            for (int i = 0; i < hitCount; i++)
            {
                Collider2D collider = hitBuffer[i].collider;
                if (collider == null) continue;

                foreach (T comp in collider.GetComponents<T>())
                {
                    anyTriggered = true;
                    if (callback(comp))
                        return true; // stop if callback signals
                }
            }

            return anyTriggered;
        }
    }
}
