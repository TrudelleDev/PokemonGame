using PokemonGame.Pokemons;
using System.Collections.Generic;
using UnityEngine;


namespace PokemonGame.Characters.PokemonTeam
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private List<Pokemon> startingPokemons = new();

        public Pokemon SelectedPokemon { get; private set; }

        public IReadOnlyList<Pokemon> Pokemons => pokemons;

        private readonly List<Pokemon> pokemons = new();

        private void Awake()
        {
            // Add every pokemon from the inspector to the pokemon list
            foreach (Pokemon pokemon in startingPokemons)
            {
                if (pokemon != null)
                {
                    AddPokemon(new Pokemon(
                        pokemon.Level,
                        pokemon.PokemonData,
                        pokemon.NatureData,
                        pokemon.AbilityData
                        ));
                }
            }
        }

        public void SelectPokemon(int index)
        {
            SelectedPokemon = pokemons[index];
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
