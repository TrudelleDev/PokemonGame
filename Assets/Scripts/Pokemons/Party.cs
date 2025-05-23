using System;
using System.Collections.Generic;
using UnityEngine;


namespace PokemonGame.Pokemons
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private List<Pokemon> startingPokemons = new();

        public Pokemon SelectedPokemon { get; private set; }

        public IReadOnlyList<Pokemon> Pokemons => pokemons;

        private readonly List<Pokemon> pokemons = new();

        public event Action<Pokemon> OnSelectPokemon;

        private void Awake()
        {
            // Add every pokemon from the inspector to the pokemon list
            foreach (Pokemon pokemon in startingPokemons)
            {
                if (pokemon != null)
                {
                    AddPokemon(new Pokemon(
                        pokemon.Level,
                        pokemon.Data,
                        pokemon.Nature,
                        pokemon.Ability,
                        pokemon.Moves
                        ));
                }
            }
        }

        public void SelectPokemon(Pokemon pokemon)
        {
            SelectedPokemon = pokemon;
            OnSelectPokemon?.Invoke(pokemon);
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
