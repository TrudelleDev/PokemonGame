using PokemonGame.Characters.Interfaces;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State for turning the character to a new facing without moving.
    /// Plays a refacing animation, then returns to idle.
    /// </summary>
    internal sealed class CharacterRefacingState : ICharacterState
    {
        private readonly CharacterStateController controller;

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
