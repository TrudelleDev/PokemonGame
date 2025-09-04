using PokemonGame.Items.UI;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// UI controller that displays items from an <see cref="InventorySection"/>.
    /// Spawns item entries, listens for changes, and provides a cancel button to return to the previous view.
    /// </summary>
    public class InventorySectionUI : MonoBehaviour
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
        private InventorySection currentSection;

        /// <summary>
        /// Binds the UI to a given inventory section and starts listening for item changes.
        /// </summary>
        /// <param name="section">The inventory section to bind to.</param>
        public void Bind(InventorySection section)
        {
            Unbind();

            if (section == null)
            {
                Log.Warning(nameof(InventorySectionUI), "Attempted to bind a null section.");
                return;
            }

            currentSection = section;
            currentSection.OnItemsChanged += OnSectionChanged;

            RefreshUI();
        }

        /// <summary>
        /// Unbinds from the current inventory section and clears the UI content.
        /// </summary>
        public void Unbind()
        {
            // Remove cancel button listener and destroy it if it's active
            if (activeCancelButton != null)
            {
                activeCancelButton.onClick.RemoveListener(HandleCancelClick);
                Destroy(activeCancelButton.gameObject);
                activeCancelButton = null;
            }

            // Unsubscribe from the section's item changes event and reset category
            if (currentSection != null)
            {
                currentSection.OnItemsChanged -= OnSectionChanged;
                currentSection = null;
            }

            // Clear UI content and refresh the menu buttons
            ClearContent();
            menuController.RefreshButtons();
        }
   
        /// <summary>
        /// Refreshes the UI to display the current items in the inventory section.
        /// </summary>
        private void RefreshUI()
        {
            if (currentSection == null)
            {
                return;
            }

            ClearContent();

            // Instantiate and bind ItemUI for each item in the section
            foreach (var item in currentSection.Items)
            {
                ItemUI itemUI = Instantiate(itemUIPrefab, contentParent);
                itemUI.Bind(item);
            }

            // Instantiate cancel button if not already active
            if (activeCancelButton == null)
            {
                activeCancelButton = Instantiate(cancelButton, contentParent);
                activeCancelButton.onClick.AddListener(HandleCancelClick);
            }

            // Ensure the cancel button is last in the hierarchy to appear at the bottom
            activeCancelButton.transform.SetAsLastSibling();
        }

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

        private void HandleCancelClick()
        {
            ViewManager.Instance.GoToPreviousView();
        }

        private void OnSectionChanged()
        {
            RefreshUI();
            menuController.RefreshButtons();
        }
    }
}
