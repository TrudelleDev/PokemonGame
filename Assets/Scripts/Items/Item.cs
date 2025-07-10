using System;
using PokemonGame.Items.Datas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Represents a stackable item instance with its associated data and quantity.
    /// </summary>
    [Serializable]
    public class Item
    {
        [SerializeField, Required] private ItemData data;
        [SerializeField, Required] private int count;

        /// <summary>
        /// The data reference for this item.
        /// </summary>
        public ItemData Data => data;

        /// <summary>
        /// The current quantity of this item instance.
        /// </summary>
        public int Count
        {
            get => count;
            set => count = Mathf.Max(0, value); // prevent negative count
        }

        public Item(ItemData data, int count)
        {
            this.data = data;
            Count = count;
        }

        /// <summary>
        /// Creates a copy of this item.
        /// </summary>
        public Item Clone() => new Item(data, count);
    }
}
