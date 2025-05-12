using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Animator behavior that triggers logic when the refacing animation ends.
    /// Notifies the <see cref="CharacterRefacingState"/> to proceed to the next state.
    /// </summary>
    public class CharacterRefacingStateBehaviour : StateMachineBehaviour
    {
        /// <summary>
        /// Called when exiting the refacing animation state. Notifies the active <see cref="CharacterRefacingState"/>.
        /// </summary>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponent<CharacterStateController>();

            if (controller.CurrentState is CharacterRefacingState refacingState)
            {
                refacingState.OnRefacingComplete();
            }
        }
    }
}
