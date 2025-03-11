using System;
using UnityEngine;

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
            switch (item.Data.Type)
            {
                case ItemType.Item:
                    itemPocket.Add(item);
                    break;
                case ItemType.KeyItem:
                    keyItemPocket.Add(item);
                    break;
                case ItemType.Pokeball:
                    pokeBallPocket.Add(item);
                    break;
            }
        }

        public void Remove(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
