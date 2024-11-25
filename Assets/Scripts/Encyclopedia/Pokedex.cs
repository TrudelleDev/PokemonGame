using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Encyclopedia
{
    public class Pokedex : MonoBehaviour
    {
        public const int TotalPokemon = 151;
        public event Action<PokedexEntry> OnPokemonChange;
        private readonly List<PokedexEntry> pokedexEntries = new();

        public void AddData(PokedexEntry data)
        {
            pokedexEntries.Add(data);
            OnPokemonChange?.Invoke(data);
        }
    }
}
