using PokemonGame.Characters.Spawn.Enums;
using UnityEngine;

namespace PokemonGame.Characters.Spawn
{
    /// <summary>
    /// Manages player spawn location selection between scene transitions.
    /// Stores the next spawn location to position the player when a new scene loads.
    /// </summary>
    public class SpawnLocationManager : Singleton<SpawnLocationManager>
    {
        /// <summary>
        /// The spawn location where the player should appear in the next scene.
        /// Defaults to <see cref="SpawnLocationID.None"/> when no location is requested.
        /// </summary>
        public SpawnLocationID NextSpawnLocation { get; private set; } = SpawnLocationID.None;

        /// <summary>
        /// Sets the spawn location to use in the next scene.
        /// </summary>
        /// <param name="id">The identifier of the target spawn location.</param>
        public void SetNextSpawnLocation(SpawnLocationID id)
        {
            NextSpawnLocation = id;
        }

        /// <summary>
        /// Clears the current spawn location, resetting it back to <see cref="SpawnLocationID.None"/>.
        /// </summary>
        public void Clear()
        {
            NextSpawnLocation = SpawnLocationID.None;
        }
    }
}
