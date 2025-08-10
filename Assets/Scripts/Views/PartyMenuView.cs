using PokemonGame.Menu.Controllers;
using PokemonGame.Menu.UI;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.PartyMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Views
{
    /// <summary>
    /// Displays the party menu where the player can select a Pokémon and choose actions.
    /// Handles input and transitions between the party slots and the option menu.
    /// </summary>
    public class PartyMenuView : View
    {
        [SerializeField, Required]
        [Tooltip("Reference to the current player party. Used to track and select Pokémon.")]
        private Party party;

        [SerializeField, Required]
        [Tooltip("Button used to cancel out of the party menu. Triggers view closing or returns to slot selection.")]
        private Button cancelButton;

        [SerializeField, Required]
        [Tooltip("Option menu UI shown after selecting a Pokémon. Allows actions like 'View', 'Use Item', etc.")]
        private PartyMenuOption partyMenuOption;

        [SerializeField, Required]
        [Tooltip("Text box used to display contextual instructions. Updated when switching between slot and option menus.")]
        private TextSetter dialogBox;

        [Title("Menu Controllers")]

        [SerializeField, Required]
        [Tooltip("Controls navigation between party slots. Handles selection and click input.")]
        private VerticalMenuController partySlotController;

        [SerializeField, Required]
        [Tooltip("Controls navigation inside the party option menu. Enabled only after selecting a Pokémon.")]
        private VerticalMenuController partyOptionController;

        private CloseView closeView;

        /// <summary>
        /// Called once before the view is shown for the first time.
        /// Sets up button and controller listeners.
        /// </summary>
        public override void Initialize()
        {
            closeView = GetComponent<CloseView>();

            partySlotController.OnClick += OnPartySlotClick;
            partyMenuOption.OnCancel += OnPartyOptionCancel;
            cancelButton.onClick.AddListener(OnCancel);
        }

        /// <summary>
        /// Called every time the view is enabled.
        /// Resets the selected party slot and updates the dialog text.
        /// </summary>
        private void OnEnable()
        {

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
        /// Called when a party slot is clicked.
        /// Selects the Pokémon and opens the party option menu.
        /// </summary>
        private void OnPartySlotClick(Button menuButton)
        {
            var slot = menuButton.GetComponent<PartyMenuSlot>();

            if (slot?.BoundPokemon == null)
                return;

            party.SelectPokemon(slot.BoundPokemon);
            TogglePartyOptionMenu(true);
        }

        /// <summary>
        /// Called when canceling the option menu.
        /// Hides the option menu and re-enables party slot control.
        /// </summary>
        private void OnPartyOptionCancel()
        {
            TogglePartyOptionMenu(false);
        }

        /// <summary>
        /// Called when the cancel button is pressed.
        /// Returns to the previous view if the option menu is not active.
        /// </summary>
        private void OnCancel()
        {
            if (!partyMenuOption.gameObject.activeInHierarchy)
            {
                ViewManager.Instance.GoToPreviousView();
            }
        }

        /// <summary>
        /// Toggles visibility and input control between the slot and option menus.
        /// Updates UI state and dialog box.
        /// </summary>
        private void TogglePartyOptionMenu(bool show)
        {
            partyOptionController.enabled = show;
            partyMenuOption.gameObject.SetActive(show);

            partySlotController.enabled = !show;
            closeView.enabled = !show;

            dialogBox.SetText(show ? "What you gonna do?" : "Choose a Pokémon or cancel.");
        }
    }
}
