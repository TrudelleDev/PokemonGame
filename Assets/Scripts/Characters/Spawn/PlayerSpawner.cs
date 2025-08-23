using PokemonGame.Characters.Spawn.Enums;
using UnityEngine;

namespace PokemonGame.Characters.Spawn
{
    /// <summary>
    /// Spawns the player at the correct location when a scene loads.
    /// Uses <see cref="SpawnLocationManager"/> to determine the target spawn point,
    /// and positions the player at the corresponding <see cref="SpawnLocation"/>.
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        private void Start()
        {
            Character player = FindObjectOfType<Character>();

            if (player == null)
            {
                Log.Warning(nameof(PlayerSpawner), "No player found in scene.");
                return;
            }

            SpawnLocationID targetID = SpawnLocationManager.Instance.NextSpawnLocation;

            if (targetID == SpawnLocationID.None)
            {
                Log.Info(nameof(PlayerSpawner), "No spawn point requested. Player will remain at current position.");
            }
            else
            {
                SpawnLocation spawnPoint = FindSpawnPoint(targetID);

                if (spawnPoint != null)
                {
                    player.transform.SetLocalPositionAndRotation(
                        spawnPoint.transform.position,
                        spawnPoint.transform.rotation
                    );
                }
                else
                {
                    Log.Warning(nameof(PlayerSpawner), $"No spawn point with ID {targetID} found in scene.");
                }
            }

            // Always clear after use
            SpawnLocationManager.Instance.Clear();
        }

        /// <summary>
        /// Searches the current scene for a spawn point matching the given ID.
        /// </summary>
        private SpawnLocation FindSpawnPoint(SpawnLocationID id)
        {
            SpawnLocation[] points = FindObjectsOfType<SpawnLocation>();

            foreach (SpawnLocation point in points)
            {
                if (point.ID == id)
                {
                    return point;
                }
            }

            return null;
        }
    }
}
