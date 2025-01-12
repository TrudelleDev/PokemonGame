using UnityEngine;

namespace PokemonGame
{
    public class AnimatorOverrider : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            animator.runtimeAnimatorController = overrideController;
        }
    }
}
