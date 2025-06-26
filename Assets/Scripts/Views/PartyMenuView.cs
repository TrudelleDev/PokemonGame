using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.PartyMenu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    public class PartyMenuView : View
    {
        [SerializeField] private Party party;
        [SerializeField] private MenuButton cancelButton;
        [SerializeField] private PartyMenuOption partyMenuOption;
        [SerializeField] private TextSetter dialogBox;

        [Title("Menu Controllers")]
        [SerializeField] private VerticalMenuController partySlotController;
        [SerializeField] private VerticalMenuController partyOptionController;

        private CloseView closeView;
        private bool resetToFirst;

        public override void Initialize()
        {
            closeView = GetComponent<CloseView>();

            partySlotController.OnClick += OnPartySlotControllerClick;
            partyMenuOption.OnCancel += OnPartyMenuOptionCancel;
            cancelButton.OnClick += OnCancel;
        }

        private void OnEnable()
        {
            if (resetToFirst)
            {
                partySlotController.ResetToFirstElement();
                resetToFirst = false;
            }
              
            dialogBox.SetText("Choose a POKÈMON or CANCEL.");
        }

        private void OnDisable()
        {
            TogglePartyOptionMenu(false);
        }

        private void OnPartyMenuOptionCancel()
        {
            TogglePartyOptionMenu(false);
        }

        private void OnPartySlotControllerClick(MenuButton menuButton)
        {
            PartyMenuSlot slot = menuButton.GetComponent<PartyMenuSlot>();

            if (slot?.Pokemon != null)
            {
                party.SelectPokemon(slot.Pokemon);
                TogglePartyOptionMenu(true);
            }
        }

        private void TogglePartyOptionMenu(bool show)
        {
            partyOptionController.enabled = show;
            partyMenuOption.gameObject.SetActive(show);

            partySlotController.enabled = !show;
            closeView.enabled = !show;

            dialogBox.SetText(show ? "What you gonna do?" : "Choose a POKÈMON or CANCEL.");
        }

        private void OnCancel()
        {
            if (!partyMenuOption.gameObject.activeInHierarchy)
            {
                resetToFirst = true;
                // Go back to game menu view
                ViewManager.Instance.GoToPreviousView();
            }
        }
    }
}
