using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Represents the gender distribution for a Pokémon species,
    /// defined by a male ratio (0–100). The female ratio is automatically derived.
    /// </summary>
    [Serializable]
    public struct PokemonGenderRatio
    {
        [SerializeField, Range(0, 100)]
        [Tooltip("Percentage chance of the Pokémon being male.")]
        private float maleRatio;

        [ShowInInspector, ReadOnly]
        [Tooltip("Percentage chance of the Pokémon being female.")]
        public readonly float FemaleRatio => 100f - maleRatio;

        public readonly float MaleRatio => maleRatio;   
    }
}
