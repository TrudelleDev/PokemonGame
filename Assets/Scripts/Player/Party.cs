using PokemonClone;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private List<Pokemon> pokemons = new();

        public IReadOnlyList<Pokemon> Pokemons => pokemons;

        private void Start()
        {
            foreach (Pokemon pokemon in pokemons)
            {
                if (pokemon != null)
                {
                    Debug.Log(pokemon.PokemonData.PokemonName);
                }
            }
        }

        public void AddPokemon(Pokemon pokemon)
        {
            pokemons.Add(pokemon);
        }

        public void RemovePokemon(Pokemon pokemon)
        {
            pokemons.Remove(pokemon);
        }
    }
}
