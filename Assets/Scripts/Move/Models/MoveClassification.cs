using System;
using PokemonGame.Move.Enums;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Models
{
    /// <summary>
    /// Represents the classification of a Pokémon move, including its elemental type
    /// and category (Physical, Special, or Status).
    /// </summary>
    [Serializable]
    public struct MoveClassification
    {
        [SerializeField, Required, Tooltip("Type of the move (e.g., Fire, Water).")]
        private TypeDefinition typeDefinition;

        [SerializeField, Tooltip("Whether the move is Physical, Special, or Status.")]
        private MoveCategory category;

        public readonly TypeDefinition TypeDefinition => typeDefinition;
        public readonly MoveCategory Category => category;
    }
}
