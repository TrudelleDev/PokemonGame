using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Handles user interaction for <see cref="InventoryOptionsView"/>.
    /// Raises events to notify the presenter when an option is selected.
    /// Acts as a controller separating UI logic from presentation logic.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryOptionsController : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Reference to the InventoryOptionsView containing the option buttons.")]
        private InventoryOptionsView inventoryOptionsView;

        /// <summary>
        /// Raised when the 'Use' option is selected by the player.
        /// </summary>
        internal event Action UseRequested;

        /// <summary>
        /// Raised when the 'Cancel' option is selected by the player.
        /// </summary>
        internal event Action CancelRequested;

        private void OnEnable()
        {
            inventoryOptionsView.UseRequested += OnUseRequested;
            inventoryOptionsView.CancelRequested += OnCancelRequested;
        }

        private void OnDisable()
        {
            inventoryOptionsView.UseRequested -= OnUseRequested;
            inventoryOptionsView.CancelRequested -= OnCancelRequested;
        }

        private void OnUseRequested() 
        {
            UseRequested?.Invoke();
        } 

        private void OnCancelRequested()
        {
            CancelRequested?.Invoke();
        }
    }
}
