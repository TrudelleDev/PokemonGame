using PokemonGame.MenuControllers;
using PokemonGame.Systems.Inventory;
using UnityEngine;

namespace PokemonGame.Views
{
    public class InventoryView : View
    {
        [SerializeField] private InventorySectionUI item;
        [SerializeField] private InventorySectionUI keyItem;
        [SerializeField] private InventorySectionUI balls;
        [Space]
        [SerializeField] private InventoryManager inventory;
        [SerializeField] private VerticalMenuController controller;

        public override void Initialize()
        {
            inventory.Initialize();

            item.Bind(inventory.Sections.Item);
            keyItem.Bind(inventory.Sections.KeyItem);
            balls.Bind(inventory.Sections.Ball);

            controller.ClearAndRepopulate();
        }

        private void Start()
        {

        }
    }
}
