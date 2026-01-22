using System;
using PokemonGame.Characters;
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
    public sealed class InventoryPresenter : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Reference to the InventoryView displaying items.")]
        private InventoryView view;

        [SerializeField, Required, Tooltip("Panel showing detailed information about a selected item.")]
        private InventoryItemDetailPanel detailPanel;

        [SerializeField, Required, Tooltip("The player character that owns the inventory being displayed.")]
        private Character player;

        [SerializeField, Required, Tooltip("The controller that handles player input for the inventory.")]
        private InventoryController controller;

        /// <summary>
        /// Raised when an item is used, returning true if usage was successful.
        /// </summary>
        public event Action<bool> ItemUsed;

        private void OnEnable()
        {
            player.Inventory.Initialize();
            view.PopulateItems(player.Inventory.Items);

            controller.ItemSelected += ShowItemOptions;
            controller.ItemHighlighted += UpdateDetails;
            controller.ReturnRequested += HandleCloseInventory;

            player.Inventory.ItemsChanged += HandleInventoryChanged;
        }

        private void OnDisable()
        {
            controller.ItemSelected -= ShowItemOptions;
            controller.ItemHighlighted -= UpdateDetails;
            controller.ReturnRequested -= HandleCloseInventory;

            player.Inventory.ItemsChanged -= HandleInventoryChanged;
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
            view.PopulateItems(player.Inventory.Items);
        }
    }
}
