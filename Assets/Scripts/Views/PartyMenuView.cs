using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.PartyMenu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Displays the party menu where the player can select a Pokémon and choose actions.
    /// Handles input and transitions between the party slots and option menu.
    /// </summary>
    public class PartyMenuView : View
    {
        [SerializeField, Required] private Party party;
        [SerializeField, Required] private MenuButton cancelButton;
        [SerializeField, Required] private PartyMenuOption partyMenuOption;
        [SerializeField, Required] private TextSetter dialogBox;

        [Title("Menu Controllers")]
        [SerializeField, Required] private VerticalMenuController partySlotController;
        [SerializeField, Required] private VerticalMenuController partyOptionController;

        private CloseView closeView;
        private bool resetToFirst;

        /// <summary>
        /// Called once before the view is shown for the first time by the <see cref="ViewManager"/>.
        /// Subscribes to relevant events and initializes controller state.
        /// </summary>
        public override void Initialize()
        {
            closeView = GetComponent<CloseView>();

            partySlotController.OnClick += OnPartySlotControllerClick;
            partyMenuOption.OnCancel += OnPartyMenuOptionCancel;
            cancelButton.OnClick += OnCancel;
        }

        /// <summary>
        /// Called every time the view is enabled.
        /// Resets controller selection and updates the dialog box text.
        /// </summary>
        private void OnEnable()
        {
            if (resetToFirst)
            {
                partySlotController.ResetToFirstElement();
                resetToFirst = false;
            }

            dialogBox.SetText("Choose a Pokémon or cancel.");
        }

        /// <summary>
        /// Called when the view is disabled.
        /// Ensures the party option menu is hidden.
        /// </summary>
        private void OnDisable()
        {
            TogglePartyOptionMenu(false);
        }

        /// <summary>
        /// Called when a party slot is selected.
        /// Selects the Pokémon and opens the party option menu.
        /// </summary>
        private void OnPartySlotControllerClick(MenuButton menuButton)
        {
            PartyMenuSlot slot = menuButton.GetComponent<PartyMenuSlot>();

            if (slot?.Pokemon != null)
            {
                party.SelectPokemon(slot.Pokemon);
                TogglePartyOptionMenu(true);
            }
        }

        /// <summary>
        /// Called when the cancel button is pressed in the party option menu.
        /// Hides the option menu and returns control to the party list.
        /// </summary>
        private void OnPartyMenuOptionCancel()
        {
            TogglePartyOptionMenu(false);
        }

        /// <summary>
        /// Toggles between the party slot menu and the party option menu.
        /// Updates both controller states and dialog box text.
        /// </summary>
        private void TogglePartyOptionMenu(bool show)
        {
            partyOptionController.enabled = show;
            partyMenuOption.gameObject.SetActive(show);

            partySlotController.enabled = !show;
            closeView.enabled = !show;

            dialogBox.SetText(show ? "What you gonna do?" : "Choose a Pokémon or cancel.");
        }

        /// <summary>
        /// Handles the cancel button press from the root menu.
        /// Returns to the previous view if the option menu is not active.
        /// </summary>
        private void OnCancel()
        {
            if (!partyMenuOption.gameObject.activeInHierarchy)
            {
                resetToFirst = true;
                ViewManager.Instance.GoToPreviousView();
            }
        }
    }
}
