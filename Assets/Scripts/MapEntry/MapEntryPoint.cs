using PokemonGame.MapEntry.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Defines an entry point in a map.
    /// Automatically registers itself with the MapEntryRegistry.
    /// </summary>
    public class MapEntryPoint : MonoBehaviour
    {
        // Gizmo visualization settings
        private const float GizmoTileSize = 1f;
        private const float GizmoLabelOffsetY = 0.3f;
        private static readonly Color GizmoColor = Color.green;

        [SerializeField, Required]
        [Tooltip("Unique ID for this entry point in the map.")]
        private MapEntryID locationID;

        /// <summary>
        /// The unique identifier for this entry point.
        /// </summary>
        public MapEntryID ID => locationID;

        private void OnEnable()
        {
            MapEntryRegistry.Register(this);
        }

        private void OnDisable()
        {
            MapEntryRegistry.Unregister(this);
        }

        /// <summary>
        /// Draws a tile-sized rectangle and label in the Scene view for easier debugging.
        /// Only runs in the Unity Editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;

            Gizmos.DrawWireCube(
                transform.position,
                new Vector3(GizmoTileSize, GizmoTileSize, 0f)
            );

#if UNITY_EDITOR
            UnityEditor.Handles.Label(
                transform.position + Vector3.up * GizmoLabelOffsetY,
                locationID.ToString()
            );
#endif
        }
    }
}
