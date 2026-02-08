using System;
using System.Collections.Generic;
using MonsterTamer.Items;
using MonsterTamer.Items.UI;
using MonsterTamer.Shared.Interfaces;
using MonsterTamer.Shared.UI.MenuButtons;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Inventory.UI
{
    /// <summary>
    /// View responsible for displaying the player's inventory.
    /// Handles item list population, highlighting, submitting, and canceling.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryView : View
    {
        [SerializeField, Required, Tooltip("Prefab used for each inventory item in the UI.")]
        private ItemUI inventoryItemPrefab;

        [SerializeField, Required, Tooltip("Prefab for the cancel button at the bottom of the inventory list.")]
        private CancelMenuButton cancelButtonPrefab;

        [SerializeField, Required, Tooltip("Parent transform that holds all item UI elements.")]
        private Transform itemsContainer;

        /// <summary>
        /// Raised when an item or option is highlighted in the inventory UI.
        /// </summary>
        public event Action<IDisplayable> OptionHighlighted;

        /// <summary>
        /// Raised when an item or option is submitted (selected) in the inventory UI.
        /// </summary>
        public event Action<IDisplayable> OptionSubmitted;

        /// <summary>
        /// Raised when the cancel button or cancel key is pressed in the inventory UI.
        /// </summary>
        public event Action ReturnRequested;

        private void OnEnable()
        {
            ReturnKeyPressed += OnReturnRequested;
            ResetMenuController();
        }

        private void OnDisable()
        {
            ReturnKeyPressed -= OnReturnRequested;
        }

        /// <summary>
        /// Populates the inventory UI with the given items.
        /// Adds a Cancel button at the bottom.
        /// </summary>
        /// <param name="items">The list of items to display.</param>
        public void PopulateItems(IReadOnlyList<Item> items)
        {
            ClearItems();

            foreach (Item item in items)
            {
                AddItem(item);
            }

            AddCancelButton();
        }

        private void AddItem(Item item)
        {
            ItemUI itemUI = Instantiate(inventoryItemPrefab, itemsContainer);

            itemUI.Bind(item);
            itemUI.OnHighlighted += OnOptionHighlighted;
            itemUI.OnSubmitted += OnOptionSubmitted;
        }

        private void ClearItems()
        {
            for (int i = itemsContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(itemsContainer.GetChild(i).gameObject);
            }
        }

        private void AddCancelButton()
        {
            CancelMenuButton button = Instantiate(cancelButtonPrefab, itemsContainer);

            button.transform.SetAsLastSibling();
            button.Selected += OnOptionHighlighted;
            button.Confirmed += OnReturnRequested;
        }

        private void OnOptionSubmitted(IDisplayable menuOption)
        {
            OptionSubmitted?.Invoke(menuOption);
        }

        private void OnOptionHighlighted(IDisplayable menuOption)
        {
            OptionHighlighted?.Invoke(menuOption);
        }

        private void OnReturnRequested()
        {
            ReturnRequested?.Invoke();
        }
    }
}
