using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Loads and caches all PokemonData assets from Addressables using a shared label.
    /// Enables fast lookup of Pokémon by PokemonID at runtime.
    /// </summary>
    public static class PokemonDataLoader
    {
        private static Dictionary<PokemonID, PokemonData> pokemonDataCache;
        private static AsyncOperationHandle<IList<PokemonData>> pokemonDataHandle;

        public static IReadOnlyDictionary<PokemonID, PokemonData> PokemonDataCache => pokemonDataCache;

        /// <summary>
        /// Asynchronously loads all PokemonData assets labeled "pokemon-data" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (pokemonDataCache != null)
            {
                return;
            }

            pokemonDataCache = new Dictionary<PokemonID, PokemonData>();
            pokemonDataHandle = Addressables.LoadAssetsAsync<PokemonData>("pokemon-data", null);

            IList<PokemonData> pokemonDataList = await pokemonDataHandle.Task;

            foreach (PokemonData pokemonData in pokemonDataList)
            {
                PokemonID key = pokemonData.PokemonID;

                if (!pokemonDataCache.ContainsKey(key))
                {
                    pokemonDataCache[key] = pokemonData;
                }
                else
                {
                    Log.Warning(nameof(PokemonDataLoader), $"Duplicate PokemonID detected: {key} (existing: {pokemonDataCache[key].name}, duplicate: {pokemonData.name})");
                }
            }

            Log.Info(nameof(PokemonDataLoader), $"Loaded {pokemonDataCache.Count}/{pokemonDataList.Count} Pokémon from Addressables.");
        }

        /// <summary>
        /// Retrieves a PokemonData by PokemonID. Returns null if missing.
        /// </summary>
        public static PokemonData Get(PokemonID id)
        {
            if (pokemonDataCache == null)
            {
                Log.Error(nameof(PokemonDataLoader), "Not initialized. You must call LoadAllAsync() before using Get().");
                return null;
            }

            if (!pokemonDataCache.TryGetValue(id, out PokemonData pokemonData))
            {
                return null;
            }

            return pokemonData;
        }

        /// <summary>
        /// Releases Addressables handle and clears the cache.
        /// </summary>
        public static void Unload()
        {
            if (pokemonDataCache == null)
            {
                return;
            }

            if (pokemonDataHandle.IsValid())
            {
                Addressables.Release(pokemonDataHandle);
            }

            pokemonDataCache.Clear();
            pokemonDataCache = null;

            Log.Info(nameof(PokemonDataLoader), "Pokémon data cache unloaded.");
        }
    }
}
