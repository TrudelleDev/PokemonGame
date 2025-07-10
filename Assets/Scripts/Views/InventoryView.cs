using PokemonGame.MenuControllers;
using PokemonGame.Systems.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Shows item categories and handles inventory panel navigation.
    /// </summary>
    public class InventoryView : View
    {
        [SerializeField, Required]
        [FoldoutGroup("Categories")]
        [Tooltip("Section for regular items.")]
        private InventoryCategoryUI itemCategory;

        [SerializeField, Required]
        [FoldoutGroup("Categories")]
        [Tooltip("Section for key items.")]
        private InventoryCategoryUI keyItemCategory;

        [SerializeField, Required]
        [FoldoutGroup("Categories")]
        [Tooltip("Section for Poké Balls.")]
        private InventoryCategoryUI ballCategory;

        [SerializeField, Required]
        [Tooltip("Inventory data manager.")]
        private InventoryManager inventoryManager;

        private HorizontalPanelController categoryController;

        private void Awake()
        {
            categoryController = GetComponent<HorizontalPanelController>();
        }

        /// <summary>
        /// Initializes inventory sections. Called once before first use.
        /// </summary>
        public override void Initialize()
        {
            inventoryManager.Initialize();

            itemCategory.Bind(inventoryManager.GetSection(Items.ItemType.Item));
            keyItemCategory.Bind(inventoryManager.GetSection(Items.ItemType.KeyItem));
            ballCategory.Bind(inventoryManager.GetSection(Items.ItemType.Pokeball));
        }

        /// <summary>
        /// Resets panel navigation when view becomes active.
        /// </summary>
        private void OnEnable()
        {
            categoryController.ResetController();
        }
    }
}
