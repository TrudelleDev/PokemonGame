using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Health
{
    /// <summary>
    /// Defines the set of sprites used to represent different health states
    /// (high, moderate, and low) in a health bar.
    /// </summary>
    [Serializable]
    public struct HealthSpriteSettings
    {
        [SerializeField, Required]
        [Tooltip("Sprite for high health state (above threshold).")]
        private Sprite highHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite for moderate health state (between thresholds).")]
        private Sprite moderateHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite for low health state (below threshold).")]
        private Sprite lowHealthSprite;

        /// <summary>
        /// Gets the sprite displayed when health is above the high threshold.
        /// </summary>
        public readonly Sprite HighHealthSprite => highHealthSprite;

        /// <summary>
        /// Gets the sprite displayed when health is between the high and low thresholds.
        /// </summary>
        public readonly Sprite ModerateHealthSprite => moderateHealthSprite;

        /// <summary>
        /// Gets the sprite displayed when health is below the low threshold.
        /// </summary>
        public readonly Sprite LowHealthSprite => lowHealthSprite;
    }
}
