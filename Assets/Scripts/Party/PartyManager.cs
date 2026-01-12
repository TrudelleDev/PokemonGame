using System;
using System.Collections.Generic;
using PokemonGame.Pokemon;
using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Manages a Pokémon party: initializes from a predefined set of PartyMemberEntry,
    /// maintains the active roster, and handles selection, adding, and removing members.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyManager : MonoBehaviour
    {
        public const int MaxPartySize = 6;

        [SerializeField, Tooltip("Predefined party used to initialize this manager.")]
        private PartyDefinition partyDefinition;

        private readonly List<PokemonInstance> members = new();
        private List<PokemonInstance> originalPartyOrder = new();

        /// <summary>
        /// The currently selected Pokémon in the party.
        /// </summary>
        internal PokemonInstance SelectedPokemon { get; private set; }

        /// <summary>
        /// Gets the index of the currently selected Pokémon in the party,
        /// or -1 if no Pokémon is selected.
        /// </summary>
        internal int SelectedIndex => SelectedPokemon != null ? members.IndexOf(SelectedPokemon) : -1;

        /// <summary>
        /// All Pokémon currently in the party.
        /// </summary>
        internal IReadOnlyList<PokemonInstance> Members => members;

        /// <summary>
        /// Event triggered when the party changes (add/remove).
        /// </summary>
        internal event Action PartyChanged;

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
            if (partyDefinition == null || partyDefinition.Members.Count == 0)
            {
                Log.Warning(nameof(PartyManager), "PartyManager: No initial members defined.");
                return;
            }

            members.Clear();

            foreach (var entry in partyDefinition.Members)
            {
                if (entry.PokemonDefinition == null) continue;

                PokemonInstance pokemon = PokemonFactory.CreatePokemon(entry.Level, entry.PokemonDefinition);
                AddPokemon(pokemon);
            }

            // Automatically select the first Pokémon if the party is not empty
            if (Members.Count > 0)
            {
                SelectPokemon(Members[0]);
            }
        }

        /// <summary>
        /// Adds a Pokémon to the party if there is space and the Pokémon is not null,
        /// then triggers the PartyChanged event.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to add to the party.</param>
        internal void AddPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null)
            {
                return;
            }

            if (members.Count >= MaxPartySize)
            {
                Log.Warning(nameof(PartyManager), $"PartyManager: Cannot add more than {MaxPartySize} Pokemon.");
                return;
            }

            members.Add(pokemon);
            PartyChanged?.Invoke();
        }

        /// <summary>
        /// Saves the current party order, typically before entering a battle.
        /// </summary>
        internal void SaveOriginalPartyOrder()
        {
            originalPartyOrder = new List<PokemonInstance>(members);
        }

        /// <summary>
        /// Restores the party order saved before battle and triggers the PartyChanged event.
        /// </summary>
        internal void RestorePartyOrder()
        {
            if (originalPartyOrder == null)
            {
                return;
            }

            for (int i = 0; i < originalPartyOrder.Count; i++)
            {
                members[i] = originalPartyOrder[i];
            }

            PartyChanged?.Invoke();
        }

        /// <summary>
        /// Swaps two Pokémon in the party by their indices.
        /// </summary>
        /// <param name="indexA">Index of the first Pokémon.</param>
        /// <param name="indexB">Index of the second Pokémon.</param>
        /// <returns>
        /// True if the swap succeeds; false if the indices are invalid or identical.
        /// </returns>
        internal bool Swap(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= members.Count ||
                indexB < 0 || indexB >= members.Count)
            {
                return false;
            }

            if (indexA == indexB)
            {
                return false;
            }

            (members[indexB], members[indexA]) = (members[indexA], members[indexB]);

            PartyChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Sets the currently selected Pokémon if it exists in the party.
        /// </summary>
        /// <param name="pokemon">The Pokémon to select.</param>
        internal void SelectPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null || !members.Contains(pokemon))
            {
                return;
            }

            SelectedPokemon = pokemon;
        }

        /// <summary>
        /// Selects a Pokémon in the party by its slot index.
        /// </summary>
        /// <param name="index">Index of the Pokémon to select.</param>
        internal void SelectSlotIndex(int index)
        {
            if (index < 0 || index >= members.Count)
            {
                return;
            }

            SelectPokemon(members[index]);
        }

        /// <summary>
        /// Fully heals all Pokémon in the party (HP, status, etc.).
        /// </summary>
        public void HealAll()
        {
            foreach (var pokemon in Members)
            {
                pokemon.Health.SetMaxHealth();
            }
        }

        /// <summary>
        /// Checks whether the party has at least one Pokémon that can still battle.
        /// </summary>
        /// <returns>True if at least one Pokémon has remaining HP.</returns>
        internal bool HasUsablePokemon()
        {
            foreach (var pokemon in members)
            {
                if (pokemon.Health.CurrentHealth > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
