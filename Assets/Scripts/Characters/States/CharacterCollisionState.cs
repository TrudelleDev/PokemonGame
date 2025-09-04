using PokemonGame.Audio;
using PokemonGame.Characters.Direction;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State entered when the character attempts to move into a blocked tile.
    /// Plays a collision animation, triggers optional SFX, and decides the next state.
    /// </summary>
    public class CharacterCollisionState : ICharacterState
    {
        private readonly CharacterStateController controller;

        public CharacterCollisionState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            controller.AnimatorController.PlayCollisionStep();

            // Play collision sound if defined
            if (controller.CollisionAudioClip != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(controller.CollisionAudioClip);
            }
        }

        /// <summary>
        /// No update logic required for collision state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required for collision state.
        /// </summary>
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
