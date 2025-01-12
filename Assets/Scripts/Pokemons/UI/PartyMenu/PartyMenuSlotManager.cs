using UnityEngine;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    public class PartyMenuSlotManager
    {
        private Party party;

        public PartyMenuSlotManager(Party party)
        {
            this.party = party;
        }

        public void Initialize(Transform transform)
        {
            // Disable every party menu slots
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<PartyMenuSlot>() != null)
                {
                    transform.GetChild(i).GetComponent<PartyMenuSlot>().SetActive(false);
                }
            }

            // Enable a party menu slot for every pokemon in the party and bind his data
            for (int i = 0; i < party.Pokemons.Count; i++)
            {
                transform.GetChild(i).GetComponent<PartyMenuSlot>().Bind(party.Pokemons[i]);
            }
        }
    }
}
