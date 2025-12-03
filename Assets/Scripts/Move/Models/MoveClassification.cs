using System;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Models
{
    /// <summary>
    /// Defines the elemental type and damage category of a Pokémon move.
    /// The type determines effectiveness interactions (e.g., Fire vs Grass),
    /// while the category determines whether the move is Physical, Special, or Status.
    /// </summary>
    [Serializable]
    public struct MoveClassification
    {
        [SerializeField, Required]
        [Tooltip("The elemental type of the move (e.g., Fire, Water, Electric).")]
        private TypeDefinition typeDefinition;

        [SerializeField, Required]
        [Tooltip("The damage category of the move (Physical, Special, or Status).")]
        private MoveCategoryDefinition categoryDefinition;

        public readonly TypeDefinition TypeDefinition => typeDefinition;
        public readonly MoveCategoryDefinition CategoryDefinition => categoryDefinition;
    }
}
