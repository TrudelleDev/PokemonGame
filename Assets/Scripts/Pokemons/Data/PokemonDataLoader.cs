using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Loads and caches all Pokémon data from Addressables using a shared label.
    /// </summary>
    public static class PokemonDataLoader
    {
        private static Dictionary<string, PokemonData> cache;

        public static IReadOnlyDictionary<string, PokemonData> Cache => cache;

        /// <summary>
        /// Asynchronously loads all Pokémon data assets labeled "pokemon-data" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (cache != null) return;

            cache = new Dictionary<string, PokemonData>();

            AsyncOperationHandle<IList<PokemonData>> loadHandle = Addressables.LoadAssetsAsync<PokemonData>("pokemon-data", null);
            IList<PokemonData> pokemons = await loadHandle.Task;

            foreach (PokemonData pokemon in pokemons)
            {
                if (string.IsNullOrEmpty(pokemon.DisplayName))
                {
                    Log.Error(nameof(PokemonDataLoader), $"Pokémon has no DisplayName: {pokemon.name}");
                    continue;
                }

                string key = pokemon.DisplayName.ToLowerInvariant();

                if (!cache.ContainsKey(key))
                {
                    cache[key] = pokemon;
                }
                else
                {
                    Log.Warning(nameof(PokemonDataLoader), $"Duplicate Pokémon: {pokemon.DisplayName}");
                }

            }

            Log.Info(nameof(PokemonDataLoader), $"Loaded {cache.Count} Pokémon from Addressables.");
        }

        /// <summary>
        /// Retrieves Pokémon data by name (case-insensitive).
        /// </summary>
        public static PokemonData Get(string name)
        {
            if (cache == null)
            {
                Log.Error(nameof(PokemonDataLoader), "not initialized. You must call LoadAllAsync() before using Get().");
                return null;
            }

            string key = name.ToLowerInvariant();
            return cache.TryGetValue(key, out PokemonData data) ? data : null;
        }
    }
}
