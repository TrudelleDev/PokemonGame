using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame
{
    public class Pokedex : MonoBehaviour
    {
        public const int TotalPokemon = 151;

        private readonly List<PokedexData> pokedex = new();

        public event Action<PokedexData> OnPokemonChange;

        public void AddData(PokedexData data)
        {
            pokedex.Add(data);
            OnPokemonChange?.Invoke(data);
        }
    }
}
