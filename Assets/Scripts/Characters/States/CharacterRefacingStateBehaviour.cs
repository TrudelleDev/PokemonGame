using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Animator behaviour that signals when the refacing animation ends.
    /// Notifies <see cref="CharacterRefacingState"/> to continue.
    /// </summary>
    public class CharacterRefacingStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponent<CharacterStateController>();

            // If still refacing, complete and return to idle
            if (controller.CurrentState is CharacterRefacingState refacingState)
            {
                refacingState.OnRefacingComplete();
            }
        }
    }
}
