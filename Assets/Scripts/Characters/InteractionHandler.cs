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

        private void RaycastAndCall<T>(System.Action<T> callback)
        {
            Vector2 direction = character.CurrentDirection.ToVector2();
            Vector3 origin = transform.position + Vector3.up * OFFSET_Y;

            int hitCount = Physics2D.RaycastNonAlloc(origin, direction, hitBuffer, RAYCAST_DISTANCE);

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
