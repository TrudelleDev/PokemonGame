using System;
using System.Collections.Generic;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Manages a Pokémon party: initializes from a predefined set of PartyMemberEntry,
    /// maintains the active roster, and handles selection, adding, and removing members.
    /// </summary>
    public class PartyManager : MonoBehaviour
    {
        /// <summary>
        /// Maximum number of Pokémon allowed in a party.
        /// </summary>
        public const int MaxPartySize = 6;

        [SerializeField, Tooltip("Predefined party used to initialize this manager.")]
        private List<PartyMemberEntry> initialMembers;

        private readonly List<PokemonInstance> members = new();

        /// <summary>
        /// The currently selected Pokémon in the party.
        /// </summary>
        public PokemonInstance SelectedPokemon { get; private set; }

        /// <summary>
        /// All Pokémon currently in the party.
        /// </summary>
        public IReadOnlyList<PokemonInstance> Members => members;

        /// <summary>
        /// Event triggered when a new Pokémon is selected.
        /// </summary>
        public event Action<PokemonInstance> OnSelectPokemon;

        /// <summary>
        /// Event triggered when the party changes (add/remove).
        /// </summary>
        public event Action OnPartyChanged;

        private void Awake()
        {
            InitializeParty();
        }

        /// <summary>
        /// Initializes the party from the initial members list.
        /// Creates runtime Pokémon instances using the factory.
        /// </summary>
        private void InitializeParty()
        {
            if (initialMembers == null || initialMembers.Count == 0)
            {
                Debug.LogWarning("PartyManager: No initial members defined.");
                return;
            }

            members.Clear();

            foreach (var entry in initialMembers)
            {
                if (entry.PokemonDefinition == null) continue;

                PokemonInstance pokemon = PokemonFactory.CreatePokemon(entry.Level, entry.PokemonDefinition);
                AddPokemon(pokemon);
            }

            if (Members.Count > 0)
            {
                SelectPokemon(Members[0]);
            }
        }

        /// <summary>
        /// Selects the given Pokémon and triggers OnSelectPokemon.
        /// </summary>
        /// <param name="pokemon">The Pokémon to select.</param>
        public void SelectPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null || !members.Contains(pokemon)) return;

            SelectedPokemon = pokemon;
            OnSelectPokemon?.Invoke(pokemon);
        }

        /// <summary>
        /// Adds a Pokémon to the party.
        /// </summary>
        /// <param name="pokemon">The Pokémon to add.</param>
        public void AddPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null) return;

            if (members.Count >= MaxPartySize)
            {
                Debug.LogWarning("PartyManager: Cannot add more than " + MaxPartySize + " Pokémon.");
                return;
            }

            members.Add(pokemon);
            OnPartyChanged?.Invoke();
        }

        /// <summary>
        /// Removes a Pokémon from the party.
        /// </summary>
        /// <param name="pokemon">The Pokémon to remove.</param>
        public void RemovePokemon(PokemonInstance pokemon)
        {
            if (pokemon == null || !members.Contains(pokemon)) return;

            members.Remove(pokemon);
            OnPartyChanged?.Invoke();

            // If the removed Pokémon was selected, select the first one in the party
            if (SelectedPokemon == pokemon && Members.Count > 0)
            {
                SelectPokemon(Members[0]);
            }
        }

        /// <summary>
        /// Checks if the party is full.
        /// </summary>
        public bool IsFull() => members.Count >= MaxPartySize;
    }
}
