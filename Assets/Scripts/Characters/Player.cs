using PokemonGame.Characters.PokemonTeam;
using PokemonGame.Encyclopedia;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class Player : MonoBehaviour
    {
        private Party party;
        private Pokedex pokedex;


        private void Awake()
        {
            party = GetComponent<Party>();
            pokedex = GetComponent<Pokedex>();
        }

        private void Start()
        {
            foreach (Pokemon pokemon in party.Pokemons)
            {
                pokedex.AddData(new PokedexEntry(true, pokemon.PokemonData));
            }
        }
    }
}
