using MonsterTamer.Utilities;
using UnityEngine;

namespace MonsterTamer.Characters.States.Behaviours
{
    /// <summary>
    /// Animator behaviour that notifies the character controller
    /// when the collision animation finishes.
    /// </summary>
    public sealed class CharacterCollisionStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Only notify if the animator has a CharacterStateController
            if (!animator.TryGetComponent<CharacterStateController>(out var controller))
            {
                Log.Warning(nameof(CharacterCollisionStateBehaviour), "No CharacterStateController found on animator.");
                return;
            }

            // Forward completion to the collision state
            if (controller.CurrentState is CharacterCollisionState collisionState)
            {
                collisionState.OnCollisionComplete();
            }
        }
    }
}
