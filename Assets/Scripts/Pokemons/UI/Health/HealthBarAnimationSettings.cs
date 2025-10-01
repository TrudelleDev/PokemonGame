using System;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Health
{
    /// <summary>
    /// Defines configuration values for health bar animation speed and delays.
    /// Used to control how quickly the health bar animates during damage and healing.
    /// </summary>
    [Serializable]
    public struct HealthBarAnimationSettings
    {
        [SerializeField]
        [Tooltip("Speed factor for damage animation. Higher = slower ticks.")]
        private float damageSpeedFactor;

        [SerializeField]
        [Tooltip("Speed factor for healing animation. Higher = slower ticks.")]
        private float healSpeedFactor;

        [SerializeField, Range(0f, 0.1f)]
        [Tooltip("Minimum tick delay (seconds) when animating health change.")]
        private float minTickDelay;

        [SerializeField, Range(0f, 0.1f)]
        [Tooltip("Maximum tick delay (seconds) when animating health change.")]
        private float maxTickDelay;

        /// <summary>
        /// Gets the damage animation speed factor.
        /// Higher values produce slower tick updates when losing HP.
        /// </summary>
        public readonly float DamageSpeedFactor => damageSpeedFactor;

        /// <summary>
        /// Gets the healing animation speed factor.
        /// Higher values produce slower tick updates when gaining HP.
        /// </summary>
        public readonly float HealthSpeedFactor => healSpeedFactor;

        /// <summary>
        /// Gets the minimum allowed delay (in seconds) between tick updates.
        /// Prevents the animation from running too fast.
        /// </summary>
        public readonly float MinTickDelay => minTickDelay;

        /// <summary>
        /// Gets the maximum allowed delay (in seconds) between tick updates.
        /// Prevents the animation from running too slow.
        /// </summary>
        public readonly float MaxTickDelay => maxTickDelay;
    }
}
