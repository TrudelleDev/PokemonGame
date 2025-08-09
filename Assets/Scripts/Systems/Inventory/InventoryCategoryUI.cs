using PokemonGame.Items;
using PokemonGame.Items.UI;
using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Displays a visual list of items from an InventoryCategory.
    /// Includes a cancel button to return to the previous view.
    /// </summary>
    public class InventoryCategoryUI : MonoBehaviour, IInventoryCategoryBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Prefab for displaying an individual item.")]
        private ItemUI itemUIPrefab;

        [SerializeField, Required]
        [Tooltip("Button used to cancel and return to the previous view.")]
        private Button cancelButton;

        [SerializeField, Required]
        [Tooltip("Parent transform that holds all item UI elements.")]
        private Transform contentParent;

        [SerializeField, Required]
        private VerticalMenuController menuController;

        private Button activeCancelButton;
        private InventoryCategory currentCategory;

        /// <summary>
        /// Binds the UI to a specific inventory category and listens for updates.
        /// </summary>
        public void Bind(InventoryCategory category)
        {
            Unbind();

            if (category == null)
            {
                Log.Warning(nameof(InventoryCategoryUI), "Tried to bind a null category.");
                return;
            }

            currentCategory = category;
            currentCategory.OnItemsChanged += OnCategoryItemsChanged;

            RefreshUI();
        }

        /// <summary>
        /// Clears the UI and unsubscribes from events to clean up references.
        /// </summary>
        public void Unbind()
        {
            if (activeCancelButton != null)
            {
                activeCancelButton.onClick.RemoveListener(HandleCancelClick);
                Destroy(activeCancelButton.gameObject);
                activeCancelButton = null;
            }

            if (currentCategory != null)
            {
                currentCategory.OnItemsChanged -= OnCategoryItemsChanged;
                currentCategory = null;
            }

            ClearContent();
            menuController.RefreshButtons();
        }

        /// <summary>
        /// Handles the cancel button click and navigates to the previous view.
        /// </summary>
        private void HandleCancelClick()
        {
            ViewManager.Instance.GoToPreviousView();
        }

        /// <summary>
        /// Called when the inventory category changes. Refreshes the item list UI.
        /// </summary>
        private void OnCategoryItemsChanged()
        {
            RefreshUI();
            menuController.RefreshButtons();
        }

        /// <summary>
        /// Rebuilds the item list and cancel button UI from the bound inventory category.
        /// </summary>
        private void RefreshUI()
        {
            if (currentCategory == null)
            {
                return;
            }

            ClearContent();

            for (int i = 0; i < currentCategory.Items.Count; i++)
            {
                Item item = currentCategory.Items[i];
                ItemUI itemUI = Instantiate(itemUIPrefab, contentParent);
                itemUI.Bind(item);
            }

            if (activeCancelButton == null)
            {
                activeCancelButton = Instantiate(cancelButton, contentParent);
                activeCancelButton.onClick.AddListener(HandleCancelClick);
            }

            activeCancelButton.transform.SetAsLastSibling();
        }

        /// <summary>
        /// Destroys all child UI elements under the content container except the persistent cancel button.
        /// </summary>
        private void ClearContent()
        {
            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                GameObject child = contentParent.GetChild(i).gameObject;
                if (activeCancelButton != null && child == activeCancelButton.gameObject)
                {
                    continue;
                }

                Destroy(child);
            }
        }
    }
}
