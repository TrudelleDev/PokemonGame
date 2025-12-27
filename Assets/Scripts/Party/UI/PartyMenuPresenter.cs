using System;
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
    internal sealed class PartyMenuPresenter : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the PartyMenuView for UI interaction.")]
        private PartyMenuView partyMenuView;

        [SerializeField, Required]
        [Tooltip("Reference to the player's party manager.")]
        private PartyManager playerParty;

        private PartySelectionMode mode;
        private bool isSwapping;
        private int swapIndexA;

        /// <summary>
        /// Raised when the player confirms a Pokémon selection.
        /// </summary>
        internal event Action<PokemonInstance> PokemonConfirmed;

        /// <summary>
        /// Raised when the player selects a Pokémon as the target for an item.
        /// </summary>
        internal event Action<PokemonInstance> ItemTargetSelected;

        /// <summary>
        /// Raised when the player requests to open the options menu.
        /// </summary>
        internal event Action OptionsRequested;

        /// <summary>
        /// Raised when the player requests to close the current view.
        /// </summary>
        internal event Action CloseRequested;

        /// <summary>
        /// Raised when the player requests to cancel the current action or menu.
        /// </summary>
        internal event Action CancelRequested;

        private void OnEnable()
        {
            partyMenuView.SlotPressed += HandleSlotPressed;
            partyMenuView.CancelRequested += HandleCancelRequested;

            partyMenuView.RefreshSlots();
            partyMenuView.ShowChoosePrompt();
        }

        private void OnDisable()
        {
            partyMenuView.SlotPressed -= HandleSlotPressed;
            partyMenuView.CancelRequested -= HandleCancelRequested;
        }

        /// <summary>
        /// Sets the current selection mode (Overworld, Battle, UseItem).
        /// </summary>
        internal void Setup(PartySelectionMode selectionMode)
        {
            mode = selectionMode;
        }

        /// <summary>
        /// Starts a swap operation for overworld mode.
        /// </summary>
        internal void StartSwap()
        {
            // Can only swap during overworld mode
            if (mode != PartySelectionMode.Overworld)
                return;

            isSwapping = true;
            swapIndexA = playerParty.SelectedIndex;
            partyMenuView.ShowSwapPrompt(partyMenuView.CurrentSlotButton);
        }

        private void HandleSlotPressed(PartyMenuSlot slot)
        {
            playerParty.SelectSlotIndex(slot.Index);

            switch (mode)
            {
                case PartySelectionMode.Overworld:
                    HandleOverworldSelection(slot);
                    break;

                case PartySelectionMode.Battle:
                    PokemonConfirmed?.Invoke(playerParty.SelectedPokemon);
                    break;

                case PartySelectionMode.UseItem:
                    ItemTargetSelected?.Invoke(playerParty.SelectedPokemon);
                    break;
            }
        }

        private void HandleOverworldSelection(PartyMenuSlot slot)
        {
            if (isSwapping)
            {
                if (swapIndexA != slot.Index)
                {
                    playerParty.Swap(swapIndexA, slot.Index);
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
            partyMenuView.ShowChoosePrompt();
        }

        private void HandleCancelRequested()
        {
            CancelRequested?.Invoke();
        }
    }
}
