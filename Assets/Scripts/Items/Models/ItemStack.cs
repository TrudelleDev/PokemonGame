using System;
using PokemonGame.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Models
{
    /// <summary>
    /// Represents a quantity of a specific item (by ID).
    /// </summary>
    [Serializable]
    public struct ItemStack
    {
        [SerializeField, Required]
        [Tooltip("The ID of the item in this stack.")]
        private ItemId itemId;

        [SerializeField, Required, MinValue(1)]
        [Tooltip("Number of items in this stack (minimum 1).")]
        private int quantity;

        /// <summary>
        /// The unique ID of the item.
        /// </summary>
        public readonly ItemId ItemID => itemId;

        /// <summary>
        /// The quantity of the item in this stack.
        /// </summary>
        public readonly int Quantity => quantity;

        /// <summary>
        /// True when the ID is not None and quantity is positive.
        /// </summary>
        public readonly bool IsValid => itemId != ItemId.None && quantity > 0;

        public ItemStack(ItemId id, int quantity)
        {
            this.itemId = id;
            this.quantity = Mathf.Max(1, quantity);
        }
    }
}
