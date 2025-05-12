namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State representing a character turning to face a new direction without moving.
    /// Plays a refacing animation, then returns to idle.
    /// </summary>
    public class CharacterRefacingState : ICharacterState
    {
        private readonly CharacterStateController controller;

        /// <summary>
        /// Initializes the refacing state with the specified controller.
        /// </summary>
        /// <param name="controller">Manages state transitions and character context.</param>
        public CharacterRefacingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Updates the facing direction and plays the refacing animation.
        /// </summary>
        public void Enter()
        {
            controller.FacingDirection = controller.Input.CurrentDirection;
            controller.AnimatorController.PlayRefacing();
        }

        /// <summary>
        /// No logic needed during update in this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup needed when exiting the state.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Called when the refacing animation completes; transitions to idle.
        /// </summary>
        public void OnRefacingComplete()
        {
            controller.SetState(controller.IdleState);
        }
    }
}
