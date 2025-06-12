using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Static loader class responsible for preloading and caching all PokemonData assets
    /// from the Resources/Pokemons folder. Provides fast lookup access to pokemon data by name.
    /// </summary>
    public class PokemonDataLoader
    {
        private const string DataPath = "Pokemons/";
        private readonly static Dictionary<string, PokemonData> cachedData = new();

        /// <summary>
        /// Loads all PokemonData assets from the Resources/Moves folder into memory.
        /// </summary>
        public static void PreloadAll()
        {
            cachedData.Clear();

            PokemonData[] pokemons = Resources.LoadAll<PokemonData>(DataPath);

            foreach (PokemonData pokemon in pokemons)
            {
                string key = pokemon.PokemonName?.ToLowerInvariant();

                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError($"[PokemonDataLoader] PokemonData({pokemon.name}) has missing or empty PokemonName.");
                    continue;
                }

                if (!cachedData.ContainsKey(key))
                    cachedData[key] = pokemon;
                else
                    Debug.LogWarning($"Duplicate pokemon name detected: {pokemon.PokemonName}");
            }

            Debug.Log($"[PokemonDataLoader] Preloaded {cachedData.Count} pokemons.");
        }


        /// <summary>
        /// Attempts to retrieve a PokemonData by name (case-insensitive).
        /// </summary>
        /// <param name="name">The pokemon name.</param>
        /// <returns>The PokemonData if found, otherwise null.</returns>
        public static PokemonData Load(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            cachedData.TryGetValue(name.ToLowerInvariant(), out var data);
            return data;
        }
    }
}
