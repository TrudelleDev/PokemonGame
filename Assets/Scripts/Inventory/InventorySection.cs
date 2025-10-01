using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Represents a section in the inventory (e.g., General Items, Key Items, Poké Balls).
    /// Manages item storage, stacking, removal, and change notifications.
    /// </summary>
    [Serializable]
    public class InventorySection
    {
        [SerializeField, Required]
        [Tooltip("Initial items that seed this section when initialized.")]
        private List<Item> startingItems = new();

        private readonly List<Item> items = new();

        /// <summary>
        /// Gets a read-only view of the items currently in this section.
        /// </summary>
        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Raised whenever the items in this section are modified 
        /// (addition, removal, or clearing).
        /// </summary>
        public event Action OnItemsChanged;

        /// <summary>
        /// Initializes this section by clearing it and reloading its predefined starting items.
        /// </summary>
        public void Initialize()
        {
            Clear();

            foreach (Item item in startingItems)
            {
                if (IsValid(item))
                {
                    Add(new Item(item.Definition, item.Quantity));
                }
            }
        }

        /// <summary>
        /// Attempts to add an item to this section. If the item already exists,
        /// its stack quantity is increased (capped at 99).
        /// </summary>
        /// <param name="item">The item to add. Must be valid and have a quantity &gt; 0.</param>
        public void Add(Item item)
        {
            if (!IsValid(item))
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Item storedItem = items[i];

                if (storedItem.ID == item.ID)
                {
                    storedItem.Quantity = Mathf.Min(99, storedItem.Quantity + item.Quantity);
                    NotifyChanged();
                    return;
                }
            }

            items.Add(new Item(item.Definition, item.Quantity));
            NotifyChanged();
        }

        /// <summary>
        /// Removes a single unit of the given item. If the stack reaches zero,
        /// the item is removed entirely from the section.
        /// </summary>
        /// <param name="item">The item to remove. Must be valid.</param>
        public void Remove(Item item)
        {
            if (!IsValid(item))
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == item.ID)
                {
                    items[i].Quantity--;

                    if (items[i].Quantity <= 0)
                    {
                        items.RemoveAt(i);
                    }

                    NotifyChanged();
                    return;
                }
            }
        }

        /// <summary>
        /// Clears all items from this section.
        /// </summary>
        public void Clear()
        {
            items.Clear();
            NotifyChanged();
        }

        /// <summary>
        /// Checks whether the given item is valid for storage.
        /// </summary>
        private bool IsValid(Item item)
        {
            return item != null && item.ID != ItemId.None;
        }

        private void NotifyChanged() => OnItemsChanged?.Invoke();
    }
}
