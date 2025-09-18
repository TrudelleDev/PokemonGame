using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Marker component for a map entry point.
    /// Registers with <see cref="MapEntryRegistry"/> and exposes its
    /// ID and world position for player relocation or spawning.
    /// </summary>
    public class EntryPointMarker : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Unique ID for this entry point, used by the MapEntryRegistry.")]
        private MapEntryID entryId;

        public MapEntryID EntryId => entryId;

        public Vector3 Position => transform.position;

        private void OnEnable()
        {
            MapEntryRegistry.Register(this);
        }

        private void OnDisable()
        {
            MapEntryRegistry.Unregister(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, TilemapInfo.CellSize);

#if UNITY_EDITOR
            UnityEditor.Handles.Label(transform.position, entryId.ToString());
#endif
        }
    }
}
