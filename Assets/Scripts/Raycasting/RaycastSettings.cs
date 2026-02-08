using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Raycasting
{
    /// <summary>
    /// Holds common raycasting settings for interaction and pathfinding.
    /// </summary>
    [CreateAssetMenu(fileName = "RaycastSettings", menuName = "Game/RaycastSettings", order = 1)]
    public class RaycastSettings : ScriptableObject
    {
        [Header("Raycast Settings")]

        [Tooltip("The distance the raycast will travel.")]
        [SerializeField, Required]
        private float raycastDistance = 1f;

        [SerializeField, Required]
        [Tooltip("The offset for the raycast's origin (x, y offset).")]
        private Vector2 raycastOffset;

        [SerializeField, Required]
        [Tooltip("Layer mask used to define which objects are interactable.")]
        private LayerMask interactionMask = Physics2D.DefaultRaycastLayers;


        /// <summary>
        /// The offset applied to the raycast's origin, in the x and y directions.
        /// </summary>
        public Vector2 RaycastOffset => raycastOffset; 

        /// <summary>
        /// The distance the raycast will travel.
        /// </summary>
        public float RaycastDistance => raycastDistance;

        /// <summary>
        /// The layer mask that determines which objects are interactable with the raycast.
        /// </summary>
        public LayerMask InteractionMask => interactionMask;
    }
}
