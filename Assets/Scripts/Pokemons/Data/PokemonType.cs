using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Represents the primary and optional secondary type of a Pokémon.
    /// </summary>
    [Serializable]
    public class PokemonType
    {
        [SerializeField, Required]
        [Tooltip("The primary type of the Pokémon.")]
        private TypeData firstType;
      
        [SerializeField]
        [Tooltip("Enable to assign a secondary type.")]
        private bool hasSecondType;

        [SerializeField, Required]     
        [ShowIf(nameof(hasSecondType))]
        [Tooltip("The optional secondary type.")]
        private TypeData secondType;

        public TypeData FirstType => firstType;
        public TypeData SecondType => secondType;
        public bool HasSecondType => hasSecondType && secondType != null;
    }
}
