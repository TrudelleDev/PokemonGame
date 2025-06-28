using System;
using System.Collections.Generic;
using PokemonGame.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Represents a single inventory section (or pocket) that holds and stacks items of a specific category.
    /// </summary>
    [Serializable]
    public class InventorySection
    {
        [SerializeField, Required] private List<Item> startingItems = new();

        private readonly List<Item> items = new();

        public IReadOnlyList<Item> Items => items;

        /// <summary>
        /// Initialize the inventory section by cloning and adding starting items.
        /// </summary>
        public void Initialize()
        {
            items.Clear();
            foreach (Item item in startingItems)
            {
                if (item != null)
                    Add(item.Clone());
            }
        }

        /// <summary>
        /// Adds an item to this section, stacking if an item with the same name exists.
        /// Ignores items with zero or negative count.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(Item item)
        {
            if (item == null || item.Data == null || item.Count <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Data.Name == item.Data.Name)
                {
                    items[i].Count += item.Count;
                    return;
                }
            }

            // Add new item if no stack found
            items.Add(item);
        }

        /// <summary>
        /// Removes a specified quantity of an item from this section.
        /// Removes the item entirely if count drops to zero or below.
        /// </summary>
        /// <param name="item">Item to remove (count indicates how many to remove).</param>
        public void Remove(Item item)
        {
            if (item == null || item.Data == null || item.Count <= 0)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Data.Name == item.Data.Name)
                {
                    items[i].Count -= item.Count;
                    if (items[i].Count <= 0)
                    {
                        items.RemoveAt(i);
                    }
                    return;
                }
            }
        }
    }
}
