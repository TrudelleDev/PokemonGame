using System;
using PokemonGame.Characters.Config;
using PokemonGame.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
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

        internal event Action PartyOpenRequested;
        internal event Action InventoryOpenRequested;
        internal event Action CloseRequested;

        private void OnEnable()
        {
            partyButton.OnSubmitted += OnPartyOpenRequested;
            inventoryButton.OnSubmitted += OnInventoryOpenRequested;
            exitButton.OnSubmitted += OnCloseRequested;

            // Base view event
            ReturnKeyPressed += OnCloseRequested;

            ResetMenuController();
        }

        private void OnDisable()
        {
            partyButton.OnSubmitted -= OnPartyOpenRequested;
            inventoryButton.OnSubmitted -= OnInventoryOpenRequested;
            exitButton.OnSubmitted -= OnCloseRequested;

            // Base view event
            ReturnKeyPressed -= OnCloseRequested;
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyBinds.Menu))
            {
                CloseRequested?.Invoke();
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
