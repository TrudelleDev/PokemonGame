using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Items.Storage
{
    [Serializable]
    public class BagPocket
    {
        [SerializeField] private List<Item> items;

        public IEnumerable<Item> Items => items;

        public void Add(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(item.Data.Name == items[i].Data.Name)
                {
                    items[i].Count += item.Count;
                    return;
                }             
            }

            items.Add(item);
        }

        public void Remove(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
