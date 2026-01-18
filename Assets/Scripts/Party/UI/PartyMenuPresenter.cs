using System;
using PokemonGame.Characters;
using PokemonGame.Party.Enums;
using PokemonGame.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI
{
    /// <summary>
    /// Handles user input on the party menu, interprets intent, 
    /// and raises events for selection, swapping, and menu actions.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PartyMenuPresenter : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("UI view that displays party slots and forwards input.")]
        private PartyMenuView partyMenuView;

        [SerializeField, Required]
        [Tooltip("Player character owning the active party.")]
        private Character player;

        private PartySelectionMode mode;
        private bool isSwapping;
        private int swapIndexA;

        /// <summary>
        /// Raised when a Monster is confirmed for an action 
        /// </summary>
        public event Action<PokemonInstance> MonsterConfirmed;

        /// <summary>
        /// Raised when a Monster is selected as a target for item usage.
        /// </summary>
        public event Action<PokemonInstance> ItemTargetSelected;

        /// <summary>
        /// Raised when the user requests to open the options menu
        /// for the currently selected party slot.
        /// </summary>
        public event Action OptionsRequested;

        /// <summary>
        /// Raised when the user requests to return or close the party menu,
        /// </summary>
        public event Action ReturnRequested;

        private void OnEnable()
        {
            partyMenuView.SlotPressed += HandleSlotPressed;
            partyMenuView.ReturnRequested += HandleReturnRequested;

            partyMenuView.ShowChoosePrompt();
            partyMenuView.RefreshSlots();
        }

        private void OnDisable()
        {
            partyMenuView.SlotPressed -= HandleSlotPressed;
            partyMenuView.ReturnRequested -= HandleReturnRequested;
        }

        /// <summary>
        /// Configures the presenter for the given party selection mode.
        /// The mode determines how slot input is interpreted
        /// </summary>
        /// <param name="selectionMode">The context in which the party menu is being used.</param>
        public void Setup(PartySelectionMode selectionMode)
        {
            mode = selectionMode;
        }

        /// <summary>
        /// Starts a swap operation for overworld mode.
        /// </summary>
        public void StartSwap()
        {
            // Can only swap during overworld mode
            if (mode != PartySelectionMode.Overworld)
                return;

            isSwapping = true;
            swapIndexA = player.Party.SelectedIndex;
            partyMenuView.ShowSwapPrompt(partyMenuView.CurrentSlotButton);
        }

        private void HandleSlotPressed(PartyMenuSlot slot)
        {
            player.Party.SelectSlotIndex(slot.Index);

            switch (mode)
            {
                case PartySelectionMode.Overworld:
                    HandleOverworldSelection(slot);
                    break;

                case PartySelectionMode.Battle:
                    HandleOverworldSelection(slot);
                    break;

                case PartySelectionMode.UseItem:
                    ItemTargetSelected?.Invoke(player.Party.SelectedMonster);
                    break;
            }
        }

        private void HandleOverworldSelection(PartyMenuSlot slot)
        {
            if (isSwapping)
            {
                if (swapIndexA != slot.Index)
                {
                    player.Party.Swap(swapIndexA, slot.Index);
                }

                EndSwap();
                partyMenuView.RefreshSlots();
                return;
            }

            // Raise event to request showing the party option menu
            OptionsRequested?.Invoke();
        }

        private void EndSwap()
        {
            isSwapping = false;
            partyMenuView.ClearSwapLock();
        }

        private void HandleReturnRequested()
        {
            if (mode == PartySelectionMode.Battle)
            {
                return;
            }

            ReturnRequested?.Invoke();
        }
    }
}
