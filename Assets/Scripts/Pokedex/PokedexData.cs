using PokemonClone;
using System;

namespace PokemonGame
{
    public struct PokedexData
    {
        public  bool IsOwn { get; private set; }

        public PokemonData Data { get; private set; }

        public PokedexData(bool isOwn, PokemonData data)
        {
            IsOwn = isOwn;
            Data = data;
        }
    }
}
