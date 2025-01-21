using UnityEngine;
using System;

namespace PokemonGame.Items.Storage
{
    public class Bag : MonoBehaviour
    {
        // The Bag contains multiple pockets that represent a specific group of item.

        [SerializeField] private BagPocket itemPocket;      // Contains all items not in other pockets.
        [SerializeField] private BagPocket keyItemPocket;   // Contains all Key Items.
        [SerializeField] private BagPocket pokeBallPocket;  // Contains all Poké Balls.

        public BagPocket ItemPocket => itemPocket;
        public BagPocket KeyItemPocket => keyItemPocket;
        public BagPocket PokeBallPocket => pokeBallPocket;

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
