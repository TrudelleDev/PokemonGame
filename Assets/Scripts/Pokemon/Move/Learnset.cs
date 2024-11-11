using System;
using UnityEngine;

namespace PokemonClone
{
    [Serializable]
    public struct Learnset
    {
        [field: SerializeField] public int Level { get; private set; }

        [field: SerializeField] public MoveData MoveData { get; private set; }
    }
}