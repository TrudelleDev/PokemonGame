using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Manages the player’s inventory.
    /// Organizes items into sections and provides add, remove, and query operations.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Definition asset that seeds this inventory at startup.")]
        private InventoryDefinition inventory;

        private Dictionary<ItemCategory, InventorySection> sections;

        /// <summary>
        /// Initializes the inventory and prepares each section for use.
        /// Call this before interacting with the inventory.
        /// </summary>
        public void Initialize()
        {
            inventory.GeneralItems.Initialize();
            inventory.KeyItems.Initialize();
            inventory.Pokeballs.Initialize();

            sections = new()
            {
                [ItemCategory.General] = inventory.GeneralItems,
                [ItemCategory.KeyItem] = inventory.KeyItems,
                [ItemCategory.Pokeball] = inventory.Pokeballs
            };
        }

        /// <summary>
        /// Adds an item to the appropriate section of the inventory.
        /// Returns true if the item was successfully added.
        /// </summary>
        public bool Add(Item item)
        {
            if (item == null || item.ID == ItemId.None || item.Quantity <= 0 || item.Definition == null)
            {
                Log.Warning(nameof(InventoryManager), "Attempted to add invalid item.");
                return false;
            }

            if (sections.TryGetValue(item.Definition.Category, out InventorySection section))
            {
                section.Add(item);
                return true;
            }

            Log.Warning(nameof(InventoryManager), $"No section found for category '{item.Definition.Category}'.");
            return false;
        }

        /// <summary>
        /// Removes one unit of the given item from its section.
        /// Returns true if the item was successfully removed.
        /// </summary>
        public bool Remove(Item item)
        {
            if (item == null || item.ID == ItemId.None || item.Definition == null)
            {
                Log.Warning(nameof(InventoryManager), "Attempted to remove invalid item.");
                return false;
            }

            if (sections.TryGetValue(item.Definition.Category, out InventorySection section))
            {
                section.Remove(item);
                return true;
            }

            Log.Warning(nameof(InventoryManager), $"No section found for category '{item.Definition.Category}'.");
            return false;
        }

        /// <summary>
        /// Gets the inventory section for the specified category.
        /// Returns null if not found.
        /// </summary>
        public InventorySection GetSection(ItemCategory type)
        {
            sections.TryGetValue(type, out var section);
            return section;
        }
    }
}
