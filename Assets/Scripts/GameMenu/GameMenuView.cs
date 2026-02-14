using System;
using MonsterTamer.Config;
using MonsterTamer.Shared.UI.Core;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.GameMenu
{
    /// <summary>
    /// Main game menu view. Raises events when the player requests
    /// to open the Party menu, open the Inventory, or close the game menu.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class GameMenuView : View
    {
        [Title("Game Menu Settings")]

        [SerializeField, Required, Tooltip("Opens the Party menu.")]
        private MenuButton partyButton;

        [SerializeField, Required, Tooltip("Opens the Inventory.")]
        private MenuButton inventoryButton;

        [SerializeField, Required, Tooltip("Closes the game menu.")]
        private MenuButton exitButton;

        /// <summary>
        /// Raised when the player requests to open the Party menu.
        /// </summary>
        internal event Action PartyOpenRequested;

        /// <summary>
        /// Raised when the player requests to open the Inventory menu.
        /// </summary>
        internal event Action InventoryOpenRequested;

        /// <summary>
        /// Raised when the player requests to close the game menu.
        /// </summary>
        internal event Action CloseRequested;

        private void OnEnable()
        {
            partyButton.Confirmed += OnPartyOpenRequested;
            inventoryButton.Confirmed += OnInventoryOpenRequested;
            exitButton.Confirmed += OnCloseRequested;

            // Base view event
            ReturnKeyPressed += OnCloseRequested;

            ResetMenuController();
        }

        private void OnDisable()
        {
            partyButton.Confirmed -= OnPartyOpenRequested;
            inventoryButton.Confirmed -= OnInventoryOpenRequested;
            exitButton.Confirmed -= OnCloseRequested;

            // Base view event
            ReturnKeyPressed -= OnCloseRequested;
        }

        protected override void Update()
        {
            base.Update();

            // Only allow closing if this is the active view.
            if (ViewManager.Instance.CurrentView != this)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Menu))
            {
                OnCloseRequested();
            }
        }

        private void OnPartyOpenRequested()
        {
            PartyOpenRequested?.Invoke();
        }

        private void OnInventoryOpenRequested()
        {
            InventoryOpenRequested?.Invoke();
        }

        private void OnCloseRequested()
        {
            CloseRequested?.Invoke();
        }
    }
}
