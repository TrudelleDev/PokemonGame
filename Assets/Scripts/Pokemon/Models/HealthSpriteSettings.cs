using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Pokemon.Models
{
    /// <summary>
    /// Defines the set of sprites used to represent different health states
    /// (high, moderate, and low) in a health bar.
    /// </summary>
    [Serializable]
    public struct HealthSpriteSettings
    {
        [SerializeField, Required, Tooltip("Sprite for high health state (above threshold).")]
        private Sprite highHealthSprite;

        [SerializeField, Required, Tooltip("Sprite for moderate health state (between thresholds).")]
        private Sprite moderateHealthSprite;

        [SerializeField, Required, Tooltip("Sprite for low health state (below threshold).")]
        private Sprite lowHealthSprite;

        public readonly Sprite HighHealthSprite => highHealthSprite;
        public readonly Sprite ModerateHealthSprite => moderateHealthSprite;
        public readonly Sprite LowHealthSprite => lowHealthSprite;
    }
}
