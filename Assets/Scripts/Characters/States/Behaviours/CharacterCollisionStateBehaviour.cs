using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Characters.States.Behaviours
{
    /// <summary>
    /// Animator behaviour that notifies the character controller
    /// when the collision animation finishes.
    /// </summary>
    public class CharacterCollisionStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {         
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
