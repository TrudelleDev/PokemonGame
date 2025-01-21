using System;
using PokemonGame.Items.Datas;
using UnityEngine;

namespace PokemonGame.Items
{
    [Serializable]
    public class Item
    {
        [SerializeField] private ItemData data;
        [SerializeField] private int count;

        public ItemData Data => data;

        public int Count
        {
            get => count;
            set => count = value;
        }
    }
}
