using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using PokemonGame.Utilities;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Manages a single inventory for a player or character.
    /// Handles item storage, addition, removal, and notifies when the inventory changes.
    /// </summary>
    public sealed class InventoryManager
    {
        private readonly List<Item> items = new();

        /// <summary>
        /// Read-only view of all items currently in the inventory.
        /// </summary>
        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Raised whenever the inventory changes (items added, removed, or cleared).
        /// </summary>
        public event Action ItemsChanged;

        /// <summary>
        /// Predefined starting items for this inventory (optional).
        /// </summary>
        public InventoryDefinition InventoryDefinition { get; private set; }

        /// <summary>
        /// Creates a new inventory manager with an optional starting definition.
        /// </summary>
        /// <param name="definition">Optional initial inventory definition.</param>
        public InventoryManager(InventoryDefinition definition = null)
        {
            InventoryDefinition = definition;
            Initialize();
        }

        /// <summary>
        /// Initializes the inventory by clearing any existing items and loading
        /// the predefined starting items from the inventory definition.
        /// </summary>
        public void Initialize()
        {
            Clear();

            if (InventoryDefinition == null || InventoryDefinition.Items == null)
            {
                Log.Warning(nameof(InventoryManager), "InventoryDefinition is missing or empty.");
                return;
            }

            foreach (Item item in InventoryDefinition.Items)
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
                    storedItem.Quantity = Math.Min(99, storedItem.Quantity + item.Quantity);
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
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear()
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
