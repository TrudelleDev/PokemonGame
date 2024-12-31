using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves
{
    [Serializable]
    public struct Learnset
    {
        [SerializeField] private int level;
        [SerializeField] private MoveData moveData;

        public readonly int Level => level;
        public readonly MoveData MoveData => moveData;
    }
}