using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves
{
    /// <summary>
    /// Static loader class responsible for preloading and caching all MoveData assets
    /// from the Resources/Moves folder. Provides fast lookup access to move data by name.
    /// </summary>
    public static class MoveDataLoader
    {
        private const string DataPath = "Moves/";
        private static readonly Dictionary<string, MoveData> cachedData = new();

        /// <summary>
        /// Loads all MoveData assets from the Resources/Moves folder into memory.
        /// </summary>
        public static void PreloadAll()
        {
            cachedData.Clear();

            MoveData[] moves = Resources.LoadAll<MoveData>(DataPath);

            foreach (MoveData move in moves)
            {
                string key = move.MoveName?.ToLowerInvariant();

                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError($"[MoveDataLoader] MoveData({move.name}) has missing or empty MoveName.");
                    continue;
                }

                if (!cachedData.ContainsKey(key))
                    cachedData[key] = move;
                else
                    Debug.LogWarning($"Duplicate move name detected: {move.MoveName}");
            }

            Debug.Log($"[MoveDataLoader] Preloaded {cachedData.Count} moves.");
        }

        /// <summary>
        /// Attempts to retrieve a MoveData by name (case-insensitive).
        /// </summary>
        /// <param name="name">The move name.</param>
        /// <returns>The MoveData if found, otherwise null.</returns>
        public static MoveData Load(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            cachedData.TryGetValue(name.ToLowerInvariant(), out var data);
            return data;
        }
    }
}
