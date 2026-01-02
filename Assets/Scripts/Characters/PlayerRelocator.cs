using PokemonGame.Characters.Core;
using PokemonGame.MapEntry;
using PokemonGame.SceneManagement;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Relocates the player after a scene load by resolving the next entry
    /// from <see cref="MapEntryRegistry"/>. If no valid entry is found,
    /// defaults to world origin (0,0,0).
    /// </summary>
    public class PlayerRelocator : Singleton<PlayerRelocator>
    {
        [SerializeField, Required]
        [Tooltip("The player character instance to position after scene load.")]
        private Character player;

        private void OnEnable()
        {
            SceneReadyNotifier.OnSceneReady += RelocatePlayer;
        }

        private void OnDisable()
        {
            SceneReadyNotifier.OnSceneReady -= RelocatePlayer;
        }

        internal void RelocatePlayer()
        {
            MapEntryID spawnPointId = MapEntryRegistry.NextEntryId;

            if (spawnPointId != MapEntryID.None && MapEntryRegistry.TryGetEntryPosition(spawnPointId, out var position))
            {
                player.Teleport(position);
                MapEntryRegistry.Clear();
            }
            else
            {
                // Fallback: spawn at world origin
                Log.Warning(nameof(PlayerRelocator), $" No entry marker found for {spawnPointId}, spawning at (0,0,0).");
                player.Teleport(Vector3.zero);
            }
        }
    }
}
