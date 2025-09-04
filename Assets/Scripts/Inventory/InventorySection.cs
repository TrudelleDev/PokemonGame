using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// A section in the inventory (e.g., Items, Key Items, Poké Balls).
    /// Holds item entries and manages stacking, removal, and lookups.
    /// </summary>
    [Serializable]
    public class InventorySection
    {
        [SerializeField]
        [Tooltip("Initial items for this section.")]
        private List<Item> startingItems = new();

        private readonly List<Item> items = new();

        /// <summary> 
        /// The items currently in this section.
        /// </summary>
        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Raised whenever items in this section change (addition or removal).
        /// </summary>
        public event Action OnItemsChanged;

        /// <summary>
        /// Initializes this section, clearing it and adding any predefined items.
        /// </summary>
        public void Initialize()
        {
            Clear();

            foreach (Item item in startingItems)
            {
                if (item != null && item.ID != ItemId.None)
                {
                    Add(new Item(item.Definition, item.Quantity));
                }
            }
        }

        /// <summary>
        /// Adds an item to this section, stacking it if already present.
        /// If the item already exists, its quantity is updated (up to a max of 99).
        /// </summary>
        /// <param name="item">The item to add. Must have a valid definition and quantity.</param>
        public void Add(Item item)
        {
            if (item == null || item.ID == ItemId.None)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Item storedItem = items[i];

                if (storedItem.ID == item.ID)
                {
                    storedItem.Quantity = Mathf.Min(99, storedItem.Quantity + item.Quantity); // cap at 99
                    OnItemsChanged?.Invoke();
                    return;
                }
            }

            items.Add(new Item(item.Definition, item.Quantity));
            OnItemsChanged?.Invoke();
        }

        /// <summary>
        /// Removes a quantity of an item from this section.
        /// If the quantity goes to zero, the item is removed.
        /// </summary>
        /// <param name="item">The item entry to remove, including the quantity to subtract.</param>
        public void Remove(Item item)
        {
            if (item == null || item.ID == ItemId.None)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == item.ID)
                {
                    items[i].Quantity -= item.Quantity;

                    if (items[i].Quantity <= 0)
                    {
                        items.RemoveAt(i);
                    }

                    OnItemsChanged?.Invoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the total quantity of a given item in this section.
        /// </summary>
        /// <param name="itemId">The ID of the item to check.</param>
        /// <returns>The quantity of the item, or 0 if not found.</returns>
        public int GetQuantity(ItemId itemId)
        {
            foreach (var item in items)
            {
                if (item.ID == itemId)
                {
                    return item.Quantity;
                }
            }

            return 0;
        }

        /// <summary>
        /// Checks whether this section contains an item with the specified ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to check for.</param>
        /// <returns>True if the item exists, otherwise false.</returns>
        public bool Contains(ItemId itemId)
        {
            foreach (var item in items)
            {
                if (item.ID == itemId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clears all items from this section.
        /// </summary>
        public void Clear()
        {
            items.Clear();
            OnItemsChanged?.Invoke();
        }
    }
}
