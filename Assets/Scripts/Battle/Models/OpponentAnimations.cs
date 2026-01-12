using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.Models
{
    /// <summary>
    /// Contains references to all animators used for the opponent during battle,
    /// including the platform, Pokémon sprite, and HUD animations.
    /// </summary>
    [Serializable]
    public struct OpponentAnimations
    {
        [SerializeField, Required, Tooltip("Animator controlling the opponent's battle platform.")]
        private Animator platformAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's Pokémon sprite.")]
        private Animator pokemonAnimator;

        [SerializeField, Required] 
        private Animator trainerAnimator;

        [SerializeField, Required]
        private Animator pokemonBarAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's HUD display.")]
        private Animator hudAnimator;

        public Animator PlatformAnimator => platformAnimator;
        public Animator PokemonAnimator => pokemonAnimator;
        public Animator TrainerAnimator => trainerAnimator;

        public Animator PokemonBarAnimator => pokemonBarAnimator;
        public Animator HudAnimator => hudAnimator;
    }
}
