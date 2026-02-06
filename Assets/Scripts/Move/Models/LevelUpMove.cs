using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Models
{
    /// <summary>
    /// Represents a move a Monster learns at a specific level.
    /// Includes the move definition and the required level to learn it.
    /// </summary>
    [Serializable]
    internal struct LevelUpMove
    {
        [SerializeField, Range(1, 100)]
        [Tooltip("The level at which the Monster learns this move.")]
        private int level;

        [SerializeField, Required]
        [Tooltip("The move definition that the Monster will learn at the specified level.")]
        private MoveDefinition moveDefinition;

        /// <summary>
        /// The level at which the Monster learns this move.
        /// </summary>
        internal readonly int Level => level;

        /// <summary>
        /// The move definition the Monster learns.
        /// </summary>
        internal readonly MoveDefinition MoveDefinition => moveDefinition;
    }
}
