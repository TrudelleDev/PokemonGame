using PokemonGame.Items.Enums;
using PokemonGame.Menu.Controllers;
using PokemonGame.Systems.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Displays the inventory UI and handles navigation between item categories.
    /// </summary>
    public class InventoryView : View
    {
        [Title("Categories")]

        [SerializeField, Required]
        [Tooltip("Category for regular items.")]
        private InventoryCategoryUI itemCategory;

        [SerializeField, Required]
        [Tooltip("Category for key items.")]
        private InventoryCategoryUI keyItemCategory;

        [SerializeField, Required]
        [Tooltip("Category for Poké Balls.")]
        private InventoryCategoryUI ballCategory;

        [Title("Inventory Manager")]

        [SerializeField, Required]
        [Tooltip("Inventory data manager.")]
        private InventoryManager inventoryManager;

        private HorizontalPanelController categoryController;

        /// <summary>
        /// Initializes inventory sections. Called once before first use.
        /// </summary>
        public override void Initialize()
        {
            inventoryManager.Initialize();

            itemCategory.Bind(inventoryManager.GetSection(ItemCategory.General));
            keyItemCategory.Bind(inventoryManager.GetSection(ItemCategory.KeyItem));
            ballCategory.Bind(inventoryManager.GetSection(ItemCategory.Pokeball));

            categoryController = GetComponent<HorizontalPanelController>();
        }

        /// <summary>
        /// Resets panel navigation when the view becomes active.
        /// </summary>
        private void OnEnable()
        {
            categoryController?.ResetController();
        }
    }
}
