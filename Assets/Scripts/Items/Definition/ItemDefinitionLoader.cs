using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGame.Items.Enums;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Loads and caches all item definitions from Addressables using a shared label.
    /// </summary>
    public static class ItemDefinitionLoader
    {
        private const string AddressablesLabel = "item-definition";

        private static Dictionary<ItemId, ItemDefinition> itemDefinitionCache;
        private static AsyncOperationHandle<IList<ItemDefinition>> itemDefinitionHandle;

        /// <summary>
        /// The loaded item definitions, indexed by ID.
        /// </summary>
        public static IReadOnlyDictionary<ItemId, ItemDefinition> ItemDefinitionCache => itemDefinitionCache;

        /// <summary>
        /// Asynchronously loads all item definitions labeled "item-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (itemDefinitionCache != null)
            {
                return;
            }

            itemDefinitionCache = new Dictionary<ItemId, ItemDefinition>();
            itemDefinitionHandle = Addressables.LoadAssetsAsync<ItemDefinition>(AddressablesLabel, null);

            IList<ItemDefinition> definitions = await itemDefinitionHandle.Task;

            foreach (var definition in definitions)
            {
                ItemId key = definition.ItemId;

                if (!itemDefinitionCache.ContainsKey(key))
                {
                    itemDefinitionCache[key] = definition;
                }
                else
                {
                    Log.Warning(nameof(ItemDefinitionLoader),
                        $"Duplicate ItemID detected: {key} (existing: {itemDefinitionCache[key].name}, duplicate: {definition.name})");
                }
            }

            Log.Info(nameof(ItemDefinitionLoader),
                $"Loaded {itemDefinitionCache.Count}/{definitions.Count} item definitions.");
        }

        /// <summary>
        /// Gets an item definition by ID. Returns null if not found or not initialized.
        /// </summary>
        /// <param name="id">The item ID to retrieve.</param>
        public static ItemDefinition Get(ItemId id)
        {
            if (itemDefinitionCache == null)
            {
                Log.Error(nameof(ItemDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return null;
            }

            itemDefinitionCache.TryGetValue(id, out var definition);
            return definition;
        }

        /// <summary>
        /// Tries to get an item definition by ID.
        /// Returns true and sets <paramref name="definition"/> if found; otherwise false.
        /// Logs an error if the cache hasn't been initialized.
        /// </summary>
        public static bool TryGet(ItemId id, out ItemDefinition definition)
        {
            if (itemDefinitionCache == null)
            {
                Log.Error(nameof(ItemDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                definition = null;
                return false;
            }

            return itemDefinitionCache.TryGetValue(id, out definition);
        }

        /// <summary>
        /// Releases Addressables handle and clears the definition cache.
        /// </summary>
        public static void Unload()
        {
            if (itemDefinitionCache == null)
            {
                return;
            }

            if (itemDefinitionHandle.IsValid())
            {
                Addressables.Release(itemDefinitionHandle);
            }

            itemDefinitionCache.Clear();
            itemDefinitionCache = null;

            Log.Info(nameof(ItemDefinitionLoader), "Item definition cache unloaded.");
        }
    }
}
