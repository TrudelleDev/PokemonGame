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
        [SerializeField] private Button cancelButton;
        [SerializeField] private PartyMenuSlot[] partyMenuSlots;

        public override void Initialize() { }

        private void Awake()
        {
            cancelButton.onClick.AddListener(() => HandleCancel());

            // Disable every party menu slots
            foreach (PartyMenuSlot menuSlot in partyMenuSlots)
            {
                menuSlot.SetActive(false);
            }

            // Enable a party menu slot for each pokemon in the party and bind his data
            for (int i = 0; i < party.Pokemons.Count; i++)
            {
                partyMenuSlots[i].Bind(party.Pokemons[i]);
            }
        }

        private void HandleCancel()
        {
            transform.GetComponent<ButtonMenuController>().ResetController();

            // Go back to game menu view
            ViewManager.Instance.ShowLast();
        }
    }
}
