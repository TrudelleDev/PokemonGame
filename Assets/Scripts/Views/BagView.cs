using PokemonGame.Items.Storage;
using PokemonGame.Items.Storage.UI;
using UnityEngine;

namespace PokemonGame.Views
{
    public class BagView : View
    {
        [SerializeField] private BagPocketContent itemPocket;
        [SerializeField] private BagPocketContent keyItemPocket;
        [SerializeField] private BagPocketContent pokeBallPocket;
        [Space]
        [SerializeField] private Bag bag;

        public override void Initialize()
        {
            itemPocket.CreateItemList(bag.ItemPocket);
            keyItemPocket.CreateItemList(bag.KeyItemPocket);
            pokeBallPocket.CreateItemList(bag.PokeBallPocket);
        }
    }
}
