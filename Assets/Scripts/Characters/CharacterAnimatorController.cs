using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Handles character animation control, including direction updates, walking steps, collisions, and refacing.
    /// Wraps around Unity's Animator for better separation of concerns.
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
        /// Constructs a new animation controller wrapper for the given Animator.
        /// </summary>
        /// <param name="animator">Animator component to control.</param>
        public CharacterAnimatorController(Animator animator)
        {
            this.animator = animator;
        }

        /// <summary>
        /// Updates animation parameters to match the given facing direction.
        /// </summary>
        /// <param name="direction">Movement or facing direction.</param>
        public void UpdateDirection(Direction direction)
        {
            Vector2Int dir = direction.ToVector();
            animator.SetFloat(VerticalParam, dir.y);
            animator.SetFloat(HorizontalParam, dir.x);
        }

        /// <summary>
        /// Plays alternating walking step animations to simulate movement.
        /// </summary>
        public void PlayWalkStep()
        {
            animator.SetTrigger(stepToggle ? WalkFirstStepTrigger : WalkSecondStepTrigger);
            stepToggle = !stepToggle;
        }

        /// <summary>
        /// Plays alternating collision animations for visual feedback on movement failure.
        /// </summary>
        public void PlayCollisionStep()
        {
            animator.SetTrigger(stepToggle ? CollideFirstStepTrigger : CollideSecondStepTrigger);
            stepToggle = !stepToggle;
        }

        /// <summary>
        /// Plays a turning-in-place animation without movement.
        /// </summary>
        public void PlayRefacing()
        {
            animator.SetTrigger(RefacingTrigger);
        }
    }
}
