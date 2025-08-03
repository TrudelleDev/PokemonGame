using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Models
{
    /// <summary>
    /// Contains references for a Pokémon's visual representation: front, back, and menu.
    /// </summary>
    [Serializable]
    public struct PokemonSprites
    {
        [SerializeField, Required]
        [Tooltip("Front sprite used in battle (opponent) and summary views.")]
        private Sprite frontSprite;

        [SerializeField, Required]
        [Tooltip("Back sprite used in battle (player side).")]
        private Sprite backSprite;

        [SerializeField, Required]
        [Tooltip("Sprite used in menus like party and summary.")]
        private Sprite menuSprite;

        /// <summary>
        /// Front sprite used in battle (opponent side) and summary views.
        /// </summary>
        public readonly Sprite FrontSprite => frontSprite;

        /// <summary>
        /// Back sprite used in battle (player side).
        /// </summary>
        public readonly Sprite BackSprite => backSprite;

        /// <summary>
        /// Sprite used in menus such as party and summary views.
        /// </summary>
        public readonly Sprite MenuSprite => menuSprite;
    }
}
