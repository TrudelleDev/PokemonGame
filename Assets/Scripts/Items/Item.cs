using System;
using PokemonGame.Items.Definition;
using PokemonGame.Items.Enums;
using PokemonGame.Items.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Runtime stackable item that wraps an ItemStack and resolves its definition.
    /// </summary>
    [Serializable]
    public class Item
    {
        [SerializeField, Required]
        [Tooltip("The underlying stack data for this item.")]
        private ItemStack stack;

        /// <summary>
        /// The underlying stack data (ID + quantity).
        /// </summary>
        public ItemStack Stack => stack;

        /// <summary>
        /// The unique ID for this item.
        /// </summary>
        public ItemId ID => stack.ItemID;

        /// <summary>
        /// The current quantity of this item (clamped to >= 0).
        /// </summary>
        public int Quantity
        {
            get => stack.Quantity;
            set => stack = new ItemStack(ID, Mathf.Max(0, value));
        }

        /// <summary>
        /// The resolved item definition for this item, or null if not found.
        /// </summary>
        public ItemDefinition Definition
        {
            get
            {
                ItemDefinitionLoader.TryGet(ID, out var definition);
                return definition;
            }
        }

        /// <summary>
        /// Creates a new Item from the given stack.
        /// </summary>
        public Item(ItemStack stack)
        {
            this.stack = stack;
        }
    }
}
