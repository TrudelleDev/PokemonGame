using PokemonGame.Characters;
using PokemonGame.Characters.Core;
using PokemonGame.MapEntry.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Places the player at the correct map entry point when a map finishes loading.
    /// Subscribes to the OnEntryPointsReady event in MapEntryRegistry.
    /// </summary>
    public class PlayerMapEntry : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the player character that should be placed.")]
        private Character player;

        private void OnEnable()
        {
            MapEntryRegistry.OnEntryPointsReady += HandleEntryPointsReady;
        }

        private void OnDisable()
        {
            MapEntryRegistry.OnEntryPointsReady -= HandleEntryPointsReady;
        }

        /// <summary>
        /// Handles player placement when entry points are registered.
        /// </summary>
        private void HandleEntryPointsReady()
        {
            MapEntryID targetID = MapEntryRegistry.NextEntry;

            if (targetID == MapEntryID.None)
            {
                return;
            }

            if (MapEntryRegistry.TryGetPosition(targetID, out Vector3 position))
            {
                player.Teleport(position);
            }
            else
            {
                Log.Warning(nameof(PlayerMapEntry), $"No entry point with ID {targetID} found.");
            }

            MapEntryRegistry.Clear();
        }
    }
}
