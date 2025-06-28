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

        public ItemData Data => data;

        public int Count { get; set; }

        public Item(ItemData data, int count)
        {
            this.data = data;
            this.count = count;
        }

        /// <summary>
        /// Creates a copy of this item.
        /// </summary>
        public Item Clone() => new Item(data, count);
    }
}
