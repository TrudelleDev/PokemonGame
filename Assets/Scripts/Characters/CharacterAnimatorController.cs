using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Enums.Extensions;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Wraps Unity's Animator to control character animations:
    /// idle, walking, collisions, and refacing.
    /// Provides a clean separation between animation logic and state handling.
    /// </summary>
    public class CharacterAnimatorController
    {
        private readonly Animator animator;
        private bool stepToggle;

        // Cached parameter hashes for performance
        private static readonly int VerticalParam = Animator.StringToHash("Vertical");
        private static readonly int HorizontalParam = Animator.StringToHash("Horizontal");
        private static readonly int RefacingTrigger = Animator.StringToHash("Reface");
        private static readonly int WalkFirstStepTrigger = Animator.StringToHash("WalkFirstStep");
        private static readonly int WalkSecondStepTrigger = Animator.StringToHash("WalkSecondStep");
        private static readonly int CollideFirstStepTrigger = Animator.StringToHash("CollideFirstStep");
        private static readonly int CollideSecondStepTrigger = Animator.StringToHash("CollideSecondStep");

        /// <summary>
        /// Creates a new wrapper for the provided <see cref="Animator"/>.
        /// </summary>
        /// <param name="animator">Animator component used for controlling character animations.</param>
        public CharacterAnimatorController(Animator animator)
        {
            this.animator = animator;
        }

        /// <summary>
        /// Updates animator parameters (Vertical/Horizontal) to match the given facing direction.
        /// </summary>
        public void UpdateDirection(FacingDirection direction)
        {
            Vector2Int dir = direction.ToVector2Int();
            animator.SetFloat(VerticalParam, dir.y);
            animator.SetFloat(HorizontalParam, dir.x);
        }

        /// <summary>
        /// Plays idle pose for the given direction (no trigger needed).
        /// </summary>
        public void PlayIdle(FacingDirection direction)
        {
            UpdateDirection(direction);
        }

        /// <summary>
        /// Plays alternating walk step triggers to animate walking motion.
        /// </summary>
        public void PlayWalkStep()
        {
            animator.SetTrigger(stepToggle ? WalkFirstStepTrigger : WalkSecondStepTrigger);
            stepToggle = !stepToggle;
        }

        /// <summary>
        /// Plays alternating collision step triggers for blocked movement feedback.
        /// </summary>
        public void PlayCollisionStep()
        {
            animator.SetTrigger(stepToggle ? CollideFirstStepTrigger : CollideSecondStepTrigger);
            stepToggle = !stepToggle;
        }

        /// <summary>
        /// Updates facing direction and plays the refacing animation.
        /// </summary>
        public void PlayRefacing(FacingDirection direction)
        {
            UpdateDirection(direction);
            animator.SetTrigger(RefacingTrigger);
        }
    }
}
