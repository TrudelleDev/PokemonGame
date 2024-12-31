namespace PokemonGame.Pokemons.UI.PartyMenu
{
    public class PartyMenuSlotManager
    {
        private readonly Party party;
        private readonly PartyMenuSlot[] partyMenuSlots;

        public PartyMenuSlotManager(Party party, PartyMenuSlot[] partyMenuSlots)
        {
            this.party = party;
            this.partyMenuSlots = partyMenuSlots;
        }

        public void Initialize()
        {
            // Disable every party slot
            for (int i = 0; i < partyMenuSlots.Length; i++)
            {
                partyMenuSlots[i].SetInteractable(false);


            }

            // Update the party menu
            for (int i = 0; i < party.Pokemons.Count; i++)
            {
                partyMenuSlots[i].Bind(party.Pokemons[i]);
            }
        }
    }
}
