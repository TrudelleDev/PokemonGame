namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State for turning the character to a new facing without moving.
    /// Plays a refacing animation, then returns to idle.
    /// </summary>
    public class CharacterRefacingState : ICharacterState
    {
        private readonly CharacterStateController controller;

        public CharacterRefacingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            // Play turn animation in current facing direction
            controller.AnimatorController.PlayRefacing(controller.FacingDirection);
        }

        public void Update() { }

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
