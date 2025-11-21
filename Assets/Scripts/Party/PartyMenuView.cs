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
    /// Handles slot navigation, option selection, and cancel input.
    /// </summary>
    public class PartyMenuView : View
    {
        private const string ChoosePokemonMessage = "Choose a Pokémon.";
        private const string ActionMessage = "What will you do?";

        [Title("References")]
        [SerializeField, Required]
        [Tooltip("Reference to the current player party.")]
        private PartyManager party;

        [SerializeField, Required]
        [Tooltip("Button used to close the party menu.")]
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("Text box displaying contextual instructions.")]
        private TextMeshProUGUI dialogueText;

        [Title("Menu Controllers")]
        [SerializeField, Required]
        [Tooltip("Controls navigation between party slots.")]
        private VerticalMenuController partySlotController;

        public bool OppenedFromInventory { get; set; }

        /// <summary>
        /// Raised when the player selects a Pokémon from the menu.
        /// </summary>
        public event Action<PokemonInstance> OnPokemonSelected;

        private void OnEnable()
        {
            partySlotController.OnClick += OnPartySlotClick;
            cancelButton.OnClick += OnCancelButtonClick;

            partySlotController.SelectFirst();
            dialogueText.text = ChoosePokemonMessage;
        }

        private void OnDisable()
        {
            partySlotController.OnClick -= OnPartySlotClick;
            cancelButton.OnClick -= OnCancelButtonClick;
        }

        /// <summary>
        /// Freezes the view, disabling party slot navigation
        /// and showing the option message instead.
        /// </summary>
        public override void Freeze()
        {
            partySlotController.enabled = false;
            dialogueText.text = ActionMessage;
            base.Freeze();
        }

        /// <summary>
        /// Unfreezes the view, re-enabling party slot navigation
        /// and showing the default choose message.
        /// </summary>
        public override void Unfreeze()
        {
            partySlotController.enabled = true;
            dialogueText.text = ChoosePokemonMessage;
            base.Unfreeze();
        }

        /// <summary>
        /// Handles clicks on a party slot and raises <see cref="OnPokemonSelected"/>
        /// if the slot contains a valid Pokémon.
        /// </summary>
        /// <param name="menuButton">The clicked menu button.</param>
        private void OnPartySlotClick(MenuButton menuButton)
        {
            PartyMenuSlot menuSlot = menuButton.GetComponent<PartyMenuSlot>();

            if (menuSlot == null || menuSlot.BoundPokemon == null)
            {
                return;
            }

            party.SelectPokemon(menuSlot.BoundPokemon);

            // Notify listeners about the selected Pokémon
            OnPokemonSelected?.Invoke(menuSlot.BoundPokemon);

            if (!OppenedFromInventory)
            {
                ViewManager.Instance.Show<PartyMenuOptionView>();
            }
        }

        private void OnCancelButtonClick()
        {

            ViewManager.Instance.CloseTopView();
        }
    }
}
