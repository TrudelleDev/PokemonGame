using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame.Assets.Scripts.Characters.Spawn
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPoint;

        private void Start()
        {
            // Find the existing player object (from persistent scene)
            Character player = FindObjectOfType<Character>();

            if (player == null)
            {
                Debug.LogWarning("No player found in scene.");
                return;
            }
            if (spawnPoint != null)
            {
                player.transform.SetLocalPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
            else
            {
                Debug.LogWarning("No spawn point assigned to PlayerSpawner.");
            }
        }
    }
}
