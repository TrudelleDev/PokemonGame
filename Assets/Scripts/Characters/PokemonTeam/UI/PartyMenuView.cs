using UnityEngine;

namespace PokemonGame.Characters.PokemonTeam.UI
{
    public class PartyMenuView : View
    {
        [SerializeField] private Party party;
        [SerializeField] private PartyMenuSlot[] partyMenuSlots;

        public override void Initialize()
        {
            // Disable every party slot
            for (int i = 0; i < partyMenuSlots.Length; i++)
            {
                partyMenuSlots[i].Initialize();
                partyMenuSlots[i].SetActive(false);
            }

        }

        private void OnEnable()
        {
            // Update the party menu
            for (int i = 0; i < party.Pokemons.Count; i++)
            {
                partyMenuSlots[i].Bind(party.Pokemons[i]);
            }
        }
    }
}
