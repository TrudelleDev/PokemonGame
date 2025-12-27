using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Manages a single inventory for a player or character.
    /// Handles item storage, addition, removal, and notifies when the inventory changes.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Initial items for this inventory.")]
        private InventoryDefinition inventoryDefinition;

        private readonly List<Item> items = new();

        /// <summary>
        /// Read-only view of all items currently in the inventory.
        /// </summary>
        internal IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Raised whenever the inventory changes (items added, removed, or cleared).
        /// </summary>
        internal event Action ItemsChanged;

        /// <summary>
        /// Initializes the inventory by clearing any existing items and loading
        /// the predefined starting items from the inventory definition.
        /// </summary>
        internal void Initialize()
        {
            Clear();

            if (inventoryDefinition == null || inventoryDefinition.Items == null)
            {
                Log.Warning(nameof(InventoryManager), "InventoryDefinition is missing or empty.");
                return;
            }

            foreach (Item item in inventoryDefinition.Items)
            {
                if (IsValid(item))
                {
                    Add(new Item(item.Definition, item.Quantity));
                }
            }
        }

        /// <summary>
        /// Adds an item to the inventory. If the item already exists,
        /// increases its stack quantity up to a maximum of 99.
        /// </summary>
        /// <param name="item">The item to add. Must be valid and have a positive quantity.</param>
        internal void Add(Item item)
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
        /// Removes a single unit of the specified item from the inventory.
        /// Removes the item entirely if the quantity reaches zero.
        /// </summary>
        /// <param name="item">The item to remove. Must be valid.</param>
        internal void Remove(Item item)
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
        /// Removes all items from the inventory.
        /// </summary>
        internal void Clear()
        {
            items.Clear();
            NotifyChanged();
        }

        private bool IsValid(Item item)
        {
            return item != null && item.ID != ItemId.None;
        }

        private void NotifyChanged()
        {
            ItemsChanged?.Invoke();
        }
    }
}
