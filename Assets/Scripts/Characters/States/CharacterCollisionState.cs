using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Inputs.Enums;
using PokemonGame.Characters.Inputs.Extensions;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Handles collision when trying to move into a blocked tile.
    /// Plays a collision animation, then decides the next state.
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
        }

        public void Update() { }

        public void Exit() { }

        public void OnCollisionComplete()
        {
            InputDirection inputDir = controller.Input.InputDirection;

            // No input, return idle
            if (inputDir == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
                return;
            }

            // Update facing to match attempted direction
            FacingDirection desiredFacing = inputDir.ToFacingDirection();
            controller.FacingDirection = desiredFacing;

            // Retry movement if possible, otherwise stay in collision
            if (controller.TileMover.CanMoveInDirection(desiredFacing))
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
