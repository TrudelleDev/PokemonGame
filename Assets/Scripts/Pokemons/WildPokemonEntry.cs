using System;
using PokemonGame.Pokemons.Definition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    /// <summary>
    /// Represents a wild Pokémon that can appear in a specific area,
    /// including its level range and encounter rate.
    /// </summary>
    [Serializable]
    public class WildPokemonEntry
    {
        [SerializeField, Required]
        [Tooltip("The Pokémon species definition for this wild encounter.")]
        private PokemonDefinition definition;

        [SerializeField, Required, Range(0, 100)]
        [Tooltip("The minimum level this wild Pokémon can appear at.")]
        private int minLevel;

        [SerializeField, Required, Range(0, 100)]
        [Tooltip("The maximum level this wild Pokémon can appear at.")]
        private int maxLevel;

        [SerializeField, Required, Range(0, 100)]
        [Tooltip("The encounter rate (percentage) for this Pokémon in the wild.")]
        private int encounterRate;

        /// <summary>
        /// Gets the Pokémon species definition for this wild encounter.
        /// </summary>
        public PokemonDefinition Pokemon => definition;

        /// <summary>
        /// Gets the minimum level this wild Pokémon can appear at.
        /// </summary>
        public int MinLevel => minLevel;

        /// <summary>
        /// Gets the maximum level this wild Pokémon can appear at.
        /// </summary>
        public int MaxLevel => maxLevel;

        /// <summary>
        /// Gets the encounter rate (percentage) for this Pokémon in the wild.
        /// </summary>
        public int EncounterRate => encounterRate;
    }
}
