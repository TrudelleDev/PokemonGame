using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Models
{
    /// <summary>
    /// Contains a Pokémon’s primary type and an optional secondary type.
    /// </summary>
    [Serializable]
    public class PokemonType
    {
        [SerializeField, Required]
        [Tooltip("Primary type of the Pokémon.")]
        private TypeDefinition firstType;

        [SerializeField]
        [Tooltip("Whether the Pokémon has a secondary type.")]
        private bool hasSecondType;

        [SerializeField, Required]
        [ShowIf(nameof(hasSecondType))]
        [Tooltip("Secondary type of the Pokémon.")]
        private TypeDefinition secondType;

        /// <summary>
        /// Primary type of the Pokémon.
        /// </summary>
        public TypeDefinition FirstType => firstType;

        /// <summary>
        /// Secondary type of the Pokémon (if any).
        /// </summary>
        public TypeDefinition SecondType => secondType;

        /// <summary>
        /// Whether the Pokémon has a valid secondary type assigned.
        /// </summary>
        public bool HasSecondType => hasSecondType && secondType != null;
    }
}
