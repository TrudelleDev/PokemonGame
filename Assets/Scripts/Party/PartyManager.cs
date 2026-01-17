using System;
using System.Collections.Generic;
using PokemonGame.Pokemon;
using PokemonGame.Utilities;

namespace PokemonGame.Party
{
    /// <summary>
    /// Runtime manager for a Pokémon party.
    /// Owns party state and battle-related logic.
    /// </summary>
    public sealed class PartyManager
    {
        public const int MaxPartySize = 6;

        private readonly List<PokemonInstance> members = new();
        private List<PokemonInstance> originalPartyOrder;

        /// <summary>
        /// The currently selected Pokémon in the party.
        /// </summary>
        public PokemonInstance SelectedPokemon { get; private set; }

        /// <summary>
        /// Gets the index of the currently selected Pokémon, or -1 if none.
        /// </summary>
        public int SelectedIndex =>
            SelectedPokemon != null ? members.IndexOf(SelectedPokemon) : -1;

        /// <summary>
        /// All Pokémon currently in the party.
        /// </summary>
        public IReadOnlyList<PokemonInstance> Members => members;

        /// <summary>
        /// Event fired when the party composition changes.
        /// </summary>
        public event Action PartyChanged;

        /// <summary>
        /// Creates a party manager from a predefined party definition.
        /// </summary>
        public PartyManager(PartyDefinition partyDefinition)
        {
            InitializeParty(partyDefinition);
        }

        private void InitializeParty(PartyDefinition partyDefinition)
        {
            if (partyDefinition == null || partyDefinition.Members.Count == 0)
            {
                Log.Warning(nameof(PartyManager), "No initial party members defined.");
                return;
            }

            foreach (var entry in partyDefinition.Members)
            {
                if (entry.PokemonDefinition == null)
                {
                    continue;
                }

                var pokemon = PokemonFactory.CreatePokemon(
                    entry.Level,
                    entry.PokemonDefinition
                );

                AddPokemon(pokemon);
            }

            if (members.Count > 0)
            {
                SelectedPokemon = members[0];
            }
        }

        public void AddPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null)
            {
                return;
            }

            if (members.Count >= MaxPartySize)
            {
                Log.Warning(nameof(PartyManager),
                    $"Cannot add more than {MaxPartySize} Pokémon.");
                return;
            }

            members.Add(pokemon);
            PartyChanged?.Invoke();
        }

        public void SaveOriginalPartyOrder()
        {
            originalPartyOrder = new List<PokemonInstance>(members);
        }

        public void RestorePartyOrder()
        {
            if (originalPartyOrder == null)
            {
                return;
            }

            members.Clear();
            members.AddRange(originalPartyOrder);

            PartyChanged?.Invoke();
        }

        public bool Swap(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= members.Count ||
                indexB < 0 || indexB >= members.Count ||
                indexA == indexB)
            {
                return false;
            }

            (members[indexA], members[indexB]) =
                (members[indexB], members[indexA]);

            PartyChanged?.Invoke();
            return true;
        }

        public void SelectPokemon(PokemonInstance pokemon)
        {
            if (pokemon == null || !members.Contains(pokemon))
            {
                return;
            }

            SelectedPokemon = pokemon;
        }

        public void SelectSlotIndex(int index)
        {
            if (index < 0 || index >= members.Count)
            {
                return;
            }

            SelectedPokemon = members[index];
        }

        public void HealAll()
        {
            foreach (var pokemon in members)
            {
                pokemon.Health.SetMaxHealth();
            }
        }

        public bool HasUsablePokemon()
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
