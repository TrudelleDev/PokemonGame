using System;
using System.Collections.Generic;
using PokemonGame.MapEntry.Enums;
using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Manages map entry points across scenes.
    /// Tracks active entry points, stores the next target,
    /// and signals when entries are ready for player placement.
    /// </summary>
    public static class MapEntryRegistry
    {
        private static readonly Dictionary<MapEntryID, MapEntryPoint> locations = new();

        /// <summary>
        /// The next entry point where the player should appear. Defaults to None.
        /// </summary>
        public static MapEntryID NextEntry { get; private set; } = MapEntryID.None;

        /// <summary>
        /// Event fired when all entry points in a map have been registered.
        /// </summary>
        public static event Action OnEntryPointsReady;

        /// <summary>
        /// Registers a map entry point.
        /// </summary>
        /// <param name="point">The entry point to add.</param>
        public static void Register(MapEntryPoint point)
        {
            if (locations.ContainsKey(point.ID))
            {
                Log.Warning(nameof(MapEntryRegistry), $"Duplicate entry point ID detected: {point.ID} in scene {point.gameObject.scene.name}");
                return;
            }

            locations.Add(point.ID, point);
        }

        /// <summary>
        /// Unregisters a map entry point.
        /// </summary>
        /// <param name="point">The entry point to remove.</param>
        public static void Unregister(MapEntryPoint point)
        {
            if (locations.ContainsKey(point.ID))
            {
                locations.Remove(point.ID);
            }
        }

        /// <summary>
        /// Tries to get the world position of a map entry point.
        /// </summary>
        /// <param name="id">The entry point ID.</param>
        /// <param name="position">Outputs the position if found.</param>
        /// <returns>True if an entry point was found; otherwise false.</returns>
        public static bool TryGetPosition(MapEntryID id, out Vector3 position)
        {
            if (locations.TryGetValue(id, out var point))
            {
                position = point.transform.position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        /// <summary>
        /// Raises the EntryPointsReady event to signal that
        /// all entry points in the current map are registered.
        /// </summary>
        public static void NotifyEntryPointsReady()
        {
            OnEntryPointsReady?.Invoke();
        }

        /// <summary>
        /// Sets the next entry point for the player.
        /// </summary>
        /// <param name="id">The entry point ID to set.</param>
        public static void SetNextEntry(MapEntryID id)
        {
            NextEntry = id;
        }

        /// <summary>
        /// Clears the stored entry point (resets to None).
        /// </summary>
        public static void Clear()
        {
            NextEntry = MapEntryID.None;
        }
    }
}
