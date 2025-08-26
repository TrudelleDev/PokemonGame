using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Enums.Extensions;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Provides directional raycasting functionality for obstacle detection.
    /// Commonly used by movement components to verify walkable paths.
    /// Also renders the last raycast direction in the Scene view using Gizmos.
    /// </summary>
    public class TileRaycaster : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayLength = 1f;
        [SerializeField] private Vector3 raycastOriginOffset = new Vector3(0, 0.5f, 0);

        private FacingDirection lastDirection = FacingDirection.South;

        /// <summary>
        /// Performs a raycast in the given facing direction to determine if the path is unobstructed.
        /// </summary>
        public bool IsPathClear(FacingDirection direction)
        {
            lastDirection = direction;

            Vector3 origin = transform.position + raycastOriginOffset;
            Vector2 rayDir = direction.ToVector2Int();
            RaycastHit2D hit = Physics2D.Raycast(origin, rayDir, rayLength, layerMask);
            return hit.collider == null;
        }

        /// <summary>
        /// Draws a ray in the Scene view to visualize the last direction checked via raycast.
        /// Only visible when the object is selected in the editor.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 origin = transform.position + raycastOriginOffset;
            Vector2 rayDir = lastDirection.ToVector2Int();

            Gizmos.DrawRay(origin, rayDir * rayLength);
        }
    }
}
