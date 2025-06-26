using System;
using PokemonGame.Pokemons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    [Serializable]
    public class PokemonType
    {
        [SerializeField] private TypeData firstType;
        [ShowIf("hasSecondType", true)]
        [SerializeField] private TypeData secondType;
        [SerializeField] private bool hasSecondType;

        public TypeData FirstType => firstType;
        public TypeData SecondType => secondType;
        public bool HasSecondType => hasSecondType;
    }
}

