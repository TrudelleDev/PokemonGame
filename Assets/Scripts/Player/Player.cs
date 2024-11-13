using PokemonClone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame
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
                pokedex.AddData(new PokedexData(true, pokemon.PokemonData));
            }
        }
    }
}
