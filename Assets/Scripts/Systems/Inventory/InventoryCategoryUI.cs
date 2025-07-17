using PokemonGame.Items.UI;
using PokemonGame.Shared;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

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
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("Parent transform that holds all item UI elements.")]
        private Transform contentParent;

        private MenuButton activeCancelButton;
        private InventoryCategory currentCategory;

        /// <summary>
        /// Binds the UI to a specific inventory category and listens for updates.
        /// </summary>
        /// <param name="category">The inventory category to display.</param>
        public void Bind(InventoryCategory category)
        {
            Unbind();

            if (category == null)
            {
                Log.Warning(this, $"Tried to bind a null category.");
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
                activeCancelButton.OnClick -= HandleCancelClick;
                Destroy(activeCancelButton.gameObject);
                activeCancelButton = null;
            }

            if (currentCategory != null)
            {
                currentCategory.OnItemsChanged -= OnCategoryItemsChanged;
                currentCategory = null;
            }

            ClearContent();
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
        }

        /// <summary>
        /// Rebuilds the item list and cancel button UI from the bound inventory category.
        /// </summary>
        private void RefreshUI()
        {
            ClearContent();

            if (currentCategory == null || itemUIPrefab == null || cancelButton == null)
                return;

            foreach (var item in currentCategory.Items)
            {
                ItemUI itemUI = Instantiate(itemUIPrefab, contentParent);
                itemUI.Bind(item);
            }

            if (activeCancelButton == null)
            {
                activeCancelButton = Instantiate(cancelButton, contentParent);
                activeCancelButton.OnClick += HandleCancelClick;
            }

            activeCancelButton.transform.SetAsLastSibling();
        }

        /// <summary>
        /// Destroys all child UI elements under the content container except for persistent cancel button.
        /// </summary>
        private void ClearContent()
        {
            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                GameObject child = contentParent.GetChild(i).gameObject;
                if (activeCancelButton != null && child == activeCancelButton.gameObject)
                    continue;

                Destroy(child);
            }
        }
    }
}
