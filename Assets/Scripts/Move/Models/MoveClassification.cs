using System;
using MonsterTamer.Move.Enums;
using MonsterTamer.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Move.Models
{
    /// <summary>
    /// Defines how a move behaves in combat by specifying its elemental type
    /// and damage category (Physical, Special, or Status).
    /// </summary>
    [Serializable]
    internal struct MoveClassification
    {
        [SerializeField, Required, Tooltip("The elemental type of the move (e.g., Fire, Water, Electric).")]
        private TypeDefinition typeDefinition;

        [SerializeField, Tooltip("Determines whether the move is Physical, Special, or Status.")]
        private MoveCategory category;

        /// <summary>
        /// Gets the elemental type of the move.
        /// </summary>
        internal readonly TypeDefinition TypeDefinition => typeDefinition;

        /// <summary>
        /// Gets the move category (Physical, Special, or Status).
        /// </summary>
        internal readonly MoveCategory Category => category;
    }
}
