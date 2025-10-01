using PokemonGame.Items;
using PokemonGame.Items.UI;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Utilities;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// UI controller responsible for displaying all items from an
    /// <see cref="InventorySection"/>. Handles spawning <see cref="ItemUI"/> entries,
    /// listening for section changes, and providing a cancel button to
    /// return to the previous view.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventorySectionUI : MonoBehaviour
    {
        [Title("Prefabs")]
        [SerializeField, Required]
        [Tooltip("Prefab used to represent a single inventory item entry.")]
        private ItemUI itemUIPrefab;

        [SerializeField, Required]
        [Tooltip("Prefab for the cancel button, used to return to the previous view.")]
        private MenuButton cancelButtonPrefab;

        [SerializeField, Required]
        [Tooltip("Parent container where all item entries and the cancel button are instantiated.")]
        private Transform contentParent;

        [SerializeField, Required]
        [Tooltip("Menu controller that manages navigation between item entries.")]
        private VerticalMenuController menuController;

        private MenuButton cancelButton;
        private InventorySection currentSection;

        /// <summary>
        /// Binds this UI to a given <see cref="InventorySection"/>,
        /// subscribes to item change events, and populates the display.
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

            OnSectionChanged();
        }

        /// <summary>
        /// Unbinds from the current section, unsubscribes from events,
        /// clears UI content, and removes the cancel button.
        /// </summary>
        public void Unbind()
        {
            if (cancelButton != null)
            {
                cancelButton.OnClick -= OnCancelButtonClick;
                Destroy(cancelButton.gameObject);
                cancelButton = null;
            }

            if (currentSection != null)
            {
                currentSection.OnItemsChanged -= OnSectionChanged;
                currentSection = null;
            }

            ClearContent();
        }

        /// <summary>
        /// Temporarily disables navigation for this section.
        /// </summary>
        public void Freeze()
        {
            menuController.enabled = false;
        }

        /// <summary>
        /// Re-enables navigation for this section.
        /// </summary>
        public void UnFreeze()
        {
            menuController.enabled = true;
        }

        /// <summary>
        /// Removes all dynamically spawned UI elements,
        /// leaving only the cancel button (if present).
        /// </summary>
        private void ClearContent()
        {
            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                GameObject child = contentParent.GetChild(i).gameObject;

                if (cancelButton != null && child == cancelButton.gameObject)
                {
                    continue;
                }

                Destroy(child);
            }
        }

        /// <summary>
        /// Refreshes the UI by rebuilding the item list
        /// and ensuring the cancel button exists.
        /// </summary>
        private void OnSectionChanged()
        {
            if (currentSection == null)
            {
                return;
            }

            ClearContent();

            foreach (Item item in currentSection.Items)
            {
                ItemUI itemUI = Instantiate(itemUIPrefab, contentParent);
                itemUI.Bind(item);
            }

            if (cancelButton == null)
            {
                cancelButton = Instantiate(cancelButtonPrefab, contentParent);
                cancelButton.OnClick += OnCancelButtonClick;
            }

            cancelButton.transform.SetAsLastSibling();
        }

        /// <summary>
        /// Closes the current view when the cancel button is clicked.
        /// </summary>
        private void OnCancelButtonClick()
        {
            ViewManager.Instance.CloseCurrentView();
        }
    }
}
