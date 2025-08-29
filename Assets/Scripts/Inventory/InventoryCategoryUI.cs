using PokemonGame.Items;
using PokemonGame.Items.UI;
using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// UI controller that displays items from an <see cref="InventoryCategory"/>.
    /// Spawns item entries, listens for changes, and provides a cancel button.
    /// </summary>
    public class InventoryCategoryUI : MonoBehaviour, IInventoryCategoryBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Prefab used to display an item entry.")]
        private ItemUI itemUIPrefab;

        [SerializeField, Required]
        [Tooltip("Button prefab for cancelling and returning to the previous view.")]
        private Button cancelButton;

        [SerializeField, Required]
        [Tooltip("Parent container for item UI elements.")]
        private Transform contentParent;

        [SerializeField, Required]
        [Tooltip("Menu controller used to manage navigation between items.")]
        private VerticalMenuController menuController;

        private Button activeCancelButton;
        private InventoryCategory currentCategory;

        /// <summary>
        /// Binds the UI to a given inventory category and starts listening for changes.
        /// </summary>
        /// <param name="category">The inventory category to bind to.</param>
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
        /// Unbinds from the current inventory category and clears UI content.
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

        private void HandleCancelClick()
        {
            ViewManager.Instance.GoToPreviousView();
        }

        private void OnCategoryItemsChanged()
        {
            RefreshUI();
            menuController.RefreshButtons();
        }

        private void RefreshUI()
        {
            if (currentCategory == null)
                return;

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
