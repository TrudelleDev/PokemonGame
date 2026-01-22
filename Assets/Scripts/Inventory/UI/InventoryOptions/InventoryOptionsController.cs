using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Handles user interaction for <see cref="InventoryOptionsView"/>.
    /// Raises events to notify the presenter when an option is selected.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InventoryOptionsController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the InventoryOptionsView containing the option buttons.")]
        private InventoryOptionsView inventoryOptionsView;

        /// <summary>
        /// Raised when the 'Use' option is selected by the player.
        /// </summary>
        public event Action UseRequested;

        /// <summary>
        /// Raised when the 'Return' option is selected by the player.
        /// </summary>
        public event Action ReturnRequested;

        private void OnEnable()
        {
            inventoryOptionsView.UseRequested += OnUseRequested;
            inventoryOptionsView.ReturnRequested += OnCancelRequested;
        }

        private void OnDisable()
        {
            inventoryOptionsView.UseRequested -= OnUseRequested;
            inventoryOptionsView.ReturnRequested -= OnCancelRequested;
        }

        private void OnUseRequested() 
        {
            UseRequested?.Invoke();
        } 

        private void OnCancelRequested()
        {
            ReturnRequested?.Invoke();
        }
    }
}
