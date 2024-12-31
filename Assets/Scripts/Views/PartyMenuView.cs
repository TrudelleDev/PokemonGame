using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.PartyMenu;
using UnityEngine;

namespace PokemonGame.Views
{
    public class PartyMenuView : View
    {
        [SerializeField] private Party party;
        [SerializeField] private PartyMenuSlot[] partyMenuSlots;

        private PartyMenuSlotManager partyMenuSlotManager;

        public override void Initialize() { }

        private void Start()
        {
            partyMenuSlotManager = new PartyMenuSlotManager(party, partyMenuSlots);
            partyMenuSlotManager.Initialize();
        }
    }
}
