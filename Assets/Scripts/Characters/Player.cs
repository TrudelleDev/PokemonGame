using PokemonGame.Encyclopedia;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private string trainerName;

        private Party party;
        private Pokedex pokedex;

        public string TrainerName => trainerName;


        private void Awake()
        {
            party = GetComponent<Party>();
            pokedex = GetComponent<Pokedex>();
        }

        private void Start()
        {
            foreach (Pokemon pokemon in party.Pokemons)
            {
                pokemon.OwnerName = trainerName;
                pokedex.AddData(new PokedexEntry(true, pokemon.Data));
            }
        }
    }
}
