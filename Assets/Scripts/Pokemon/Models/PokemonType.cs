using System;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemon.Models
{
    /// <summary>
    /// Contains a Pokémon’s primary type and an optional secondary type.
    /// </summary>
    [Serializable]
    public struct PokemonType
    {
        [SerializeField, Required, Tooltip("Primary type of the Pokémon.")]
        private TypeDefinition firstType;

        [SerializeField, Tooltip("Whether the Pokémon has a secondary type.")]
        private bool hasSecondType;

        [SerializeField, Required, Tooltip("Secondary type of the Pokémon.")]
        [ShowIf(nameof(hasSecondType))]
        private TypeDefinition secondType;

        public readonly TypeDefinition FirstType => firstType;
        public readonly TypeDefinition SecondType => secondType;
        public readonly bool HasSecondType => hasSecondType && secondType != null;
    }
}
