using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Models
{
    /// <summary>
    /// Contains a Pokémon's gender distribution as male and female ratios.
    /// </summary>
    [Serializable]
    public struct PokemonGenderRatio
    {
        [SerializeField, Range(0, 100)]
        [Tooltip("Percentage chance of the Pokémon being male.")]
        private float maleRatio;

        /// <summary>
        /// Percentage chance of the Pokémon being female (100 - MaleRatio).
        /// </summary>
        [ShowInInspector, ReadOnly]
        [Tooltip("Percentage chance of the Pokémon being female.")]
        public readonly float FemaleRatio => 100f - maleRatio;

        /// <summary>
        /// Percentage chance of the Pokémon being male.
        /// </summary>
        public readonly float MaleRatio => maleRatio;   
    }
}
