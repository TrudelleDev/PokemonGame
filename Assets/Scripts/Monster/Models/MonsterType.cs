using System;
using MonsterTamer.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Monster.Models
{
    /// <summary>
    /// Represents a Monster's elemental typing.
    /// Contains a primary type and an optional secondary type.
    /// </summary>
    [Serializable]
    internal struct MonsterType
    {
        [SerializeField, Required, Tooltip("Primary type of the Monster.")]
        private TypeDefinition firstType;

        [SerializeField, Tooltip("Secondary elemental type (optional).")]
        private TypeDefinition secondType;

        internal TypeDefinition FirstType => firstType;
        internal TypeDefinition SecondType => secondType;
    }
}
