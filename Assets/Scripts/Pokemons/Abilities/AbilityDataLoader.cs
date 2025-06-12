using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities
{
    /// <summary>
    /// Static loader class responsible for preloading and caching all AbilityData assets 
    /// from the Resources/Abilities folder. Enables fast, case-insensitive lookup of 
    /// abilities by name at runtime.
    /// </summary>
    public static class AbilityDataLoader
    {
        private const string AbilityPath = "Abilities/";
        private static readonly Dictionary<string, AbilityData> cachedData = new();

        /// <summary>
        /// Loads all AbilityData assets from the Resources/Abilities folder into memory.
        /// Clears any previously cached data. Logs errors for missing or duplicate names.
        /// </summary>
        public static void PreloadAll()
        {
            cachedData.Clear();

            AbilityData[] abilities = Resources.LoadAll<AbilityData>(AbilityPath);

            foreach (AbilityData ability in abilities)
            {
                string key = ability.AbilityName.ToLowerInvariant();

                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError($"[AbilityDataLoader] AbilityData({ability.name}) has missing or empty AbilityName.");
                    continue;
                }

                if (!cachedData.ContainsKey(key))
                    cachedData[key] = ability;
                else
                    Debug.LogWarning($"Duplicate ability name detected: {ability.AbilityName}");
            }

            Debug.Log($"[AbilityLoader] Preloaded {cachedData.Count} abilities.");
        }

        /// <summary>
        /// Attempts to retrieve a AbilityData by name (case-insensitive).
        /// </summary>
        /// <param name="name">The ability name.</param>
        /// <returns>The AbilityData if found, otherwise null.</returns>
        public static AbilityData Load(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            cachedData.TryGetValue(name.ToLowerInvariant(), out var data);
            return data;
        }
    }
}
