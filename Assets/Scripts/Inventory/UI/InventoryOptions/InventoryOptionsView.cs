using System;
using PokemonGame.Menu;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Displays options for a selected inventory item (Use, Cancel).
    /// Raises events when the player selects an option.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryOptionsView : View
    {
        [SerializeField, Required, Tooltip("Button used to confirm item usage.")]
        private MenuButton useButton;

        [SerializeField, Required, Tooltip("Button used to cancel and close the options menu.")]
        private MenuButton cancelButton;

        [SerializeField, Required, Tooltip("Text element used to display the selected item name and description.")]
        private TextMeshProUGUI descriptionText;

        /// <summary>
        /// Raised when the player chooses to use the selected item.
        /// </summary>
        internal event Action UseRequested;

        /// <summary>
        /// Raised when the player cancels the inventory options menu.
        /// </summary>
        internal event Action CancelRequested;

        private void OnEnable()
        {
            useButton.OnSubmitted += OnUseRequested;
            cancelButton.OnSubmitted += OncancelRequested;

            // Base view event
            CancelKeyPressed += OncancelRequested;
            ResetMenuController();
        }

        private void OnDisable()
        {
            useButton.OnSubmitted -= OnUseRequested;
            cancelButton.OnSubmitted -= OncancelRequested;

            // Base view event
            CancelKeyPressed -= OncancelRequested;
        }

        /// <summary>
        /// Updates the description text displayed in the inventory options view.
        /// </summary>
        /// <param name="text">The text to display as the item description.</param>
        internal void SetDescription(string text)
        {
            descriptionText.text = text;
        }

        private void OnUseRequested()
        {
            UseRequested?.Invoke();
        }

        private void OncancelRequested() 
        {
            CancelRequested?.Invoke();
        } 
    }
}
