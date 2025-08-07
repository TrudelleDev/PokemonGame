using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGame.Natures.Enums;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Pokemons.Natures
{
    /// <summary>
    /// Loads and caches all nature definitions from Addressables using a shared label.
    /// Cached lookups are keyed by NatureID.
    /// </summary>
    public static class NatureDefinitionLoader
    {
        private const string AddressablesLabel = "nature-definition";

        private static Dictionary<NatureID, NatureDefinition> natureDefinitionCache;
        private static AsyncOperationHandle<IList<NatureDefinition>> natureDefinitionHandle;

        /// <summary>
        /// The loaded nature definitions indexed by NatureID.
        /// </summary>
        public static IReadOnlyDictionary<NatureID, NatureDefinition> NatureDefinitionCache => natureDefinitionCache;

        /// <summary>
        /// Asynchronously loads all nature definitions labeled "nature-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (natureDefinitionCache != null)
            {
                return;
            }

            natureDefinitionCache = new Dictionary<NatureID, NatureDefinition>();
            natureDefinitionHandle = Addressables.LoadAssetsAsync<NatureDefinition>(AddressablesLabel, null);

            IList<NatureDefinition> definitions = await natureDefinitionHandle.Task;

            foreach (NatureDefinition definition in definitions)
            {
                if (definition == null)
                {
                    Log.Warning(nameof(NatureDefinitionLoader), "Null NatureDefinition encountered; skipping.");
                    continue;
                }

                var key = definition.ID;

                if (!natureDefinitionCache.ContainsKey(key))
                {
                    natureDefinitionCache[key] = definition;
                }
                else
                {
                    Log.Warning(nameof(NatureDefinitionLoader),
                        $"Duplicate NatureID detected: {key} (existing: {natureDefinitionCache[key].name}, duplicate: {definition.name})");
                }
            }

            Log.Info(nameof(NatureDefinitionLoader),
                $"Loaded {natureDefinitionCache.Count}/{definitions.Count} nature definitions.");
        }

        /// <summary>
        /// Gets a nature definition by NatureID. Returns null if not found or not initialized.
        /// </summary>
        /// <param name="id">The unique identifier of the nature.</param>
        public static NatureDefinition Get(NatureID id)
        {
            if (natureDefinitionCache == null)
            {
                Log.Error(nameof(NatureDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return null;
            }

            natureDefinitionCache.TryGetValue(id, out var definition);
            return definition;
        }

        /// <summary>
        /// Releases Addressables handle and clears the definition cache.
        /// </summary>
        public static void Unload()
        {
            if (natureDefinitionCache == null)
            {
                return;
            }

            if (natureDefinitionHandle.IsValid())
            {
                Addressables.Release(natureDefinitionHandle);
            }

            natureDefinitionCache.Clear();
            natureDefinitionCache = null;

            Log.Info(nameof(NatureDefinitionLoader), "Nature definition cache unloaded.");
        }
    }
}
