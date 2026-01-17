using PokemonGame.Characters.Directions;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Controls character animations (idle, walk, collision, refacing) via Animator.
    /// Separates animation logic from state handling.
    /// </summary>
    public sealed class CharacterAnimatorController
    {
        private static readonly int VerticalParam = Animator.StringToHash("Vertical");
        private static readonly int HorizontalParam = Animator.StringToHash("Horizontal");
        private static readonly int RefacingTrigger = Animator.StringToHash("Reface");
        private static readonly int WalkFirstStepTrigger = Animator.StringToHash("WalkFirstStep");
        private static readonly int WalkSecondStepTrigger = Animator.StringToHash("WalkSecondStep");
        private static readonly int CollideFirstStepTrigger = Animator.StringToHash("CollideFirstStep");
        private static readonly int CollideSecondStepTrigger = Animator.StringToHash("CollideSecondStep");

        private readonly Animator animator;
        private bool isWalkingFirstStep = true;
        private bool isCollisionFirstStep = true;

        /// <summary>
        /// Initializes the controller with the given Animator.
        /// Throws if animator is null.
        /// </summary>
        /// <param name="animator">Animator component used to control animations.</param>
        public CharacterAnimatorController(Animator animator)
        {
            if (animator == null)
            {
                throw new System.ArgumentNullException(nameof(animator), "Animator is required but was null.");
            }

            this.animator = animator;
        }

        /// <summary>
        /// Updates the Animator's Vertical and Horizontal parameters to match the given direction.
        /// </summary>
        /// <param name="facingDirection">Direction the character should face.</param>
        public void UpdateDirection(FacingDirection facingDirection)
        {
            Vector2Int dir = facingDirection.ToVector2Int();
            animator.SetFloat(VerticalParam, dir.y);
            animator.SetFloat(HorizontalParam, dir.x);
        }

        /// <summary>
        /// Sets the idle pose for the given direction.
        /// </summary>
        /// <param name="facingDirection">Direction the character should face while idle.</param>
        internal void PlayIdle(FacingDirection facingDirection)
        {
            UpdateDirection(facingDirection);
        }

        /// <summary>
        /// Plays the next walking step trigger (alternates first/second step).
        /// </summary>
        public void PlayWalkStep()
        {
            animator.SetTrigger(isWalkingFirstStep ? WalkFirstStepTrigger : WalkSecondStepTrigger);
            isWalkingFirstStep = !isWalkingFirstStep;
        }

        /// <summary>
        /// Plays the next collision step trigger (alternates first/second step).
        /// </summary>
        public void PlayCollisionStep()
        {
            animator.SetTrigger(isCollisionFirstStep ? CollideFirstStepTrigger : CollideSecondStepTrigger);
            isCollisionFirstStep = !isCollisionFirstStep;
        }

        /// <summary>
        /// Updates facing direction and plays the refacing animation.
        /// </summary>
        /// <param name="facingDirection">Direction the character should face.</param>
        public void PlayRefacing(FacingDirection facingDirection)
        {
            UpdateDirection(facingDirection);
            animator.SetTrigger(RefacingTrigger);
        }
    }
}
