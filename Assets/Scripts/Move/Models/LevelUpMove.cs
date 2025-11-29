using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Models
{
    /// <summary>
    /// Defines a move learned by a Pokémon at a specific level,
    /// including the move's definition and the level requirement.
    /// </summary>
    [Serializable]
    public struct LevelUpMove
    {
        [SerializeField, Range(1, 100)]
        [Tooltip("The level at which the Pokémon learns this move.")]
        private int level;

        [SerializeField, Required]
        [Tooltip("The move definition that the Pokémon will learn at the specified level.")]
        private MoveDefinition moveDefinition;

        public readonly int Level => level;
        public readonly MoveDefinition MoveDefinition => moveDefinition;
    }
}
