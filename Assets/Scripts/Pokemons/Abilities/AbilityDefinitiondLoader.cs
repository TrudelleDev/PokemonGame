using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Pokemons.Abilities
{
    /// <summary>
    /// Loads and caches all ability definitions from Addressables using a shared label.
    /// </summary>
    public static class AbilityDefinitiondLoader
    {
        private const string AddressablesLabel = "ability-definition";

        private static Dictionary<AbilityID, AbilityDefinition> abilityDefinitionCache;
        private static AsyncOperationHandle<IList<AbilityDefinition>> abilityDefinitionHandle;

        /// <summary>
        /// The loaded ability definitions, indexed by ID.
        /// </summary>
        public static IReadOnlyDictionary<AbilityID, AbilityDefinition> AbilityDefinitionCache => abilityDefinitionCache;

        /// <summary>
        /// Asynchronously loads all ability definitions labeled "ability-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (abilityDefinitionCache != null)
            {
                return;
            }

            abilityDefinitionCache = new Dictionary<AbilityID, AbilityDefinition>();
            abilityDefinitionHandle = Addressables.LoadAssetsAsync<AbilityDefinition>(AddressablesLabel, null);

            IList<AbilityDefinition> definitions = await abilityDefinitionHandle.Task;

            foreach (var definition in definitions)
            {
                AbilityID key = definition.ID;

                if (!abilityDefinitionCache.ContainsKey(key))
                {
                    abilityDefinitionCache[key] = definition;
                }
                else
                {
                    Log.Warning(nameof(AbilityDefinitiondLoader),
                        $"Duplicate AbilityID detected: {key} (existing: {abilityDefinitionCache[key].name}, duplicate: {definition.name})");
                }
            }

            Log.Info(nameof(AbilityDefinitiondLoader),
                $"Loaded {abilityDefinitionCache.Count}/{definitions.Count} ability definitions.");
        }

        /// <summary>
        /// Gets an ability definition by ID. Returns null if not found or not initialized.
        /// </summary>
        /// <param name="id">The ability ID to retrieve.</param>
        public static AbilityDefinition Get(AbilityID id)
        {
            if (abilityDefinitionCache == null)
            {
                Log.Error(nameof(AbilityDefinitiondLoader), "Not initialized. Call LoadAllAsync() first.");
                return null;
            }

            abilityDefinitionCache.TryGetValue(id, out var definition);
            return definition;
        }

        /// <summary>
        /// Releases Addressables handle and clears the definition cache.
        /// </summary>
        public static void Unload()
        {
            if (abilityDefinitionCache == null)
            {
                return;
            }

            if (abilityDefinitionHandle.IsValid())
            {
                Addressables.Release(abilityDefinitionHandle);
            }

            abilityDefinitionCache.Clear();
            abilityDefinitionCache = null;

            Log.Info(nameof(AbilityDefinitiondLoader), "Ability definition cache unloaded.");
        }
    }
}
