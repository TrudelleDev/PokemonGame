using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemon.Models
{
    /// <summary>
    /// Represents a wild Pokémon that can appear in a specific area,
    /// including its level range and encounter rate.
    /// </summary>
    [Serializable]
    public struct WildPokemonEntry
    {
        [SerializeField, Required, Tooltip("The Pokémon species definition for this wild encounter.")]
        private PokemonDefinition definition;

        [SerializeField, Range(1, 100), Tooltip("The minimum level this wild Pokémon can appear at.")]
        private int minLevel;

        [SerializeField, Range(1, 100), Tooltip("The maximum level this wild Pokémon can appear at.")]
        private int maxLevel;

        [SerializeField, Range(1, 100), Tooltip("The encounter rate (percentage) for this Pokémon in the wild.")]
        private int encounterRate;

        public readonly PokemonDefinition Pokemon => definition;
        public readonly int MinLevel => minLevel;
        public readonly int MaxLevel => maxLevel;
        public readonly int EncounterRate => encounterRate;
    }
}
