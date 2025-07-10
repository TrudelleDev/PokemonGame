using System;
using System.Collections.Generic;
using PokemonGame.Items;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Represents a single inventory section that holds and stacks items of a specific type.
    /// </summary>
    [Serializable]
    public class InventoryCategory
    {
        [SerializeField] private List<Item> startingItems = new();
        private readonly List<Item> items = new();

        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Initializes this category by cloning the starting items.
        /// </summary>
        public void Initialize()
        {
            Clear();
            foreach (var item in startingItems)
            {
                if (item != null)
                    Add(item.Clone());
            }
        }

        /// <summary>
        /// Adds an item to this category, stacking if already present.
        /// </summary>
        public void Add(Item item)
        {
            if (item == null || item.Data == null || item.Count <= 0)
                return;

            foreach (var existing in items)
            {
                if (existing.Data.ID == item.Data.ID)
                {
                    existing.Count += item.Count;
                    return;
                }
            }

            items.Add(item);
        }

        /// <summary>
        /// Removes a quantity of an item from this category.
        /// </summary>
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
                    return;
                }
            }
        }

        /// <summary>
        /// Checks if the item with the given ID exists in the category.
        /// </summary>
        public bool Contains(string itemId)
        {
            return items.Exists(i => i.Data.ID == itemId);
        }


        /// <summary>
        /// Clears all items from this inventory category.
        /// </summary>
        public void Clear() => items.Clear();
    }
}
