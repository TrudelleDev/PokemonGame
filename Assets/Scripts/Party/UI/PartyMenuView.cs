using System;
using PokemonGame.Shared.UI.Core;
using PokemonGame.Shared.UI.Navigation;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party.UI
{
    /// <summary>
    /// UI-only view for the party menu.
    /// Displays party slots and raises user input events without containing game logic.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuView : View
    {
        private const string SelectMonsterMessage = "Select a Monster.";
        private const string SwapStartMessage = "Move to where?";
        private const string ChooseActionMessage = "Do what with this Monster?";

        [SerializeField, Required]
        [Tooltip("Manages and displays the Monster slots in the party.")]
        private PartyMenuSlotManager menuSlotManager;

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
        public event Action<PartyMenuSlot> SlotPressed;

        /// <summary>
        /// Raised when the player requests to cancel the party menu.
        /// </summary>
        public event Action ReturnRequested;

        /// <summary>
        /// Gets the currently selected party slot button.
        /// </summary>
        public MenuButton CurrentSlotButton => partySlotController.CurrentButton;

        private void OnEnable()
        {
            partySlotController.Confirmed += OnSlotClicked;
            ReturnKeyPressed += OnCancelRequested; // Base view event

            dialogueText.text = SelectMonsterMessage;
            ResetMenuController();
        }

        private void OnDisable()
        {
            partySlotController.Confirmed -= OnSlotClicked;
            ReturnKeyPressed -= OnCancelRequested; // Base view event
        }

        /// <summary>
        /// Refreshes all Pokémon slots to reflect current party data.
        /// </summary>
        public void RefreshSlots()
        {
            menuSlotManager.RefreshSlots();
        }

        /// <summary>
        /// Shows the standard "Select a Monster" prompt in the dialogue text.
        /// </summary>
        public void ShowChoosePrompt()
        {
            dialogueText.text = SelectMonsterMessage;
        }

        /// <summary>
        /// Shows the swap prompt and locks the selected button to indicate it is the active swap slot.
        /// </summary>
        /// <param name="selectedButton">The button currently selected for swapping.</param>
        public void ShowSwapPrompt(MenuButton selectedButton)
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
        public void ClearSwapLock()
        {
            if (lockedSwapButton != null)
            {
                lockedSwapButton.LockSelectSprite = false;
                lockedSwapButton = null;
            }

            dialogueText.text = SelectMonsterMessage;
        }

        private void OnSlotClicked(MenuButton menuButton)
        {
            PartyMenuSlot slot = menuButton.GetComponent<PartyMenuSlot>();

            if (slot == null || slot.BoundMonster == null)
            {
                return;
            }

            dialogueText.text = ChooseActionMessage;
            SlotPressed?.Invoke(slot);
        }

        private void OnCancelRequested()
        {
            ReturnRequested?.Invoke();
        }
    }
}
