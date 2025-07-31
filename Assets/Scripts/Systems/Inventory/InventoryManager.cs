using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Datas;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
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


        private Dictionary<ItemType, InventoryCategory> categories;

        /// <summary>
        /// Initializes the inventory by binding each category and creating the internal lookup dictionary.
        /// This should be called before interacting with the inventory.
        /// </summary>
        public void Initialize()
        {
            item?.Initialize();
            keyItem?.Initialize();
            ball?.Initialize();

            categories = new Dictionary<ItemType, InventoryCategory>
            {
                { ItemType.Item, item },
                { ItemType.KeyItem, keyItem },
                { ItemType.Pokeball, ball }
            };
        }

        /// <summary>
        /// Adds a quantity of an item to the appropriate inventory section based on its item type.
        /// If an equivalent item already exists, the quantity is stacked.
        /// </summary>
        /// <param name="itemData">The item data to add to the inventory.</param>
        /// <param name="quantity">The quantity of the item to add.</param>
        public void Add(ItemData itemData, int quantity)
        {
            EnsureInitialized();

            if (itemData == null || quantity <= 0)
                return;

            if (categories.TryGetValue(itemData.Type, out var section))
            {
                section.Add(itemData, quantity);
            }
            else
            {
                Log.Warning(nameof(InventoryManager), $"Unhandled item type {itemData.Type}");
            }
        }

        /// <summary>
        /// Removes the specified quantity of an item from the appropriate inventory section.
        /// If the item's count reaches zero, it is removed from the section.
        /// </summary>
        /// <param name="item">The item to remove from the inventory.</param>
        public void Remove(Item item)
        {
            EnsureInitialized();

            if (item == null || item.Data == null)
                return;

            if (categories.TryGetValue(item.Data.Type, out var section))
            {
                section.Remove(item);
            }
            else
            {
                Log.Warning(nameof(InventoryManager), $" Unhandled item type '{item.Data.Type}");
            }
        }

        /// <summary>
        /// Retrieves the inventory section corresponding to a given item type.
        /// </summary>
        /// <param name="type">The type of item (e.g., Item, KeyItem, Pokeball).</param>
        /// <returns>The matching InventoryCategory, or null if not found.</returns>
        public InventoryCategory GetSection(ItemType type)
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
        public bool HasItem(string itemId, ItemType type)
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
