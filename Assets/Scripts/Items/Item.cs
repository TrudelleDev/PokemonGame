using System;
using PokemonGame.Items.Definition;
using PokemonGame.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Represents a runtime item stack in the player's inventory or world.
    /// Each item references a static <see cref="ItemDefinition"/>
    /// and maintains a mutable quantity that can be increased or decreased at runtime.
    /// </summary>
    [Serializable]
    public class Item
    {
        [SerializeField, Required, Tooltip("Reference to the item's static definition.")]
        private ItemDefinition definition;

        [SerializeField, Required, Range(1, 99), Tooltip("Number of items in this stack.")]
        private int quantity = 1;

        /// <summary>
        /// Gets the static definition for this item (metadata such as name, category, and icon).
        /// </summary>
        public ItemDefinition Definition => definition;

        /// <summary>
        /// Gets the unique ID of this item, provided by its definition.
        /// Returns <see cref="ItemId.None"/> if the definition is missing.
        /// </summary>
        public ItemId ID => definition != null ? definition.ItemId : ItemId.None;

        /// <summary>
        /// Gets or sets the current quantity of this item stack.
        /// The value is always clamped to be non-negative.
        /// </summary>
        public int Quantity
        {
            get => quantity;
            set => quantity = Mathf.Max(0, value);
        }

        /// <summary>
        /// Creates a new item stack instance at runtime.
        /// </summary>
        /// <param name="definition">The static definition of the item.</param>
        /// <param name="quantity">The number of items in this stack.</param>
        public Item(ItemDefinition definition, int quantity = 1)
        {
            this.definition = definition;
            this.quantity = Mathf.Max(0, quantity);
        }
    }
}
