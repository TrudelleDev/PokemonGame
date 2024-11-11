using System;
using UnityEngine;

namespace PokemonClone
{
    [Serializable]
    public struct PokemonBaseStats
    {
        [field: SerializeField] public int HealthPoint { get; private set; }

        [field: SerializeField] public int Attack { get; private set; }

        [field: SerializeField] public int Defense { get; private set; }

        [field: SerializeField] public int SpecialAttack { get; private set; }

        [field: SerializeField] public int SpecialDefense { get; private set; }

        [field: SerializeField] public int Speed { get; private set; }

    }
}