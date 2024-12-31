using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Natures
{
    [Serializable]
    public class Nature
    {
        [SerializeField] private NatureData data;

        public NatureData Data => data;
    }
}
