using System;
using MonsterTamer.Shared.UI.Core;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Displays options for a selected inventory item (Use, Return).
    /// Raises events when the player selects an option.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InventoryOptionsView : View
    {
        [SerializeField, Required, Tooltip("Button used to confirm item usage.")]
        private MenuButton useButton;

        [SerializeField, Required, Tooltip("Button used to cancel and close the options menu.")]
        private MenuButton cancelButton;

        /// <summary>
        /// Raised when the player chooses to use the selected item.
        /// </summary>
        public event Action UseRequested;

        /// <summary>
        /// Raised when the 'Return' option is selected by the player.
        /// </summary>
        public event Action ReturnRequested;

        private void OnEnable()
        {
            useButton.Confirmed += OnUseRequested;
            cancelButton.Confirmed += OnReturnRequested;

            // Base view event
            ReturnKeyPressed += OnReturnRequested;
            ResetMenuController();
        }

        private void OnDisable()
        {
            useButton.Confirmed -= OnUseRequested;
            cancelButton.Confirmed -= OnReturnRequested;

            // Base view event
            ReturnKeyPressed -= OnReturnRequested;
        }

        private void OnUseRequested()
        {
            UseRequested?.Invoke();
        }

        private void OnReturnRequested()
        {
            ReturnRequested?.Invoke();
        }
    }
}
