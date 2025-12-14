using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.Models
{
    /// <summary>
    /// Contains references to all animators used for the player during battle,
    /// including the platform, trainer sprite, active Pokémon, HUD, and Pokéball throw animations.
    /// </summary>
    [Serializable]
    public struct PlayerAnimations
    {
        [SerializeField, Required, Tooltip("Animator controlling the player's battle platform.")]
        private Animator platformAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's trainer sprite.")]
        private Animator trainerSpriteAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's active Pokémon sprite.")]
        private Animator pokemonAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's HUD display.")]
        private Animator hudAnimator;

        [SerializeField, Required, Tooltip("Animator used for the Pokéball throw animation.")]
        private Animator throwBallAnimator;

        public Animator PlatformAnimator => platformAnimator;
        public Animator TrainerSpriteAnimator => trainerSpriteAnimator;
        public Animator PokemonAnimator => pokemonAnimator;
        public Animator HudAnimator => hudAnimator;
        public Animator ThrowBallAnimator => throwBallAnimator;
    }
}
