using System;
using UnityEngine;

namespace MonsterTamer.Pokemon.Models
{
    /// <summary>
    /// Defines configuration values for health bar animation speed and delays.
    /// Used to control how quickly the health bar animates during damage and healing.
    /// </summary>
    [Serializable]
    public struct HealthBarAnimationSettings
    {
        [SerializeField, Tooltip("Speed factor for damage animation. Higher = slower ticks.")]
        private float damageSpeedFactor;

        [SerializeField, Tooltip("Speed factor for healing animation. Higher = slower ticks.")]
        private float healSpeedFactor;

        [SerializeField, Range(0f, 0.1f), Tooltip("Minimum tick delay (seconds) when animating health change.")]
        private float minTickDelay;

        [SerializeField, Range(0f, 0.1f), Tooltip("Maximum tick delay (seconds) when animating health change.")]
        private float maxTickDelay;

        public readonly float DamageSpeedFactor => damageSpeedFactor;
        public readonly float HealthSpeedFactor => healSpeedFactor;
        public readonly float MinTickDelay => minTickDelay;
        public readonly float MaxTickDelay => maxTickDelay;
    }
}
