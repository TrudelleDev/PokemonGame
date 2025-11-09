using System;
using PokemonGame.Moves.Definition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Moves
{
    /// <summary>
    /// Represents a move that a Pokémon can learn at a specific level.
    /// </summary>
    [Serializable]
    public struct LevelUpMove
    {
        [SerializeField, Required, Range(1, 100)]
        [Tooltip("The level at which the Pokémon learns this move.")]
        private int level;

        [SerializeField, Required]
        [Tooltip("The move definition that the Pokémon will learn at the specified level.")]
        private MoveDefinition definition;

        /// <summary>
        /// Gets the level at which the Pokémon learns this move.
        /// </summary>
        public readonly int Level => level;

        /// <summary>
        /// Gets the move definition associated with this level-up move.
        /// </summary>
        public readonly MoveDefinition Definition => definition;
    }
}
