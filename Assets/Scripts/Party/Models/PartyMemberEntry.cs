using System;
using PokemonGame.Pokemon;
using UnityEngine;

namespace PokemonGame.Party.Models
{
    /// <summary>
    /// Represents a single Pokémon entry in a trainer's party,
    /// including its species and starting level.
    /// </summary>
    [Serializable]
    public struct PartyMemberEntry
    {
        [SerializeField, Tooltip("Pokémon species for this party slot.")]
        private PokemonDefinition pokemonDefinition;

        [SerializeField, Range(1, 100), Tooltip("Level of this Pokémon.")]
        private int level;

        /// <summary>
        /// The Pokémon species assigned to this party slot.
        /// </summary>
        public readonly PokemonDefinition PokemonDefinition => pokemonDefinition;

        /// <summary>
        /// The level of this Pokémon.
        /// </summary>
        public readonly int Level => level;
    }
}
