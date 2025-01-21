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
            throw new NotImplementedException();
        }

        public void Remove(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
