using System;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory.UI
{
    /// <summary>
    /// Handles player input for the inventory menu, interprets intent,
    /// and raises events for item selection, highlighting, and cancellation.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InventoryController : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Reference to the inventory view.")]
        private InventoryView view;

        /// <summary>
        /// Raised when the player selects an item.
        /// </summary>
        internal event Action<IDisplayable> ItemSelected;

        /// <summary>
        /// Raised when the player highlights an item (cursor moves).
        /// </summary>
        internal event Action<IDisplayable> ItemHighlighted;

        /// <summary>
        /// Raised when the player requests to close the inventory.
        /// </summary>
        public event Action ReturnRequested;

        private void OnEnable()
        {
            view.OptionSubmitted += HandleItemSelected;
            view.OptionHighlighted += HandleItemHighlighted;
            view.ReturnRequested += HandleReturnRequested;
            view.ReturnKeyPressed += HandleReturnRequested;
        }

        private void OnDisable()
        {
            view.OptionSubmitted -= HandleItemSelected;
            view.OptionHighlighted -= HandleItemHighlighted;
            view.ReturnRequested -= HandleReturnRequested;
            view.ReturnKeyPressed -= HandleReturnRequested;
        }

        private void HandleItemSelected(IDisplayable displayable)
        {
            ItemSelected?.Invoke(displayable);
        }

        private void HandleItemHighlighted(IDisplayable displayable)
        {
            ItemHighlighted?.Invoke(displayable);
        }

        private void HandleReturnRequested()
        {
            ReturnRequested?.Invoke();
        }
    }
}
