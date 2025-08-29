using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Inputs.Enums;
using PokemonGame.Characters.Inputs.Extensions;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Idle state: the character is standing still.
    /// Evaluates input each frame and transitions to refacing, walking, or collision.
    /// </summary>
    public class CharacterIdleState : ICharacterState
    {
        private readonly CharacterStateController controller;
        private readonly InteractionHandler interactionHandler;

        public CharacterIdleState(CharacterStateController controller)
        {
            this.controller = controller;
            interactionHandler = controller.GetComponent<InteractionHandler>();
        }

        public void Enter()
        {
            controller.AnimatorController.PlayIdle(controller.FacingDirection);
        }

        public void Update()
        {
            InputDirection input = controller.Input.InputDirection;

            if (input == InputDirection.None)
                return;

            FacingDirection facing = input.ToFacingDirection();

            // Trigger interactions before moving
            interactionHandler?.CheckForTriggers(input.ToVector2Int());

            // Reface if needed
            if (controller.FacingDirection != facing)
            {
                controller.FacingDirection = facing;
                controller.SetState(controller.RefacingState);
                return;
            }

            // Walk or collide
            controller.SetState(controller.TileMover.CanMoveInDirection(facing)
                ? controller.WalkingState
                : controller.CollisionState);
        }

        public void Exit() { }
    }
}
