using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Monster.Models
{
    /// <summary>
    /// Defines the set of sprites used to represent different health states
    /// (high, moderate, and low) in a health bar.
    /// </summary>
    [Serializable]
    internal struct HealthSpriteSettings
    {
        [SerializeField, Required, Tooltip("Sprite for high health state (above threshold).")]
        private Sprite highHealthSprite;

        [SerializeField, Required, Tooltip("Sprite for moderate health state (between thresholds).")]
        private Sprite moderateHealthSprite;

        [SerializeField, Required, Tooltip("Sprite for low health state (below threshold).")]
        private Sprite lowHealthSprite;

        internal Sprite HighHealthSprite => highHealthSprite;
        internal Sprite ModerateHealthSprite => moderateHealthSprite;
        internal Sprite LowHealthSprite => lowHealthSprite;
    }
}
