using PokemonGame.Items;
using PokemonGame.Items.UI;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Main inventory UI view. Displays all items in the player's inventory,
    /// manages item navigation, and handles the cancel/back button.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HorizontalPanelController))]
    public class InventoryView : View
    {
        [FoldoutGroup("Inventory Settings")]
        [SerializeField, Required]
        [Tooltip("Reference to the character's inventory.")]
        private InventoryManager inventory;

        [FoldoutGroup("Inventory Settings")]
        [SerializeField, Required]
        [Tooltip("Prefab for a single inventory item entry.")]
        private ItemUI inventoryItemPrefab;

        [FoldoutGroup("Inventory Settings")]
        [SerializeField, Required]
        [Tooltip("Prefab for the back button.")]
        private MenuButton cancelButtonPrefab;

        [FoldoutGroup("Inventory Settings")]
        [SerializeField, Required]
        [Tooltip("Parent container where all inventory items are instantiated.")]
        private Transform itemsContainer;

        [FoldoutGroup("Inventory Settings")]
        [SerializeField, Required]
        [Tooltip("Controls vertical navigation between inventory items.")]
        private VerticalMenuController menuController;


        private MenuButton cancelButton;

        /// <summary>
        /// Initializes the inventory view by binding to the inventory manager,
        /// subscribing to item change events, and populating the UI.
        /// </summary>
        public override void Preload()
        {
            Unbind();
            inventory.Initialize();
            inventory.OnItemsChanged += OnSectionChanged;
            OnSectionChanged();
        }

        /// <summary>
        /// Clears the current UI, unsubscribes from events,
        /// and removes the cancel button if it exists.
        /// </summary>
        public void Unbind()
        {
            if (cancelButton != null)
            {
                cancelButton.OnClick -= OnCancelButtonClick;
                Destroy(cancelButton.gameObject);
                cancelButton = null;
            }

            inventory.OnItemsChanged -= OnSectionChanged;

            ClearContent();
        }

        /// <summary>
        /// Disables navigation in this view.
        /// </summary>
        public override void Freeze()
        {
            menuController.enabled = false;
        }

        /// <summary>
        /// Re-enables navigation in this view.
        /// </summary>
        public override void Unfreeze()
        {
            menuController.enabled = true;
        }

        /// <summary>
        /// Removes all dynamically spawned item UI elements, leaving only
        /// the cancel button if it exists.
        /// </summary>
        private void ClearContent()
        {
            for (int i = itemsContainer.childCount - 1; i >= 0; i--)
            {
                GameObject child = itemsContainer.GetChild(i).gameObject;

                if (cancelButton != null && child == cancelButton.gameObject)
                {
                    continue;
                }

                Destroy(child);
            }
        }

        /// <summary>
        /// Refreshes the UI when the inventory changes:
        /// spawns ItemUI entries and ensures the cancel button is present.
        /// </summary>
        private void OnSectionChanged()
        {
            ClearContent();

            foreach (Item item in inventory.Items)
            {
                ItemUI itemUI = Instantiate(inventoryItemPrefab, itemsContainer);
                itemUI.Bind(item);
            }

            if (cancelButton == null)
            {
                cancelButton = Instantiate(cancelButtonPrefab, itemsContainer);
                cancelButton.OnClick += OnCancelButtonClick;
            }

            cancelButton.transform.SetAsLastSibling();
        }

        /// <summary>
        /// Closes this view when the cancel button is clicked.
        /// </summary>
        private void OnCancelButtonClick()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
