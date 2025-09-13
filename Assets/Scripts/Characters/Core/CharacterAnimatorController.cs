using PokemonGame.Characters.Direction;
using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Wraps Unity's Animator to control character animations:
    /// idle, walking, collisions, and refacing.
    /// Provides a clean separation between animation logic and state handling.
    /// </summary>
    public class CharacterAnimatorController
    {
        private readonly Animator animator;
        private bool isFirstStep = true;

        // Cached parameter hashes for performance
        private static readonly int VerticalParam = Animator.StringToHash("Vertical");
        private static readonly int HorizontalParam = Animator.StringToHash("Horizontal");
        private static readonly int RefacingTrigger = Animator.StringToHash("Reface");
        private static readonly int WalkFirstStepTrigger = Animator.StringToHash("WalkFirstStep");
        private static readonly int WalkSecondStepTrigger = Animator.StringToHash("WalkSecondStep");
        private static readonly int CollideFirstStepTrigger = Animator.StringToHash("CollideFirstStep");
        private static readonly int CollideSecondStepTrigger = Animator.StringToHash("CollideSecondStep");

        /// <summary>
        /// Creates a new wrapper for the provided Animator.
        /// </summary>
        /// <param name="animator">Animator component used to control character animations.</param>
        public CharacterAnimatorController(Animator animator)
        {
            if (animator == null)
            {
                Log.Error(nameof(CharacterAnimatorController), "Animator is required but was null.");
                return;
            }

            this.animator = animator;
        }

        /// <summary>
        /// Updates animator parameters (Vertical/Horizontal) to match the given facing direction.
        /// </summary>
        /// <param name="facingDirection">The direction the character should face.</param>
        public void UpdateDirection(FacingDirection facingDirection)
        {
            Vector2Int direction = facingDirection.ToVector2Int();
            animator.SetFloat(VerticalParam, direction.y);
            animator.SetFloat(HorizontalParam, direction.x);
        }

        /// <summary>
        /// Plays idle pose for the given direction (no trigger needed).
        /// </summary>
        /// <param name="facingDirection">The direction the character should face while idle.</param>
        public void PlayIdle(FacingDirection facingDirection)
        {
            UpdateDirection(facingDirection);
        }

        /// <summary>
        /// Plays alternating walk step triggers to animate walking motion.
        /// </summary>
        public void PlayWalkStep()
        {
            if (isFirstStep)
            {
                animator.SetTrigger(WalkFirstStepTrigger);
            }
            else
            {
                animator.SetTrigger(WalkSecondStepTrigger);
            }

            isFirstStep = !isFirstStep;
        }

        /// <summary>
        /// Plays alternating collision step triggers for blocked movement feedback.
        /// </summary>
        public void PlayCollisionStep()
        {
            if (isFirstStep)
            {
                animator.SetTrigger(CollideFirstStepTrigger);
            }
            else
            {
                animator.SetTrigger(CollideSecondStepTrigger);
            }

            isFirstStep = !isFirstStep;
        }

        /// <summary>
        /// Updates facing direction and plays the refacing animation.
        /// </summary>
        /// <param name="facingDirection">The new direction the character should turn toward.</param>
        public void PlayRefacing(FacingDirection facingDirection)
        {
            UpdateDirection(facingDirection);
            animator.SetTrigger(RefacingTrigger);
        }
    }
}
