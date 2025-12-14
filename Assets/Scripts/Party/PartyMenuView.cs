using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Pokemon;
using PokemonGame.Summary;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Displays the party menu where the player can select a Pokémon
    /// and choose actions. This view only raises intent events;
    /// higher-level systems decide what those actions mean.
    /// </summary>
    public class PartyMenuView : View
    {
        private const string ChoosePokemonMessage = "Choose a Pokémon.";
        private const string ActionMessage = "What will you do?";
        private const string SwapStartMessage = "Move to where?";

        [Title("References")]
        [SerializeField, Required] private PartyManager party;
        [SerializeField] private PartyMenuSlotManager slotManager;
        [SerializeField, Required] private MenuButton cancelButton;
        [SerializeField, Required] private TextMeshProUGUI dialogueText;
        [SerializeField, Required] private VerticalMenuController partySlotController;

        private bool isSwapping;
        private int swapIndexA = -1;
        private MenuButton lockSwapButton;

        public bool OppenedFromInventory { get; set; }

        public event Action<PokemonInstance> OnPokemonSelected;
        public event Action OnCloseButtonPress;

        public PartyManager Party => party;

        private void OnEnable()
        {
            partySlotController.OnClick += OnPartySlotClick;
            cancelButton.OnClick += OnCancelPressed;
            OnCloseKeyPress += OnCancelPressed;

            RefreshSlots();
            partySlotController.SelectFirst();
            dialogueText.text = ChoosePokemonMessage;
        }

        private void OnDisable()
        {
            partySlotController.OnClick -= OnPartySlotClick;
            cancelButton.OnClick -= OnCancelPressed;
            OnCloseKeyPress -= OnCancelPressed;
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
            {
                dialogueText.text = ChoosePokemonMessage;
            }

            base.Unfreeze();
        }

        public void StartSwapMode()
        {
            isSwapping = true;
            swapIndexA = party.SelectedIndex;

            lockSwapButton = partySlotController.CurrentButton;
            if (lockSwapButton != null)
            {
                lockSwapButton.LockSelectSprite = true;
            }

            dialogueText.text = SwapStartMessage;
        }

        private void HandleSwapSelection(int clickedIndex)
        {
            if (swapIndexA == clickedIndex)
            {
                return;
            }

            party.Swap(swapIndexA, clickedIndex);
            RefreshSlots();
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

        private void RefreshSlots()
        {
            slotManager.RefreshSlots();
        }

        private void OnPartySlotClick(MenuButton menuButton)
        {
            if (menuButton.GetComponent<PartyMenuSlot>() is not { BoundPokemon: { } boundPokemon })
            {
                return;
            }

            int clickedIndex = menuButton.GetComponent<PartyMenuSlot>().SlotIndex;

            if (isSwapping)
            {
                HandleSwapSelection(clickedIndex);
                return;
            }

            party.SelectPokemon(boundPokemon);
            OnPokemonSelected?.Invoke(boundPokemon);

            if (OppenedFromInventory)
            {
                return;
            }

            var optionView = ViewManager.Instance.Show<PartyMenuOptionView>();

            optionView.OnSwitchSelected -= OnOptionSwitchSelected;
            optionView.OnSummarySelected -= OnOptionSummarySelected;
            optionView.OnCancelSelected -= OnOptionCancelSelected;

            optionView.OnSwitchSelected += OnOptionSwitchSelected;
            optionView.OnSummarySelected += OnOptionSummarySelected;
            optionView.OnCancelSelected += OnOptionCancelSelected;
        }

        private void OnOptionSwitchSelected()
        {
            StartSwapMode();
            RefreshSlots();
            ViewManager.Instance.CloseTopView();
        }

        private void OnOptionSummarySelected()
        {
            ViewManager.Instance.Show<SummaryView>();
        }

        private void OnOptionCancelSelected()
        {
            ViewManager.Instance.CloseTopView();
        }

        private void OnCancelPressed()
        {
            if (isSwapping)
            {
                ResetSwapState();
                return;
            }

            OnCloseButtonPress?.Invoke();
        }
    }
}
