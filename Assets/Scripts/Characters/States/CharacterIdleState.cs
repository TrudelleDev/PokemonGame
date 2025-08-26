using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Inputs.Enums;
using PokemonGame.Characters.Inputs.Extensions;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Represents the idle state where the character is not moving.
    /// Transitions to walking, refacing, or collision states based on input and tile conditions.
    /// </summary>
    public class CharacterIdleState : ICharacterState
    {
        private readonly CharacterStateController controller;

        public CharacterIdleState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        public void Update()
        {
            InputDirection inputDirection = controller.Input.InputDirection;

<<<<<<< HEAD
            if (inputDirection == InputDirection.None)
                return;
=======
            if (direction == Direction.None) return;
>>>>>>> origin/main

            FacingDirection nextFacingDirection = inputDirection.ToFacingDirection();

            // Turn first if facing a different way
            if (controller.FacingDirection != nextFacingDirection)
            {
                controller.FacingDirection = nextFacingDirection;
                controller.SetState(controller.RefacingState);
                return;
            }

            // Walk if movement is possible
            if (controller.TileMover.CanMoveInDirection(nextFacingDirection))
            {
<<<<<<< HEAD
                controller.SetState(controller.WalkingState);
                return;
=======
                controller.SetState(controller.WalkingState); // Transition to walking if tile is passable.
            }
            else
            {
                controller.SetState(controller.CollisionState);
>>>>>>> origin/main
            }

            // Otherwise, collision feedback
            controller.SetState(controller.CollisionState);
        }

        public void Enter()
        {
            controller.AnimatorController.PlayIdle(controller.FacingDirection);
        }

        public void Exit() { }
    }
}
