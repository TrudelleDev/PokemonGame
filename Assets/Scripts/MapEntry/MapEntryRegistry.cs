using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Global registry for map entry points.
    /// Tracks <see cref="EntryPointMarker"/> instances in each scene and 
    /// provides lookup for relocating the player or NPCs.
    /// </summary>
    public static class MapEntryRegistry
    {
        /// <summary>
        /// The ID of the entry point to use when the next scene loads.
        /// </summary>
        public static MapEntryID NextEntryId { get; private set; } = MapEntryID.None;

        private static readonly Dictionary<MapEntryID, EntryPointMarker> entryMarkers = new();

        /// <summary>
        /// Registers an entry point marker with the registry.
        /// </summary>
        /// <param name="marker">The marker instance to register.</param>
        public static void Register(EntryPointMarker marker)
        {
            if (marker == null)
            {
                return;
            }

            entryMarkers[marker.EntryId] = marker;
        }

        /// <summary>
        /// Unregisters an entry point marker, if it matches the instance currently stored.
        /// </summary>
        /// <param name="marker">The marker instance to unregister.</param>
        public static void Unregister(EntryPointMarker marker)
        {
            if (marker == null)
            {
                return;
            }

            if (entryMarkers.TryGetValue(marker.EntryId, out EntryPointMarker existing) && existing == marker)
            {
                entryMarkers.Remove(marker.EntryId);
            }
        }

        /// <summary>
        /// Sets the ID of the entry point to use for the next scene load.
        /// </summary>
        /// <param name="entryId">The ID of the entry point to set as next.</param>
        public static void SetNextEntry(MapEntryID entryId)
        {
            NextEntryId = entryId;
        }

        /// <summary>
        /// Clears the pending next entry point.
        /// </summary>
        public static void Clear()
        {
            NextEntryId = MapEntryID.None;
        }

        /// <summary>
        /// Attempts to resolve the world position of a given entry point.
        /// </summary>
        /// <param name="entryId">The entry ID to look up.</param>
        /// <param name="position">Outputs the world position of the entry if found, otherwise <c>default</c>.</param>
        /// <returns><c>true</c> if the entry was found and position resolved; otherwise <c>false</c>.</returns>
        public static bool TryGetEntryPosition(MapEntryID entryId, out Vector3 position)
        {
            if (entryMarkers.TryGetValue(entryId, out EntryPointMarker marker))
            {
                position = marker.Position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }
    }
}