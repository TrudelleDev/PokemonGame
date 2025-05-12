namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State representing a character's collision when trying to move into a blocked tile.
    /// Plays a collision animation and determines the next state based on input and tile conditions.
    /// </summary>
    public class CharacterCollisionState : ICharacterState
    {
        private readonly CharacterStateController controller;

        /// <summary>
        /// Initializes the state with the given controller.
        /// </summary>
        /// <param name="controller">The controller managing character states and transitions.</param>
        public CharacterCollisionState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Triggers the collision animation when entering the state.
        /// </summary>
        public void Enter()
        {
            controller.AnimatorController.PlayCollisionStep();
        }

        /// <summary>
        /// No frame-specific logic in this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No specific cleanup when exiting the state.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Determines the next state based on the collision result.
        /// </summary>
        public void OnCollisionComplete()
        {
            Direction direction = controller.Input.CurrentDirection;

            if (direction == Direction.None)
            {
                controller.SetState(controller.IdleState); // Transition to idle if no direction.
            }
            else if (controller.TileMover.CanMoveInDirection(direction))
            {
                controller.SetState(controller.WalkingState); // Transition to walking if tile is passable.
            }
            else
            {
                controller.SetState(controller.CollisionState); // Remain in collision if still blocked.
            }
        }
    }
}
