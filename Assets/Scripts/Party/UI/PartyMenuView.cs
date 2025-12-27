using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party.UI
{
    /// <summary>
    /// Pure UI view for the party menu.
    /// Raises intent events only; contains no game or flow logic.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuView : View
    {
        private const string ChoosePokemonMessage = "Choose a Pokémon.";
        private const string SwapStartMessage = "Move to where?";

        [SerializeField, Required]
        [Tooltip("Manages and displays the Pokémon slots in the party.")]
        private PartyMenuSlotManager menuSlotManager;

        [SerializeField, Required]
        [Tooltip("Button used to cancel or close the party menu.")]
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("Text field that displays prompts and messages.")]
        private TextMeshProUGUI dialogueText;

        [SerializeField, Required]
        [Tooltip("Controls navigation of the party slots via keyboard/controller.")]
        private VerticalMenuController partySlotController;

        private MenuButton lockedSwapButton;

        /// <summary>
        /// Raised when the player presses a party menu slot.
        /// </summary>
        internal event Action<PartyMenuSlot> SlotPressed;

        /// <summary>
        /// Raised when the player requests to cancel the party menu.
        /// </summary>
        internal event Action CancelRequested;

        /// <summary>
        /// Gets the currently selected party slot button.
        /// </summary>
        internal MenuButton CurrentSlotButton => partySlotController.CurrentButton;

        private void OnEnable()
        {
            partySlotController.OnClick += OnSlotClicked;
            cancelButton.OnSubmitted += OnCancelRequested;

            // Base view event
            CancelKeyPressed += OnCancelRequested;

            dialogueText.text = ChoosePokemonMessage;
            ResetMenuController();
        }

        private void OnDisable()
        {
            partySlotController.OnClick -= OnSlotClicked;
            cancelButton.OnSubmitted -= OnCancelRequested;

            // Base view event
            CancelKeyPressed -= OnCancelRequested;
        }

        /// <summary>
        /// Refreshes all Pokémon slots to reflect current party data.
        /// </summary>
        internal void RefreshSlots()
        {
            menuSlotManager.RefreshSlots();
        }

        /// <summary>
        /// Shows the standard "Choose a Pokémon" prompt in the dialogue text.
        /// </summary>
        internal void ShowChoosePrompt()
        {
            dialogueText.text = ChoosePokemonMessage;
        }

        /// <summary>
        /// Shows the swap prompt and locks the selected button to indicate it is the active swap slot.
        /// </summary>
        /// <param name="selectedButton">The button currently selected for swapping.</param>
        internal void ShowSwapPrompt(MenuButton selectedButton)
        {
            lockedSwapButton = selectedButton;

            if (lockedSwapButton != null)
            {
                lockedSwapButton.LockSelectSprite = true;
            }

            dialogueText.text = SwapStartMessage;
        }

        /// <summary>
        /// Clears any active swap lock and resets the dialogue prompt.
        /// </summary>
        internal void ClearSwapLock()
        {
            if (lockedSwapButton != null)
            {
                lockedSwapButton.LockSelectSprite = false;
                lockedSwapButton = null;
            }

            dialogueText.text = ChoosePokemonMessage;
        }

        private void OnSlotClicked(MenuButton menuButton)
        {
            PartyMenuSlot slot = menuButton.GetComponent<PartyMenuSlot>();

            if (slot == null || slot.BoundPokemon == null)
            {
                return;
            }

            SlotPressed?.Invoke(slot);
        }

        private void OnCancelRequested()
        {
            CancelRequested?.Invoke();
        }
    }
}
