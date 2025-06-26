using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    [Serializable]
    public struct PokemonGenderRatio
    {
        [SerializeField] private float maleRatio;
        [SerializeField, ReadOnly] private float femaleRatio;

        public readonly float MaleRatio => maleRatio;
        public readonly float FemaleRatio => femaleRatio;

        public PokemonGenderRatio(float maleRatio)
        {
            this.maleRatio = maleRatio;
            this.femaleRatio = 100 - maleRatio;
        }
    }
}
