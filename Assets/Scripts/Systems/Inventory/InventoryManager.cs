using System.Collections.Generic;
using PokemonGame.Items;
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

        /// <summary>
        /// Internal dictionary mapping item types to their corresponding inventory sections.
        /// </summary>
        private Dictionary<ItemType, InventoryCategory> categories;

        /// <summary>
        /// Initializes the inventory by binding each category and creating the internal lookup dictionary.
        /// This should be called before interacting with the inventory.
        /// </summary>
        public void Initialize()
        {
            item.Initialize();
            keyItem.Initialize();
            ball.Initialize();

            categories = new Dictionary<ItemType, InventoryCategory>
            {
                { ItemType.Item, item },
                { ItemType.KeyItem, keyItem },
                { ItemType.Pokeball, ball }
            };
        }

        /// <summary>
        /// Adds an item to the appropriate inventory section based on its item type.
        /// Stacks the item if an equivalent one already exists.
        /// </summary>
        /// <param name="item">The item to add to the inventory.</param>
        public void Add(Item item)
        {
            EnsureInitialized();

            if (categories.TryGetValue(item.Data.Type, out var section))
            {
                section.Add(item);
            }
            else
            {
                Debug.LogWarning($"Unhandled item type: {item.Data.Type}");
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

            if (categories.TryGetValue(item.Data.Type, out var section))
            {
                section.Remove(item);
            }
            else
            {
                Debug.LogWarning($"Unhandled item type: {item.Data.Type}");
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

        private void EnsureInitialized()
        {
            if (categories == null)
                Initialize();
        }
    }
}
