using PokemonGame.Characters.Direction;
using PokemonGame.Raycasting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Provides directional raycasting functionality for obstacle detection.
    /// </summary>
    [DisallowMultipleComponent]
    public class TileRaycaster : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Configuration for raycasting: offset, distance, and collision mask.")]
        private RaycastSettings raycastSettings;

        private FacingDirection lastDirection = FacingDirection.South;

        /// <summary>
        /// Performs a raycast in the given facing direction to determine if the path is unobstructed.
        /// </summary>
        /// <param name="direction">The facing direction to check.</param>
        /// <returns>True if no obstacle was detected; otherwise false.</returns>
        public bool IsPathClear(FacingDirection direction)
        {
            lastDirection = direction;

            // Use the RaycastSettings for offset and distance
            Vector3 origin = transform.position + new Vector3(raycastSettings.RaycastOffset.x, raycastSettings.RaycastOffset.y, 0);
            Vector2 rayDir = direction.ToVector2Int();
            RaycastHit2D hit = Physics2D.Raycast(origin, rayDir, raycastSettings.RaycastDistance, raycastSettings.InteractionMask);
            return hit.collider == null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 origin = transform.position + new Vector3(raycastSettings.RaycastOffset.x, raycastSettings.RaycastOffset.y, 0);
            Vector2 rayDir = lastDirection.ToVector2Int();

            Gizmos.DrawRay(origin, rayDir * raycastSettings.RaycastDistance);
        }
    }
}
