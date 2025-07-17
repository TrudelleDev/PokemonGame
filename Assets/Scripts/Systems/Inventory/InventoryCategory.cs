using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Datas;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Represents a single inventory section that holds and stacks items of a specific type.
    /// </summary>
    [Serializable]
    public class InventoryCategory
    {
        [SerializeField]
        [Tooltip("Initial items for this category (used when initializing).")]
        private List<ItemStack> startingItems = new();

        private readonly List<Item> items = new();

        public IReadOnlyList<Item> Items => items;

        public event Action OnItemsChanged;

        /// <summary>
        /// Initializes this category by clearing all items and adding any starting items.
        /// </summary>
        public void Initialize()
        {
            Clear();

            foreach (ItemStack itemStack in startingItems)
            {
                if (itemStack.IsValid)
                    Add(itemStack.Data, itemStack.Quantity);
            }
        }

        /// <summary>
        /// Adds a quantity of an item to this category, stacking it if already present.
        /// </summary>
        /// <param name="itemData">The item data to add.</param>
        /// <param name="quantity">The quantity to add.</param>
        public void Add(ItemData itemData, int quantity)
        {
            if (itemData == null || quantity <= 0)
                return;

            foreach (Item existing in items)
            {
                if (existing.Data.ID == itemData.ID)
                {
                    existing.Count += quantity;
                    OnItemsChanged?.Invoke();
                    return;
                }
            }

            items.Add(new Item(itemData, quantity));
            OnItemsChanged?.Invoke();
        }

        /// <summary>
        /// Removes a quantity of an item by reference. If the remaining count is zero or less, removes the item entirely.
        /// </summary>
        /// <param name="item">The item and quantity to remove.</param>
        public void Remove(Item item)
        {
            if (item == null || item.Data == null || item.Count <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Data.ID == item.Data.ID)
                {
                    items[i].Count -= item.Count;

                    if (items[i].Count <= 0)
                        items.RemoveAt(i);

                    OnItemsChanged?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Removes a quantity of an item by ID. If the remaining count is zero or less, removes the item entirely.
        /// </summary>
        /// <param name="itemId">The item ID to remove.</param>
        /// <param name="quantity">The quantity to remove.</param>
        public void Remove(string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(itemId) || quantity <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Data.ID == itemId)
                {
                    items[i].Count -= quantity;

                    if (items[i].Count <= 0)
                        items.RemoveAt(i);

                    OnItemsChanged?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the quantity of the specified item ID currently in this category.
        /// </summary>
        /// <param name="itemId">The ID of the item to count.</param>
        /// <returns>The total quantity of the item.</returns>
        public int GetQuantity(string itemId)
        {
            foreach (var item in items)
            {
                if (item.Data.ID == itemId)
                    return item.Count;
            }

            return 0;
        }

        /// <summary>
        /// Checks whether this category contains at least one item with the specified ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to search for.</param>
        /// <returns>True if the item is found; otherwise, false.</returns>
        public bool Contains(string itemId)
        {
            return items.Exists(i => i.Data.ID == itemId);
        }

        /// <summary>
        /// Removes all items from this category.
        /// </summary>
        public void Clear()
        {
            items.Clear();
            OnItemsChanged?.Invoke();
        }
    }
}
