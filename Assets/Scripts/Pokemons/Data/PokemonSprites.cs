using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    [Serializable]
    public struct PokemonSprites
    {
        [SerializeField] private Sprite frontSprite;
        [SerializeField] private Sprite backSprite;
        [SerializeField] private Sprite menuSprite;

        public readonly Sprite FrontSprite => frontSprite;
        public readonly Sprite BackSprite => backSprite;
        public readonly Sprite MenuSprite => menuSprite;
    }
}
