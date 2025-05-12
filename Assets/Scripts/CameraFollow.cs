using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Character player; // Reference to the player
        [SerializeField] private Vector3 offset = new Vector3(0, 1, -10); // Fixed offset from the player (camera positioning)

        private void LateUpdate()
        {
            // Just follow the player exactly, with a fixed offset
            transform.position = player.transform.position + offset;
        }
    }
}
