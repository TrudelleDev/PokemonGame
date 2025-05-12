using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Animator state machine behavior that triggers logic when the collision animation ends.
    /// Notifies the <see cref="CharacterCollisionState"/> to transition to the next state.
    /// </summary>
    public class CharacterCollisionStateBehaviour : StateMachineBehaviour
    {
        /// <summary>
        /// Called when exiting the collision animation state. Notifies <see cref="CharacterCollisionState"/> to handle state transition.
        /// </summary>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponent<CharacterStateController>();

            if (controller.CurrentState is CharacterCollisionState collisionState)
            {
                collisionState.OnCollisionComplete();
            }
        }
    }
}
