using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// A category in the inventory (e.g., Items, Key Items, Poké Balls).
    /// Holds item entries and manages stacking, removal, and lookups.
    /// </summary>
    [Serializable]
    public class InventoryCategory
    {
        [SerializeField, Required]
        [Tooltip("Initial items for this category (used at initialization).")]
        private List<Item> startingItems = new();

        private readonly List<Item> items = new();

        /// <summary> 
        /// The items currently in this category.
        /// </summary>
        public IReadOnlyList<Item> Items => items;

        /// <summary>
        ///  Raised whenever items in this category change.
        /// </summary>
        public event Action OnItemsChanged;

        /// <summary>
        /// Initializes this category, clearing and adding any predefined items.
        /// </summary>
        public void Initialize()
        {
            Clear();

            foreach (var item in startingItems)
            {
                if (item != null && item.ID != ItemId.None)
                {
                    Add(new Item(item.Definition, item.Quantity));
                }
            }
        }

        /// <summary>
        /// Adds an item to this category, stacking it if already present.
        /// </summary>
        /// <param name="item">The item to add. Must have a valid definition and quantity.</param>
        public void Add(Item item)
        {
            if (item == null || item.ID == ItemId.None)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                var existing = items[i];
                if (existing.ID == item.ID)
                {
                    existing.Quantity = Mathf.Min(99, existing.Quantity + item.Quantity); // cap at 99
                    OnItemsChanged?.Invoke();
                    return;
                }
            }

            items.Add(new Item(item.Definition, item.Quantity));
            OnItemsChanged?.Invoke();
        }

        /// <summary>
        /// Removes a quantity of an item by reference.
        /// </summary>
        /// <param name="item">The item entry to remove, including the quantity to subtract.</param>
        public void Remove(Item item)
        {
            if (item == null || item.ID == ItemId.None)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == item.ID)
                {
                    items[i].Quantity -= item.Quantity;

                    if (items[i].Quantity <= 0)
                        items.RemoveAt(i);

                    OnItemsChanged?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the total quantity of a given item.
        /// </summary>
        /// <param name="itemId">The ID of the item to check.</param>
        /// <returns>The quantity of the item, or 0 if not found.</returns>
        public int GetQuantity(ItemId itemId)
        {
            foreach (var item in items)
            {
                if (item.ID == itemId)
                    return item.Quantity;
            }

            return 0;
        }

        /// <summary>
        /// Checks whether this category contains an item.
        /// </summary>
        /// <param name="itemId">The ID of the item to look for.</param>
        /// <returns>True if the item exists, otherwise false.</returns>
        public bool Contains(ItemId itemId) => items.Exists(i => i.ID == itemId);

        /// <summary>
        /// Clears all items from this category.
        /// </summary>
        public void Clear()
        {
            items.Clear();
            OnItemsChanged?.Invoke();
        }
    }
}
