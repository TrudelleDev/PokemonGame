using PokemonGame.Characters.Directions;
using PokemonGame.Characters.Interfaces;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Idle state: the character stands still.
    /// Evaluates input each frame and transitions to refacing, walking, or collision.
    /// </summary>
    internal sealed class CharacterIdleState : ICharacterState
    {
        private readonly CharacterStateController controller;

        public CharacterIdleState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Enters the idle state and plays the idle animation for the current facing direction.
        /// </summary>
        public void Enter()
        {
            controller.AnimatorController.PlayIdle(controller.FacingDirection);
        }

        /// <summary>
        /// Called every frame while idle.
        /// Evaluates input to decide whether to reface, walk, collide, or stay idle.
        /// </summary>
        public void Update()
        {
            InputDirection input = controller.Input.CurrentDirection;

            if (input == InputDirection.None)
            {
                return;
            }

            FacingDirection facing = input.ToFacingDirection();

            // Trigger interactions before moving
            if (controller.TriggerHandler != null && controller.TriggerHandler.TryTrigger(input.ToVector2Int()))
            {
                return;
            }

            // Reface if needed
            if (controller.FacingDirection != facing)
            {
                controller.FacingDirection = facing;
                controller.SetState(controller.RefacingState);
                return;
            }

            // Walk or collide depending on tile availability
            if (controller.TileMover.CanMoveInDirection(facing))
            {
                controller.SetState(controller.WalkingState);
            }
            else
            {
                controller.SetState(controller.CollisionState);
            }
        }

        /// <summary>
        /// No cleanup required for idle state.
        /// </summary>
        public void Exit() { }
    }
}
