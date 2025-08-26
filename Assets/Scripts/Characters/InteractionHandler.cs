using PokemonGame.Characters.Enums.Extensions;
using PokemonGame.Characters.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Handles player interactions by raycasting in the facing direction
    /// and invoking interaction methods on objects in front of the character.
    /// Supports both manual "accept" interactions and automatic trigger checks.
    /// </summary>
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField, Required]
        [Tooltip("Maximum distance the raycast checks for interactables.")]
        private float raycastDistance = 1f;

        [SerializeField,Required]
        [Tooltip("Vertical offset from the character pivot to cast the ray.")]
        private float offsetY = 0.5f;

        [SerializeField, Required]
        [Tooltip("Which layers are considered interactable.")]
        private LayerMask interactionMask = Physics2D.DefaultRaycastLayers;

        private Character character;
        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[5];

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        private void Update()
        {
            // Manual interact
            if (Input.GetKeyDown(KeyBind.Accept))
            {
                CheckForInteractables();
            }

            // Auto triggers
            CheckForTriggers();
        }

        private void CheckForInteractables()
        {
            RaycastAndCall<IInteract>(interactable => interactable.Interact(character));
        }

        private void CheckForTriggers()
        {
            RaycastAndCall<ITrigger>(trigger => trigger.Trigger(character));
        }

        /// <summary>
        /// Generic raycast method that finds all matching components in front of the character
        /// and invokes a callback for each.
        /// </summary>
        private void RaycastAndCall<T>(System.Action<T> callback)
        {
            Vector2 direction = character.StateController.FacingDirection.ToVector2Int();
            Vector3 origin = transform.position + Vector3.up * offsetY;

            int hitCount = Physics2D.RaycastNonAlloc(origin, direction, hitBuffer, raycastDistance, interactionMask);

            for (int i = 0; i < hitCount; i++)
            {
                Collider2D collider = hitBuffer[i].collider;
                if (collider == null) continue;

                T[] components = collider.GetComponents<T>();
                foreach (T comp in components)
                {
                    callback(comp);
                }
            }
        }
    }
}
