using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Type
{
    /// <summary>
    /// Stores the audio clips to play for each type effectiveness category.
    /// </summary>
    [Serializable]
    public struct TypeEffectivenessSounds
    {
        [SerializeField, Required, Tooltip("Sound to play when a move deals normal effectiveness damage.")]
        private AudioClip normalSound;

        [SerializeField, Required, Tooltip("Sound to play when a move is super effective against the target.")]
        private AudioClip superEffectiveSound;

        [SerializeField, Required, Tooltip("Sound to play when a move is not very effective against the target.")]
        private AudioClip notVeryEffectiveSound;

        /// <summary>
        /// Returns the appropriate AudioClip for the given type effectiveness.
        /// </summary>
        /// <param name="effectiveness">The type effectiveness category.</param>
        /// <returns>The AudioClip associated with the effectiveness, or null if undefined.</returns>
        public readonly AudioClip GetEffectivenessSound(TypeEffectiveness effectiveness)
        {
            return effectiveness switch
            {
                TypeEffectiveness.SuperEffective    => superEffectiveSound,
                TypeEffectiveness.NotVeryEffective  => notVeryEffectiveSound,
                TypeEffectiveness.Normal            => normalSound,
                _                                   => null
            };
        }
    }
}
