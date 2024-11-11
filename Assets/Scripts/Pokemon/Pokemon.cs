using PokemonGame;
using System;
using UnityEngine;

namespace PokemonClone
{
    [Serializable]
    public class Pokemon
    {
        [field: SerializeField] public int Level { get; private set; }

        [field: SerializeField] public PokemonData PokemonData { get; private set; }

        [field: SerializeField] public NatureData NatureData { get; private set; }

        [field: SerializeField] public AbilityData AbilityData { get; private set; }
    }
}

