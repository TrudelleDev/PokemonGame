using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Definition;
using PokemonGame.Items.Enums;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Manages the player's inventory, organizing items into separate sections
    /// based on their type (e.g., regular items, key items, Poké Balls).
    /// Provides methods to add, remove, and retrieve items.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [Header("Inventory Sections")]

        [Tooltip("Regular items (e.g., Potions, Repels).")]
        [SerializeField] private InventoryCategory item;

        [Tooltip("Key items (e.g., Bike, Maps).")]
        [SerializeField] private InventoryCategory keyItem;

        [Tooltip("Poké Balls and similar items.")]
        [SerializeField] private InventoryCategory ball;


        private Dictionary<ItemCategory, InventoryCategory> categories;

        /// <summary>
        /// Initializes the inventory by binding each category and creating the internal lookup dictionary.
        /// This should be called before interacting with the inventory.
        /// </summary>
        public void Initialize()
        {
            item?.Initialize();
            keyItem?.Initialize();
            ball?.Initialize();

            categories = new Dictionary<ItemCategory, InventoryCategory>
            {
                { ItemCategory.General, item },
                { ItemCategory.KeyItem, keyItem },
                { ItemCategory.Pokeball, ball }
            };
        }

        /// <summary>
        /// Adds an item to the appropriate section of the inventory.
        /// </summary>
        /// <param name="item">The runtime item instance to add.</param>
        public void Add(Item item)
        {
            EnsureInitialized();

            if (item == null || item.ID == ItemId.None || item.Quantity <= 0)
            {
                Debug.LogWarning("[InventoryManager] Attempted to add invalid item.");
                return;
            }

            var def = item.Definition;
            if (def == null)
            {
                Debug.LogWarning($"[InventoryManager] Missing definition for ID '{item.ID}'. Did you load definitions?");
                return;
            }

            if (categories.TryGetValue(def.Category, out var section) && section != null)
            {
                section.Add(item); // if your InventoryCategory still works with stacks
            }
            else
            {
                Debug.LogWarning($"[InventoryManager] Unhandled item category '{def.Category}'.");
            }
        }

        /// <summary>
        /// Removes the specified item stack from its inventory category (by ItemId).
        /// If the stack's count reaches zero, the section should remove it internally.
        /// </summary>
        /// <param name="item">The runtime item instance to remove.</param>
        public void Remove(Item item)
        {
            EnsureInitialized();

            if (item == null) return;

            var id = item.ID;
            if (id == ItemId.None)
            {
                Log.Warning(nameof(InventoryManager), "Attempted to remove item with Unknown ID.");
                return;
            }

            var def = ItemDefinitionLoader.Get(id);
            if (def == null)
            {
                Log.Warning(nameof(InventoryManager), $"Missing ItemDefinition for ID '{id}'. Did you load item definitions?");
                return;
            }

            if (categories.TryGetValue(def.Category, out var section))
            {
                section.Remove(item);
            }
            else
            {
                Log.Warning(nameof(InventoryManager), $"Unhandled item type '{def.Category}'.");
            }
        }

        /// <summary>
        /// Retrieves the inventory section corresponding to a given item type.
        /// </summary>
        /// <param name="type">The type of item (e.g., Item, KeyItem, Pokeball).</param>
        /// <returns>The matching InventoryCategory, or null if not found.</returns>
        public InventoryCategory GetSection(ItemCategory type)
        {
            EnsureInitialized();
            return categories.TryGetValue(type, out var section) ? section : null;
        }

        /// <summary>
        /// Checks if the inventory contains at least one instance of an item by ID.
        /// </summary>
        /// <param name="itemId">The item ID to look for.</param>
        /// <param name="type">The item type section to check within.</param>
        /// <returns>True if found, otherwise false.</returns>
        public bool HasItem(ItemId itemId, ItemCategory type)
        {
            EnsureInitialized();
            var section = GetSection(type);
            return section != null && section.Contains(itemId);
        }

        private void EnsureInitialized()
        {
            if (categories == null)
                Initialize();
        }
    }
}
