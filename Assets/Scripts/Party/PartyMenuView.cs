using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Displays the party menu where the player can select a Pokémon and choose actions.
    /// Handles slot navigation, option selection, swapping, and cancel input.
    /// </summary>
    public class PartyMenuView : View
    {
        private const string ChoosePokemonMessage = "Choose a Pokémon.";
        private const string ActionMessage = "What will you do?";
        private const string SwapStartMessage = "Move to where?";

        [Title("References")]
        [SerializeField, Required, Tooltip("Reference to the current player party.")]
        private PartyManager party;

        [SerializeField]
        private PartyMenuSlotManager slotManager;

        [SerializeField, Required, Tooltip("Button used to close the party menu.")]
        private MenuButton cancelButton;

        [SerializeField, Required, Tooltip("Text box displaying contextual instructions.")]
        private TextMeshProUGUI dialogueText;

        [Title("Menu Controllers")]
        [SerializeField, Required, Tooltip("Controls navigation between party slots.")]
        private VerticalMenuController partySlotController;

        private bool isSwapping;
        private int swapIndexA = -1;
        private MenuButton lockSwapButton;

        public bool OppenedFromInventory { get; set; }

        public event Action<PokemonInstance> OnPokemonSelected;

        private void OnEnable()
        {
            partySlotController.OnClick += OnPartySlotClick;
            cancelButton.OnClick += CancelButtonClick;

            partySlotController.SelectFirst();
            dialogueText.text = ChoosePokemonMessage;
        }

        private void OnDisable()
        {
            partySlotController.OnClick -= OnPartySlotClick;
            cancelButton.OnClick -= CancelButtonClick;

            // Ensure swap state is reset if view is closed while swapping
            if (isSwapping)
            {
                ResetSwapState();
            }
        }

        public override void Freeze()
        {
            partySlotController.enabled = false;
            dialogueText.text = ActionMessage;
            base.Freeze();
        }

        public override void Unfreeze()
        {
            partySlotController.enabled = true;

            if (!isSwapping)
                dialogueText.text = ChoosePokemonMessage;

            base.Unfreeze();
        }

        public void StartSwapMode()
        {
            isSwapping = true;
            swapIndexA = party.SelectedIndex;

            lockSwapButton = partySlotController.CurrentButton;
            if (lockSwapButton != null)
                lockSwapButton.LockSelectSprite = true;

            dialogueText.text = SwapStartMessage;
        }

        private void HandleSwapSelection(int clickedIndex)
        {
            if (swapIndexA == clickedIndex)
                return;

            party.Swap(swapIndexA, clickedIndex);
            slotManager.RefreshSlots();

            ResetSwapState();
        }

        private void ResetSwapState()
        {
            isSwapping = false;
            swapIndexA = -1;

            if (lockSwapButton != null)
            {
                lockSwapButton.LockSelectSprite = false;
                lockSwapButton = null;
            }

            dialogueText.text = ChoosePokemonMessage;
        }

        private void OnPartySlotClick(MenuButton menuButton)
        {
            if (menuButton.GetComponent<PartyMenuSlot>() is not { BoundPokemon: { } boundPokemon } menuSlot)
                return;

            int clickedIndex = menuSlot.SlotIndex;

            if (isSwapping)
            {
                HandleSwapSelection(clickedIndex);
                return;
            }

            party.SelectPokemon(boundPokemon);
            OnPokemonSelected?.Invoke(boundPokemon);

            if (!OppenedFromInventory)
                ViewManager.Instance.Show<PartyMenuOptionView>();
        }

        private void CancelButtonClick()
        {
            // Reset swap if active when canceling
            if (isSwapping)
            {
                ResetSwapState();
            }

            ViewManager.Instance.CloseTopView();
        }
    }
}
