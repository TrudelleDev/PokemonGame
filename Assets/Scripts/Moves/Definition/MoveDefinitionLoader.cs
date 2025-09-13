using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGame.Moves.Enums;
using PokemonGame.Utilities;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PokemonGame.Moves.Definition
{
    /// <summary>
    /// Loads and caches all move definitions from Addressables using a shared label.
    /// Cached lookups are keyed by MoveID.
    /// </summary>
    public static class MoveDefinitionLoader
    {
        private const string AddressablesLabel = "move-definition";

        private static Dictionary<MoveId, MoveDefinition> moveDefinitionCache;
        private static AsyncOperationHandle<IList<MoveDefinition>> moveDefinitionHandle;

        /// <summary>
        /// The loaded move definitions indexed by MoveID.
        /// </summary>
        public static IReadOnlyDictionary<MoveId, MoveDefinition> MoveDefinitionCache => moveDefinitionCache;

        /// <summary>
        /// Asynchronously loads all move definitions labeled "move-definition" from Addressables.
        /// </summary>
        public static async Task LoadAllAsync()
        {
            if (moveDefinitionCache != null)
            {
                return;
            }

            moveDefinitionCache = new Dictionary<MoveId, MoveDefinition>();
            moveDefinitionHandle = Addressables.LoadAssetsAsync<MoveDefinition>(AddressablesLabel, null);

            IList<MoveDefinition> definitions = await moveDefinitionHandle.Task;

            foreach (MoveDefinition definition in definitions)
            {
                if (definition == null)
                {
                    Log.Warning(nameof(MoveDefinitionLoader), "Null MoveDefinition encountered; skipping.");
                    continue;
                }

                var key = definition.ID;

                if (!moveDefinitionCache.ContainsKey(key))
                {
                    moveDefinitionCache[key] = definition;
                }
                else
                {
                    Log.Warning(nameof(MoveDefinitionLoader),
                        $"Duplicate MoveID detected: {key} (existing: {moveDefinitionCache[key].name}, duplicate: {definition.name})");
                }
            }

            Log.Info(nameof(MoveDefinitionLoader),
                $"Loaded {moveDefinitionCache.Count}/{definitions.Count} move definitions.");
        }

        /// <summary>
        /// Gets a move definition by MoveID. Returns null if not found or not initialized.
        /// </summary>
        /// <param name="id">The unique identifier of the move.</param>
        public static MoveDefinition Get(MoveId id)
        {
            if (moveDefinitionCache == null)
            {
                Log.Error(nameof(MoveDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return null;
            }

            moveDefinitionCache.TryGetValue(id, out var definition);
            return definition;
        }

        /// <summary>
        /// Tries to get a move definition by MoveID.
        /// Returns true if found; false otherwise (or if not initialized).
        /// </summary>
        public static bool TryGet(MoveId id, out MoveDefinition definition)
        {
            definition = null;

            if (moveDefinitionCache == null)
            {
                Log.Error(nameof(MoveDefinitionLoader), "Not initialized. Call LoadAllAsync() first.");
                return false;
            }

            if (moveDefinitionCache.TryGetValue(id, out var def))
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
            if (moveDefinitionCache == null)
            {
                return;
            }

            if (moveDefinitionHandle.IsValid())
            {
                Addressables.Release(moveDefinitionHandle);
            }

            moveDefinitionCache.Clear();
            moveDefinitionCache = null;

            Log.Info(nameof(MoveDefinitionLoader), "Move definition cache unloaded.");
        }
    }
}
