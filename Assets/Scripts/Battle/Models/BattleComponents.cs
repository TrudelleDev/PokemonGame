using System;
using MonsterTamer.Battle.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.Models
{
    /// <summary>
    /// Holds references to core battle components, such as animations and audio.
    /// Used by <see cref="BattleView"/> to manage battle visuals and sound effects.
    /// </summary>
    [Serializable]
    internal struct BattleComponents
    {
        [SerializeField, Required, Tooltip("Controls all battle animations for player and opponent Monster.")]
        private BattleAnimation animation;

        [SerializeField, Required, Tooltip("Handles all battle-related audio effects and music.")]
        private BattleAudio audio;

        /// <summary>
        /// Controls all battle animations for player and opponent Monster.
        /// </summary>
        internal readonly BattleAnimation Animation => animation;

        /// <summary>
        /// Handles all battle-related audio effects and music.
        /// </summary>
        internal readonly BattleAudio Audio => audio;
    }
}
