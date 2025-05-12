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

        private Direction lastDirection = Direction.None;

        /// <summary>
        /// Performs a raycast in the given direction to determine if the path is unobstructed.
        /// </summary>
        /// <param name="direction">The direction in which to cast the ray.</param>
        /// <returns>True if the ray hits no colliders; otherwise, false.</returns>
        public bool IsPathClear(Direction direction)
        {
            lastDirection = direction;

            Vector3 origin = transform.position + raycastOriginOffset;
            Vector2Int dirVec = DirectionExtensions.ToVector(direction);
            Vector2 rayDir = new Vector2(dirVec.x, dirVec.y).normalized;
            RaycastHit2D hit = Physics2D.Raycast(origin, rayDir, rayLength, layerMask);
            return hit.collider == null;
        }

        /// <summary>
        /// Draws a ray in the Scene view to visualize the last direction checked via raycast.
        /// Only visible when the object is selected in the editor.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (lastDirection == Direction.None) return;

            Gizmos.color = Color.red;
            Vector3 origin = transform.position + raycastOriginOffset;
            Vector2Int dirVec = DirectionExtensions.ToVector(lastDirection);
            Gizmos.DrawRay(origin, new Vector2(dirVec.x, dirVec.y).normalized * rayLength);
        }
    }
}
