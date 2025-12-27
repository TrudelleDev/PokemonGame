using System;
using PokemonGame.Inventory.UI.InventoryOptions;
using PokemonGame.Items.Definition;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory.UI
{
    /// <summary>
    /// Handles inventory UI logic and data binding.
    /// Subscribes to an <see cref="InventoryController"/> to respond to player input.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryPresenter : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Reference to the InventoryView displaying items.")]
        private InventoryView view;

        [SerializeField, Required, Tooltip("Panel showing detailed information about a selected item.")]
        private InventoryItemDetailPanel detailPanel;

        [SerializeField, Required, Tooltip("The inventory manager containing the player's items.")]
        private InventoryManager inventory;

        [SerializeField, Required, Tooltip("The controller that handles player input for the inventory.")]
        private InventoryController controller;

        /// <summary>
        /// Raised when an item is used, returning true if usage was successful.
        /// </summary>
        internal event Action<bool> ItemUsed;

        private void OnEnable()
        {
            inventory.Initialize();
            view.PopulateItems(inventory.Items);

            controller.ItemSelected += ShowItemOptions;
            controller.ItemHighlighted += UpdateDetails;
            controller.CancelRequested += HandleCloseInventory;

            inventory.ItemsChanged += HandleInventoryChanged;
        }

        private void OnDisable()
        {
            controller.ItemSelected -= ShowItemOptions;
            controller.ItemHighlighted -= UpdateDetails;
            controller.CancelRequested -= HandleCloseInventory;

            inventory.ItemsChanged -= HandleInventoryChanged;
        }

        private void UpdateDetails(IDisplayable displayable)
        {
            if (displayable == null)
            {
                detailPanel.Unbind();
            }
            else
            {
                detailPanel.Bind(displayable);
            }
        }

        private void ShowItemOptions(IDisplayable displayable)
        {
            if (displayable is not ItemDefinition itemDefinition)
            {
                return;
            }
           
            // Show the options view (UI)
            var optionsView = ViewManager.Instance.Show<InventoryOptionsView>();

            // Initialize the presenter, not the view
            if (optionsView.TryGetComponent<InventoryOptionsPresenter>(out var optionsPresenter))
            {
                optionsPresenter.Initialize(itemDefinition);

                // Subscribe to item used event
                optionsPresenter.ItemUsed += result =>
                {
                    ItemUsed?.Invoke(result);
                };
            }
        }

        private void HandleCloseInventory()
        {
            ViewManager.Instance.Close<InventoryView>();
        }

        private void HandleInventoryChanged()
        {
            view.PopulateItems(inventory.Items);
        }
    }
}
