using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves
{
    [Serializable]
    public class Move
    {
        [SerializeField] private MoveData data;

        public int PowerPointRemaining { get; private set; }
        public MoveData Data => data;

        public Move(MoveData data)
        {
            this.data = data;
            PowerPointRemaining = data.PowerPoint;
        }
    }
}
