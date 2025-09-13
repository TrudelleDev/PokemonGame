using System.Collections.Generic;
using PokemonGame.Items;
using PokemonGame.Items.Enums;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Central component that manages the character's inventory.
    /// Keeps items organized by section and provides add/remove/query operations.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Definition asset that seeds this inventory at startup.")]
        private InventoryDefinition inventory;

        private Dictionary<ItemCategory, InventorySection> sections;

        /// <summary>
        /// Initializes the inventory by binding each section and creating the internal lookup dictionary.
        /// This should be called before interacting with the inventory.
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
        /// Items are categorized by their item type (General, KeyItem, Pokeball).
        /// </summary>
        /// <param name="item">The item to add to the inventory.</param>
        public void Add(Item item)
        {
            if (item == null || item.ID == ItemId.None || item.Quantity <= 0)
            {
                Log.Warning(nameof(InventoryManager), "Attempted to add invalid item.");
                return;
            }

            if (item.Definition == null)
            {
                Log.Warning(nameof(InventoryManager), $"Missing definition for ID '{item.ID}'. Did you load definitions?");
                return;
            }

            if (sections.TryGetValue(item.Definition.Category, out var section) && section != null)
            {
                section.Add(item);
            }
            else
            {
                Log.Warning(nameof(InventoryManager), $"No inventory category found for '{item.Definition.Category}'.");
            }
        }

        /// <summary>
        /// Removes the specified item stack from its corresponding inventory section.
        /// </summary>
        /// <param name="item">The item to remove from the inventory.</param>
        public void Remove(Item item)
        {
            if (item == null || item.ID == ItemId.None)
            {
                Log.Warning(nameof(InventoryManager), "Attempted to remove invalid item.");
                return;
            }

            if (item.Definition == null)
            {
                Log.Warning(nameof(InventoryManager), $"Missing definition for ID '{item.ID}'. Did you load definitions?");
                return;
            }

            if (sections.TryGetValue(item.Definition.Category, out var section))
            {
                section.Remove(item);
            }
            else
            {
                Log.Warning(nameof(InventoryManager), $"No inventory category found for '{item.Definition.Category}'.");
            }
        }

        /// <summary>
        /// Retrieves the inventory section corresponding to the specified item category.
        /// </summary>
        /// <param name="type">The item category (e.g., General, KeyItem, Pokeball).</param>
        /// <returns>The inventory section for the specified item category, or null if not found.</returns>
        public InventorySection GetSection(ItemCategory type)
        {
            if (sections.TryGetValue(type, out var section))
            {
                return section;
            }

            return null;
        }

        /// <summary>
        /// Checks if the inventory contains at least one instance of an item by its ID and category.
        /// </summary>
        /// <param name="itemId">The item ID to check for.</param>
        /// <param name="type">The category of item to check in (e.g., General, KeyItem, Pokeball).</param>
        /// <returns>True if the item exists in the inventory, otherwise false.</returns>
        public bool HasItem(ItemId itemId, ItemCategory type)
        {
            InventorySection section = GetSection(type);

            if (section != null)
            {
                return section.Contains(itemId);
            }

            return false;
        }
    }
}
