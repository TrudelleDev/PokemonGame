using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Contains sprite references for a Pokémon's visual representation,
    /// including front (battle), back (player view), and menu icons.
    /// </summary>
    [Serializable]
    public struct PokemonSprites
    {
        [SerializeField, Required]
        [Tooltip("Front-facing sprite used in battle.")]
        private Sprite frontSprite;

        [SerializeField, Required]
        [Tooltip("Back-facing sprite used when the Pokémon is on the player's side.")]
        private Sprite backSprite;

        [SerializeField, Required]
        [Tooltip("Sprite used in menus, such as party or summary views.")]
        private Sprite menuSprite;

        public readonly Sprite FrontSprite => frontSprite;
        public readonly Sprite BackSprite => backSprite;
        public readonly Sprite MenuSprite => menuSprite;
    }
}
