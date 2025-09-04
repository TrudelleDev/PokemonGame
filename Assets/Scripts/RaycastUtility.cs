using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Utility class for raycasting functionality.
    /// </summary>
    public static class RaycastUtility
    {
        /// <summary>
        /// Executes a raycast and calls the provided callback for any valid components found.
        /// </summary>
        public static bool RaycastAndCall<T>(Vector2 direction, RaycastSettings raycastSettings, Transform origin, System.Func<T, bool> callback)
        {
            if (direction == Vector2.zero) return false;

            // Use the RaycastSettings for offset and distance
            Vector3 originPosition = origin.position + new Vector3(0, raycastSettings.RaycastOffset.y, 0);
            RaycastHit2D[] hitBuffer = new RaycastHit2D[5];

            int hitCount = Physics2D.RaycastNonAlloc(
                originPosition,
                direction,
                hitBuffer,
                raycastSettings.RaycastDistance,
                raycastSettings.InteractionMask
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
