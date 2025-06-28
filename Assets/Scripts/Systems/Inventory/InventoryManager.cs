using System;
using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Systems.Inventory.Groups;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Represents the player's inventory, divided into distinct pockets
    /// based on item type (e.g., items, key items, Poké Balls).
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventorySectionGroup sections;

        private Dictionary<ItemType, InventorySection> pockets;

        public InventorySectionGroup Sections => sections;

        public void Initialize()
        {
            sections.Item.Initialize();
            sections.KeyItem.Initialize();
            sections.Ball.Initialize();

            pockets = new Dictionary<ItemType, InventorySection>
            {
                { ItemType.Item, sections.Item},
                { ItemType.KeyItem, sections.KeyItem },
                { ItemType.Pokeball, sections.Ball }
            };
        }

        /// <summary>
        /// Adds an item to the appropriate pocket based on its type.
        /// </summary>
        /// <param name="item">The item to add to the inventory.</param>
        public void Add(Item item)
        {
            if (pockets.TryGetValue(item.Data.Type, out var section))
            {
                section.Add(item);
            }
            else
            {
                Debug.LogWarning($"Unhandled item type: {item.Data.Type}");
            }
        }

        /// <summary>
        /// Removes an item from the appropriate pocket based on its type.
        /// If the item's count reaches zero, it is removed from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(Item item)
        {
            if (pockets.TryGetValue(item.Data.Type, out var section))
            {
                section.Remove(item);
            }
            else
            {
                Debug.LogWarning($"Unhandled item type: {item.Data.Type}");
            }
        }

        /// <summary>
        /// Gets the inventory section for a given item type, or null if not found.
        /// </summary>
        public InventorySection GetSection(ItemType type)
        {
            return pockets.TryGetValue(type, out var section) ? section : null;
        }
    }
}
