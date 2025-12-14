using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.Models
{
    /// <summary>
    /// Holds references to core battle components, such as animations and audio.
    /// Used by <see cref="BattleView"/> to manage battle visuals and sound effects.
    /// </summary>
    [Serializable]
    public struct BattleComponents
    {
        [SerializeField, Required, Tooltip("Controls all battle animations for player and opponent Pokémon.")]
        private BattleAnimation animation;

        [SerializeField, Required, Tooltip("Handles all battle-related audio effects and music.")]
        private BattleAudio audio;

        public BattleAnimation Animation => animation;
        public BattleAudio Audio => audio;
    }
}
