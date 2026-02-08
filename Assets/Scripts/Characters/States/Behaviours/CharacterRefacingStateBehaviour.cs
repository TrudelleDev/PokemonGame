using MonsterTamer.Utilities;
using UnityEngine;

namespace MonsterTamer.Characters.States.Behaviours
{
    /// <summary>
    /// Animator behaviour that notifies the character controller
    /// when the refacing animation finishes.
    /// </summary>
    public sealed class CharacterRefacingStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Only notify if the animator has a CharacterStateController
            if (!animator.TryGetComponent<CharacterStateController>(out var controller))
            {
                Log.Warning(nameof(CharacterRefacingStateBehaviour), "No CharacterStateController found on animator.");
                return;
            }

            // Forward completion to the refacing state
            if (controller.CurrentState is CharacterRefacingState refacingState)
            {
                refacingState.OnRefacingComplete();
            }
        }
    }
}
