using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.Models
{
    /// <summary>
    /// Contains references to all animators used for the player during battle,
    /// including Monster sprite, and HUD animations.
    /// </summary>
    [Serializable]
    internal struct PlayerAnimations
    {
        [SerializeField, Required, Tooltip("Animator controlling the player's Monster sprite.")]
        private Animator monsterAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's trainer sprite.")]
        private Animator trainerAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's HUD display.")]
        private Animator hudAnimator;

        /// <summary>
        /// Animator controlling the player's trainer sprite.
        /// </summary>
        internal readonly Animator MonsterAnimator => monsterAnimator;

        /// <summary>
        /// Animator controlling the player's Monster sprite.
        /// </summary>
        internal readonly Animator TrainerSpriteAnimator => trainerAnimator;

        /// <summary>
        /// Animator controlling the player's HUD display.
        /// </summary>
        internal readonly Animator HudAnimator => hudAnimator;
    }
}
