using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGame.Pokemons.Enums;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Pokemons.Definition
{
    /// <summary>
    /// Loads and caches all Pokémon definitions from Addressables using a shared label.
    /// Enables fast lookup of Pokémon by PokemonID at runtime.
    /// </summary>
    public static class PokemonDefinitionLoader
    {
        private const string AddressablesLabel = "pokemon-definition";

        private static Dictionary<PokemonId, PokemonDefinition> pokemonDefinitionCache;
        private static AsyncOperationHandle<IList<PokemonDefinition>> pokemonDefinitionHandle;

        /// <summary>
        /// Gets the cached Pokémon definitions keyed by their PokemonID.
        /// </summary>
        public static IReadOnlyDictionary<PokemonId, PokemonDefinition> PokemonDefinitionCache => pokemonDefinitionCache;

        /// <summary>
        /// Asynchronously loads all Pokémon definitions labeled "pokemon-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (pokemonDefinitionCache != null)
            {
                return;
            }

            pokemonDefinitionCache = new Dictionary<PokemonId, PokemonDefinition>();
            pokemonDefinitionHandle = Addressables.LoadAssetsAsync<PokemonDefinition>(AddressablesLabel, null);

            IList<PokemonDefinition> pokemonDefinitionList = await pokemonDefinitionHandle.Task;

            foreach (PokemonDefinition pokemonData in pokemonDefinitionList)
            {
                PokemonId key = pokemonData.PokemonID;

                if (!pokemonDefinitionCache.ContainsKey(key))
                {
                    pokemonDefinitionCache[key] = pokemonData;
                }
                else
                {
                    Log.Warning(nameof(PokemonDefinitionLoader),
                        $"Duplicate PokemonID detected: {key} (existing: {pokemonDefinitionCache[key].name}, duplicate: {pokemonData.name})");
                }
            }

            Log.Info(nameof(PokemonDefinitionLoader),
                $"Loaded {pokemonDefinitionCache.Count}/{pokemonDefinitionList.Count} Pokémon definition.");
        }

        /// <summary>
        /// Retrieves a Pokémon definition by PokemonID. Returns null if missing.
        /// </summary>
        /// <param name="id">The unique identifier of the Pokémon to retrieve.</param>
        public static PokemonDefinition Get(PokemonId id)
        {
            if (pokemonDefinitionCache == null)
            {
                Log.Error(nameof(PokemonDefinitionLoader), "Not initialized. You must call LoadAllAsync() before using Get().");
                return null;
            }

            pokemonDefinitionCache.TryGetValue(id, out var pokemonData);
            return pokemonData;
        }

        /// <summary>
        /// Tries to get a Pokémon definition by PokemonID.
        /// Returns true if found; false otherwise (or if not initialized).
        /// </summary>
        public static bool TryGet(PokemonId id, out PokemonDefinition pokemonData)
        {
            pokemonData = null;

            if (pokemonDefinitionCache == null)
            {
                Log.Error(nameof(PokemonDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return false;
            }

            if (pokemonDefinitionCache.TryGetValue(id, out var def))
            {
                pokemonData = def;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Releases Addressables handle and clears the cache.
        /// </summary>
        public static void Unload()
        {
            if (pokemonDefinitionCache == null)
            {
                return;
            }

            if (pokemonDefinitionHandle.IsValid())
            {
                Addressables.Release(pokemonDefinitionHandle);
            }

            pokemonDefinitionCache.Clear();
            pokemonDefinitionCache = null;

            Log.Info(nameof(PokemonDefinitionLoader), "Pokémon definition cache unloaded.");
        }
    }
}
