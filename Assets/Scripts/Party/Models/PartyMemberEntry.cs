using System;
using MonsterTamer.Monster;
using UnityEngine;

namespace MonsterTamer.Party.Models
{
    /// <summary>
    /// Represents a single Monster entry in a trainer's party,
    /// including its species and starting level.
    /// </summary>
    [Serializable]
    internal struct PartyMemberEntry
    {
        [SerializeField, Tooltip("Mosnter species for this party slot.")]
        private MonsterDefinition monsterDefinition;

        [SerializeField, Range(1, 100), Tooltip("Level of this Monster.")]
        private int level;

        /// <summary>
        /// The Monster species assigned to this party slot.
        /// </summary>
        public readonly MonsterDefinition MonsterDefinition => monsterDefinition;

        /// <summary>
        /// The level of this Monster.
        /// </summary>
        public readonly int Level => level;
    }
}
