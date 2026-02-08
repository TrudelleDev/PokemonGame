using MonsterTamer.Audio;
using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Interfaces;

namespace MonsterTamer.Characters.States
{
    /// <summary>
    /// State entered when the character attempts to move into a blocked tile.
    /// Plays a collision animation, triggers optional SFX, and decides the next state.
    /// </summary>
    internal sealed class CharacterCollisionState : ICharacterState
    {
        private readonly CharacterStateController controller;

        public CharacterCollisionState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            controller.AnimatorController.PlayCollisionStep();

            if (controller.CollisionAudioClip != null)
            {
                AudioManager.Instance.PlaySFX(controller.CollisionAudioClip);
            }
        }

        public void Update() { }

        public void Exit() { }

        /// <summary>
        /// Called by an animation event when the collision step finishes.
        /// Decides the next state based on current input and tile availability.
        /// </summary>
        public void OnCollisionComplete()
        {
            InputDirection currentDirection = controller.Input.CurrentDirection;

            if (currentDirection == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
                return;
            }

            // Update facing direction to match attempted input
            FacingDirection desiredFacingDirection = currentDirection.ToFacingDirection();
            controller.FacingDirection = desiredFacingDirection;

            // Retry movement if possible, otherwise re-enter collision state
            if (controller.TileMover.CanMoveInDirection(desiredFacingDirection))
            {
                controller.SetState(controller.WalkingState);
            }
            else
            {
                controller.SetState(controller.CollisionState);
            }
        }
    }
}
