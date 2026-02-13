using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.Models
{
    /// <summary>
    /// Contains references to all animators used for the opponent during battle,
    /// including Monster sprite, and HUD animations.
    /// </summary>
    [Serializable]
    internal struct OpponentAnimations
    {
        [SerializeField, Required, Tooltip("Animator controlling the wild or opponent's Monster sprite.")]
        private Animator monsterAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's trainer sprite")]
        private Animator trainerAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's HUD display.")]
        private Animator hudAnimator;

        internal readonly Animator MonsterAnimator => monsterAnimator;
        internal readonly Animator TrainerAnimator => trainerAnimator;
        internal readonly Animator HudAnimator => hudAnimator;
    }
}
