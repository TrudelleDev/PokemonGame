using PokemonGame.Attributes;
using PokemonGame.Pokemons;
using System;
using UnityEngine;

namespace PokemonGame
{
    [Serializable]
    public class PokemonType
    {
        [SerializeField] private TypeData firstType;
        [DrawIf("hasSecondType", true)]
        [SerializeField] private TypeData secondType;
        [SerializeField] private bool hasSecondType;

        public TypeData FirstType => firstType;
        public TypeData SecondType => secondType;
        public bool HasSecondType => hasSecondType;
    }
}

