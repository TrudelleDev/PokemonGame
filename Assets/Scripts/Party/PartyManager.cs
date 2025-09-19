using System;
using System.Collections.Generic;
using PokemonGame.Pokemons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Manages the lifecycle of a Pok�mon party: initializes from a definition,
    /// maintains the active roster, and handles member selection.
    /// </summary>
    public class PartyManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Predefined party used to initialize this manager.")]
        private PartyDefinition initialPartyDefinition;

        private readonly List<Pokemon> members = new();

        /// <summary>
        /// The currently selected Pok�mon in the party.
        /// </summary>
        public Pokemon SelectedPokemon { get; private set; }

        /// <summary>
        /// All Pok�mon currently in the party.
        /// </summary>
        public IReadOnlyList<Pokemon> Members => members;

        /// <summary>
        /// Event triggered when a new Pok�mon is selected.
        /// </summary>
        public event Action<Pokemon> OnSelectPokemon;

        private void Awake()
        {
            if (initialPartyDefinition == null)
            {
                return;
            }

            foreach (Pokemon pokemon in initialPartyDefinition.Members)
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
            members.Add(pokemon);
        }
    }
}
