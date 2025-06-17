using System;
using System.Collections.Generic;
using UnityEngine;


namespace PokemonGame.Pokemons
{
    /// <summary>
    /// Manages a party of Pok�mon, including selection, initialization, and access.
    /// </summary>
    public class Party : MonoBehaviour
    {
        [SerializeField] private List<Pokemon> startingParty = new();

        private readonly List<Pokemon> pokemons = new();

        public Pokemon SelectedPokemon { get; private set; }

        public IReadOnlyList<Pokemon> Pokemons => pokemons;

        public event Action<Pokemon> OnSelectPokemon;

        private void Awake()
        {
            foreach (Pokemon pokemon in startingParty)
            {
                AddPokemon(pokemon.Clone());
            }
        }

        /// <summary>
        /// Selects the given Pok�mon and notifies listeners.
        /// </summary>
        /// <param name="pokemon">The Pok�mon to select.</param>
        public void SelectPokemon(Pokemon pokemon)
        {
            SelectedPokemon = pokemon;
            OnSelectPokemon?.Invoke(pokemon);
        }

        /// <summary>
        /// Adds a Pok�mon to the party.
        /// </summary>
        /// <param name="pokemon">The Pok�mon to add.</param>
        public void AddPokemon(Pokemon pokemon)
        {
            pokemons.Add(pokemon);
        }
    }
}
