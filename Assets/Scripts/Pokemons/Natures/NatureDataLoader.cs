using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons.Natures
{
    /// <summary>
    /// Static loader class responsible for preloading and caching all NatureData assets 
    /// from the Resources/Natures folder. Provides fast lookup access to nature data by name.
    /// </summary>
    public static class NatureDataLoader
    {
        private const string NaturePath = "Natures/";
        private static readonly Dictionary<string, NatureData> cachedData = new();

        /// <summary>
        /// Loads all NatureData assets from the Resources/Natures folder into memory.
        /// Should be called once during game initialization.
        /// </summary>
        public static void PreloadAll()
        {
            cachedData.Clear();
            NatureData[] natures = Resources.LoadAll<NatureData>(NaturePath);

            foreach (NatureData nature in natures)
            {
                string key = nature.NatureName?.ToLowerInvariant();

                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError($"[NatureLoader] NatureData({nature.name}) has missing or empty NatureName.");
                    continue;
                }

                if (!cachedData.ContainsKey(key))
                    cachedData[key] = nature;
                else
                    Debug.LogWarning($"[NatureLoader] Duplicate nature name detected: {nature.NatureName}");
            }

            Debug.Log($"[NatureLoader] Preloaded {cachedData.Count} natures.");
        }

        /// <summary>
        /// Attempts to retrieve a NatureData by name (case-insensitive).
        /// </summary>
        /// <param name="name">The nature name.</param>
        /// <returns>The NatureData if found, otherwise null.</returns>
        public static NatureData Load(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            cachedData.TryGetValue(name.ToLowerInvariant(), out var data);
            return data;
        }
    }
}
