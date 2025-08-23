using PokemonGame.Characters.Spawn.Enums;
using UnityEngine;

namespace PokemonGame.Characters.Spawn
{
    /// <summary>
    /// Marks a location in the scene where the player can spawn.
    /// Each spawn location is identified by a <see cref="SpawnLocationID"/> 
    /// so that <see cref="SpawnLocationManager"/> can position the player 
    /// after a scene transition or load.
    /// </summary>
    public class SpawnLocation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The unique identifier for this spawn location.")]
        private SpawnLocationID spawnLocationID;

        /// <summary>
        /// Gets the identifier for this spawn location.
        /// </summary>
        public SpawnLocationID ID => spawnLocationID;

#if UNITY_EDITOR
        [SerializeField, Min(0.1f)]
        private float gizmoSize = 1f;

        [SerializeField]
        private Color gizmoColor = Color.green;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(transform.position, new Vector3(gizmoSize, gizmoSize, 0.01f));

            UnityEditor.Handles.color = gizmoColor;
            UnityEditor.Handles.Label(
                transform.position + Vector3.up * (gizmoSize * 0.6f),
                $"[{spawnLocationID}]"
            );
        }
#endif
    }
}
