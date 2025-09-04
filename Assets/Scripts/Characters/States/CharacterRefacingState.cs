namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State for turning the character to a new facing without moving.
    /// Plays a refacing animation, then returns to idle.
    /// </summary>
    public class CharacterRefacingState : ICharacterState
    {
        private readonly CharacterStateController controller;

        /// <summary>
        /// Creates a new refacing state for the given controller.
        /// </summary>
        /// <param name="controller">The character controller that owns this state.</param>
        public CharacterRefacingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Enters the refacing state and plays the refacing animation.
        /// If no animator is available, immediately transitions back to idle.
        /// </summary>
        public void Enter()
        {
            if (controller.AnimatorController != null)
            {
                controller.AnimatorController.PlayRefacing(controller.FacingDirection);
            }
            else
            {
                controller.SetState(controller.IdleState);
            }
        }

        /// <summary>
        /// No update logic required for refacing state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required for idle state.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Called when the refacing animation completes.
        /// Transitions the character back to idle.
        /// </summary>
        public void OnRefacingComplete()
        {
            controller.SetState(controller.IdleState);
        }
    }
}
