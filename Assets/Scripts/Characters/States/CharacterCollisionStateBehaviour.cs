using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Animator behaviour that signals when the collision animation ends.
    /// Notifies <see cref="CharacterCollisionState"/> to transition.
    /// </summary>
    public class CharacterCollisionStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponent<CharacterStateController>();

            // If still in collision state let it decide what comes next
            if (controller.CurrentState is CharacterCollisionState collisionState)
            {
                collisionState.OnCollisionComplete();
            }
        }
    }
}
