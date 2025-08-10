using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGame.Abilities.Enums;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Abilities.Definition
{
    /// <summary>
    /// Loads and caches all ability definitions from Addressables using a shared label.
    /// </summary>
    public static class AbilityDefinitionLoader
    {
        private const string AddressablesLabel = "ability-definition";

        private static Dictionary<AbilityId, AbilityDefinition> abilityDefinitionCache;
        private static AsyncOperationHandle<IList<AbilityDefinition>> abilityDefinitionHandle;

        /// <summary>
        /// The loaded ability definitions, indexed by ID.
        /// </summary>
        public static IReadOnlyDictionary<AbilityId, AbilityDefinition> AbilityDefinitionCache => abilityDefinitionCache;

        /// <summary>
        /// Asynchronously loads all ability definitions labeled "ability-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (abilityDefinitionCache != null)
            {
                return;
            }

            abilityDefinitionCache = new Dictionary<AbilityId, AbilityDefinition>();
            abilityDefinitionHandle = Addressables.LoadAssetsAsync<AbilityDefinition>(AddressablesLabel, null);

            IList<AbilityDefinition> definitions = await abilityDefinitionHandle.Task;

            foreach (var definition in definitions)
            {
                AbilityId key = definition.ID;

                if (!abilityDefinitionCache.ContainsKey(key))
                {
                    abilityDefinitionCache[key] = definition;
                }
                else
                {
                    Log.Warning(nameof(AbilityDefinitionLoader),
                        $"Duplicate AbilityID detected: {key} (existing: {abilityDefinitionCache[key].name}, duplicate: {definition.name})");
                }
            }

            Log.Info(nameof(AbilityDefinitionLoader),
                $"Loaded {abilityDefinitionCache.Count}/{definitions.Count} ability definitions.");
        }

        /// <summary>
        /// Gets an ability definition by ID. Returns null if not found or not initialized.
        /// </summary>
        /// <param name="id">The ability ID to retrieve.</param>
        public static AbilityDefinition Get(AbilityId id)
        {
            if (abilityDefinitionCache == null)
            {
                Log.Error(nameof(AbilityDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return null;
            }

            abilityDefinitionCache.TryGetValue(id, out var definition);
            return definition;
        }

        /// <summary>
        /// Tries to get an ability definition by ID.
        /// Returns true if found; false otherwise (or if not initialized).
        /// </summary>
        public static bool TryGet(AbilityId id, out AbilityDefinition definition)
        {
            definition = null;

            if (abilityDefinitionCache == null)
            {
                Log.Error(nameof(AbilityDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return false;
            }

            if (abilityDefinitionCache.TryGetValue(id, out var def))
            {
                definition = def;
                return true;
            }

            return false;
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

            Log.Info(nameof(AbilityDefinitionLoader), "Ability definition cache unloaded.");
        }
    }
}
