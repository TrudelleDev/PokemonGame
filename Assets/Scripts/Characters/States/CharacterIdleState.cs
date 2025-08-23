namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Represents the idle state where the character is not moving.
    /// Transitions to walking, refacing, or collision states based on input and tile conditions.
    /// </summary>
    public class CharacterIdleState : ICharacterState
    {
        private readonly CharacterStateController controller;

        /// <summary>
        /// Creates a new idle state with the specified controller.
        /// </summary>
        /// <param name="controller">Controls state transitions for the character.</param>
        public CharacterIdleState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Checks input each frame to determine if a state transition is needed.
        /// </summary>
        public void Update()
        {
            Direction direction = controller.Input.CurrentDirection;

            if (direction == Direction.None) return;

            if (controller.FacingDirection != direction)
            {
                controller.SetState(controller.RefacingState); // Transition to refacing if the facing direction is not the same as the current direction.
            }
            else if (controller.TileMover.CanMoveInDirection(direction))
            {
                controller.SetState(controller.WalkingState); // Transition to walking if tile is passable.
            }
            else
            {
                controller.SetState(controller.CollisionState);
            }
        }

        /// <summary>
        /// Called when entering the idle state. No setup required.
        /// </summary>
        public void Enter() { }

        /// <summary>
        /// Called when exiting the idle state. No cleanup required.
        /// </summary>
        public void Exit() { }
    }
}
