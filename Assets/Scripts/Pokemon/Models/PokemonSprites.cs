using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemon.Models
{
    /// <summary>
    /// Contains references for a Pokémon's visual representation: front, back, and menu.
    /// </summary>
    [Serializable]
    public struct PokemonSprites
    {
        [SerializeField, Required, Tooltip("Front sprite used in battle (opponent) and summary views.")]
        private Sprite frontSprite;

        [SerializeField, Required, Tooltip("Back sprite used in battle (player side).")]
        private Sprite backSprite;

        [SerializeField, Required, Tooltip("Sprite used in menus like party and summary.")]
        private Sprite menuSprite;

        public readonly Sprite FrontSprite => frontSprite;
        public readonly Sprite BackSprite => backSprite;
        public readonly Sprite MenuSprite => menuSprite;
    }
}
