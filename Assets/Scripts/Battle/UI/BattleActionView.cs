using System;
using PokemonGame.Shared.UI.Core;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// View that displays the main battle action options
    /// Raises intent events when the player selects an action,
    /// allowing the battle state machine to react accordingly.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BattleActionView : View
    {
        [SerializeField, Required, Tooltip("Button to initiate the move selection screen.")]
        private MenuButton moveSelectionButton;

        [SerializeField, Required, Tooltip("Button to open the inventory screen.")]
        private MenuButton inventoryButton;

        [SerializeField, Required, Tooltip("Button to open the Monster party screen.")]
        private MenuButton partyButton;

        [SerializeField, Required, Tooltip("Button to attempt escaping the battle.")]
        private MenuButton escapeButton;

        /// <summary>
        /// Raised when the player selects the move selection option.
        /// </summary>
        internal event Action MoveSelectionRequested;

        /// <summary>
        /// Raised when the player selects the inventory option.
        /// </summary>
        internal event Action InventoryRequested;

        /// <summary>
        /// Raised when the player selects the party option.
        /// </summary>
        internal event Action PartyRequested;

        /// <summary>
        /// Raised when the player selects the escape option.
        /// </summary>
        internal event Action EscapeRequested;

        private void OnEnable()
        {
            moveSelectionButton.Confirmed += OnMoveSelectionRequested;
            inventoryButton.Confirmed += OnInventoryRequested;
            partyButton.Confirmed += OnPartyRequested;
            escapeButton.Confirmed += OnEscapeRequested;
        }

        private void OnDisable()
        {
            moveSelectionButton.Confirmed -= OnMoveSelectionRequested;
            inventoryButton.Confirmed -= OnInventoryRequested;
            partyButton.Confirmed -= OnPartyRequested;
            escapeButton.Confirmed -= OnEscapeRequested;

            ResetMenuController();
        }

        private void OnMoveSelectionRequested() => MoveSelectionRequested?.Invoke();
        private void OnInventoryRequested() => InventoryRequested?.Invoke();
        private void OnPartyRequested() => PartyRequested?.Invoke();
        private void OnEscapeRequested() => EscapeRequested?.Invoke();
    }
}