using System;
using PokemonGame.Pokemon;
using UnityEngine;

namespace PokemonGame.Party.Models
{
    /// <summary>
    /// Represents a single Monster entry in a trainer's party,
    /// including its species and starting level.
    /// </summary>
    [Serializable]
    public struct PartyMemberEntry
    {
        [SerializeField, Tooltip("Mosnter species for this party slot.")]
        private PokemonDefinition monsterDefinition;

        [SerializeField, Range(1, 100), Tooltip("Level of this Monster.")]
        private int level;

        /// <summary>
        /// The Monster species assigned to this party slot.
        /// </summary>
        public readonly PokemonDefinition MonsterDefinition => monsterDefinition;

        /// <summary>
        /// The level of this Monster.
        /// </summary>
        public readonly int Level => level;
    }
}
