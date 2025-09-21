using PokemonGame.Characters.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Minimal camera follow.
    /// Follows the player using a fixed world-space offset.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("The Character this camera will follow. Assign the player GameObject here.")]
        private Character player;

        [SerializeField, Required]
        [Tooltip("Offset from the player's position (world units). Default: (0, 1, -10).")]
        private Vector3 offset = new Vector3(0f, 1f, -10f);

        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }
}
