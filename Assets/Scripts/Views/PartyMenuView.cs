using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.PartyMenu;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Views
{
    public class PartyMenuView : View
    {
        [SerializeField] private Party party;
        [SerializeField] private MenuButton cancelButton;
        [SerializeField] private PartyMenuOption partyMenuOption;
        [SerializeField] private PartyMenuDialogBox dialogBox;

        private PartyMenuSlotManager slotManager;
        [SerializeField] private VerticalMenuController partySlotController;
        private CloseView closeView;

        public override void Initialize() 
        {

        }

        private void Awake()
        {
            closeView = GetComponent<CloseView>();
            //partySlotController = GetComponent<VerticalMenuController>();

            partySlotController.Click += OnPartySlotControllerClick;
            partyMenuOption.OnCancel += PartyMenuOption_OnCancel;
            cancelButton.OnClick += () => HandleCancel();

            // Default dialog box size and text
            dialogBox.SetSize(180, 28);
            dialogBox.SetText("Choose a POKEMON.");

            

        }

        private void OnEnable()
        {
            partySlotController.ResetMenuController();

            slotManager = new PartyMenuSlotManager(party);
            slotManager.Initialize(partySlotController.transform); /// TODO: clean this
        }

        private void PartyMenuOption_OnCancel()
        {
            partySlotController.enabled = true;
            closeView.enabled = true;

            // Close the party menu option
            partyMenuOption.gameObject.SetActive(false);
            // Enable the party menu option controller
            partyMenuOption.GetComponentInChildren<VerticalMenuController>().enabled = false;

            // Change the text and size of the dialog box when close
            dialogBox.SetText("Choose a POKEMON.");
            dialogBox.SetSize(180, 28);

        }

        private void OnPartySlotControllerClick(MenuButton menuButton)
        {
            // Open party menu option when a pokemon is selected in the party menu
            if (menuButton.GetComponent<PartyMenuSlot>() != null)
            {
                party.SelectPokemon(menuButton.GetComponent<PartyMenuSlot>().Pokemon);

                partySlotController.enabled = false;
                closeView.enabled = false;

                // Open the party menu option
                partyMenuOption.gameObject.SetActive(true);
                // Disable the controller of the party menu option
                partyMenuOption.GetComponentInChildren<VerticalMenuController>().enabled = true;

                // Change the text and size of the dialog box when open
                dialogBox.SetSize(135, 28);
                dialogBox.SetText("What you gonna do?");
            }
        }

        private void HandleCancel()
        {         
            if (!partyMenuOption.gameObject.activeInHierarchy)
            {      
                // Go back to game menu view
                ViewManager.Instance.ShowLast();
            }
        }
    }
}
