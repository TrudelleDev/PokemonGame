using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
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
        private const string DialogueChoosePokemon = "Choose a Pokémon or cancel.";
        private const string DialogueOptionMenu = "What will you do?";

        [SerializeField, Required]
        [Tooltip("ViewManager for handling local views like the party option menu.")]
        private ViewManager innerViewManager;

        [Title("References")]
        [SerializeField, Required]
        [Tooltip("Reference to the current player party.")]
        private PartyManager party;

        [SerializeField, Required]
        [Tooltip("Button used to close the party menu.")]
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("Option menu shown after selecting a Pokémon.")]
        private PartyMenuOption partyMenuOption;

        [SerializeField, Required]
        [Tooltip("Text box displaying contextual instructions.")]
        private TextMeshProUGUI dialogueText;

        [Title("Menu Controllers")]
        [SerializeField, Required]
        [Tooltip("Controls navigation between party slots.")]
        private VerticalMenuController partySlotController;

        [SerializeField, Required]
        [Tooltip("Controls navigation inside the option menu.")]
        private VerticalMenuController partyOptionController;

        public override void Preload()
        {
            partySlotController.OnClick += OnPartySlotClick;
            partyMenuOption.OnCancel += OnPartyOptionCancel;
            cancelButton.OnClick += OnCancel;
        }

        private void OnDestroy()
        {
            partySlotController.OnClick -= OnPartySlotClick;
            partyMenuOption.OnCancel -= OnPartyOptionCancel;
            cancelButton.OnClick -= OnCancel;
        }

        private void OnEnable()
        {
            SetOptionMenuActive(false); // ensure clean state
        }

        /// <summary>
        /// Opens the option menu for the clicked Pokémon slot.
        /// </summary>
        private void OnPartySlotClick(MenuButton menuButton)
        {
            PartyMenuSlot menuSlot = menuButton.GetComponent<PartyMenuSlot>();

            if(menuSlot == null || menuSlot.BoundPokemon == null) 
            {
                return;
            }

            party.SelectPokemon(menuSlot.BoundPokemon);
            SetOptionMenuActive(true);
        }

        /// <summary>
        /// Closes the option menu and re-enables slot navigation.
        /// </summary>
        private void OnPartyOptionCancel()
        {
            SetOptionMenuActive(false);
        }

        /// <summary>
        /// Exits the party menu if no option menu is active.
        /// </summary>
        private void OnCancel()
        {
            if (!partyMenuOption.gameObject.activeInHierarchy)
            {
                ViewManager.Instance.GoToPreviousView();
            }
        }

        /// <summary>
        /// Switches between slot controller and option controller.
        /// </summary>
        private void SetOptionMenuActive(bool active)
        {
            partyOptionController.AcceptInput = active;
            partySlotController.AcceptInput = !active;

            if (active)
            {
                innerViewManager.Show<PartyMenuOption>();
                dialogueText.text = DialogueOptionMenu;
            }
            else
            {
                innerViewManager.CloseCurrentView();
                dialogueText.text = DialogueChoosePokemon;

                // auto-reselect the last party slot if any
                if (partySlotController.CurrentButton != null)
                {
                    partySlotController.CurrentButton.SetSelected(true);
                }
            }
        }
    }
}
