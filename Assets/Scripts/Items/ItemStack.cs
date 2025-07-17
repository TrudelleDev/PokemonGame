using System;
using PokemonGame.Items.Datas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Represents a quantity of a specific item data used for initializing inventories or drops.
    /// </summary>
    [Serializable]
    public struct ItemStack
    {
        [SerializeField, Required]
        [Tooltip("Reference to the item data.")]
        private ItemData data;

        [OnValueChanged(nameof(ClampQuantity))]
        [SerializeField, MinValue(1)]
        [Tooltip("Number of items in this stack (minimum 1).")]
        private int quantity;

        public readonly ItemData Data => data;
        public readonly int Quantity => quantity;
        public readonly bool IsValid => data != null && quantity > 0;

        private void ClampQuantity()
        {
            if (quantity < 1)
                quantity = 1;
        }
    }
}
