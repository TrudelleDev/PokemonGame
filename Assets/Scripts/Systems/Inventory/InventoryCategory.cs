using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using PokemonGame.Items.Models;
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

        /// <summary>
        /// The list of items currently in this category.
        /// </summary>
        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Event triggered whenever the items in this category change.
        /// </summary>
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
                {
                    Add(itemStack);
                }
            }
        }

        /// <summary>
        /// Adds a quantity of an item to this category, stacking it if already present.
        /// </summary>
        /// <param name="itemId">The item ID to add.</param>
        /// <param name="quantity">The quantity to add.</param>
        public void Add(ItemStack itemStack)
        {
            if (itemStack.ItemID == ItemId.None || itemStack.Quantity <= 0)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Item existing = items[i];
                if (existing.ID.Equals(itemStack.ItemID))
                {
                    existing.Quantity += itemStack.Quantity;
                    OnItemsChanged?.Invoke();
                    return;
                }
            }

            items.Add(new Item(itemStack));
            OnItemsChanged?.Invoke();
        }

        /// <summary>
        /// Removes a quantity of an item by reference.
        /// </summary>
        /// <param name="item">The item and quantity to remove.</param>
        public void Remove(Item item)
        {
            if (item == null || item.Definition == null || item.Quantity <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Definition.ItemId == item.Definition.ItemId)
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
        /// Removes a quantity of an item by enum ID.
        /// </summary>
        /// <param name="itemId">The enum ID of the item to remove.</param>
        /// <param name="quantity">The quantity to remove.</param>
        public void Remove(ItemId itemId, int quantity)
        {
            if (itemId == ItemId.None || quantity <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Definition.ItemId == itemId)
                {
                    items[i].Quantity -= quantity;

                    if (items[i].Quantity <= 0)
                        items.RemoveAt(i);

                    OnItemsChanged?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the quantity of the specified item ID currently in this category.
        /// </summary>
        public int GetQuantity(ItemId itemId)
        {
            foreach (var item in items)
            {
                if (item.Definition.ItemId == itemId)
                    return item.Quantity;
            }

            return 0;
        }

        /// <summary>
        /// Checks whether this category contains at least one item with the specified ID.
        /// </summary>
        public bool Contains(ItemId itemId)
        {
            return items.Exists(i => i.Definition.ItemId == itemId);
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
