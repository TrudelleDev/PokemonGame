using PokemonGame.Items.Enums;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Main inventory view. Displays item categories (General, Key Items, Poké Balls)
    /// and coordinates navigation between them.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HorizontalPanelController))]
    public class InventoryView : View
    {
        [Title("Inventory")]
        [SerializeField, Required]
        [Tooltip("Inventory manager responsible for loading and tracking items.")]
        private InventoryManager inventoryManager;

        [Title("Sections")]
        [SerializeField, Required]
        [Tooltip("UI section displaying general items.")]
        private InventorySectionUI generalItemSection;

        [SerializeField, Required]
        [Tooltip("UI section displaying key items.")]
        private InventorySectionUI keyItemSection;

        [SerializeField, Required]
        [Tooltip("UI section displaying Poké Balls.")]
        private InventorySectionUI ballSection;

        private HorizontalPanelController sectionController;

        private void Awake()
        {
            sectionController = GetComponent<HorizontalPanelController>();
        }

        private void OnEnable()
        {
            sectionController.ResetController();
        }

        /// <summary>
        /// Initializes the inventory and binds all sections.
        /// </summary>
        public override void Preload()
        {
            inventoryManager.Initialize();

            generalItemSection.Bind(inventoryManager.GetSection(ItemCategory.General));
            keyItemSection.Bind(inventoryManager.GetSection(ItemCategory.KeyItem));
            ballSection.Bind(inventoryManager.GetSection(ItemCategory.Pokeball));
        }

        /// <summary>
        /// Disables navigation and freezes all sections.
        /// </summary>
        public override void Freeze()
        {
            sectionController.enabled = false;

            generalItemSection.Freeze();
            keyItemSection.Freeze();
            ballSection.Freeze();
        }

        /// <summary>
        /// Re-enables navigation and unfreezes all sections.
        /// </summary>
        public override void Unfreeze()
        {
            sectionController.enabled = true;

            generalItemSection.UnFreeze();
            keyItemSection.UnFreeze();
            ballSection.UnFreeze();
        }
    }
}
