using System.Collections;
using UnityEngine;

namespace PokemonGame.Utilities
{
    public static class AnimationUtility
    {
        public static IEnumerator WaitForAnimation(Animator animator, int stateHash)
        {
            // Wait until animator enters the target state
            while (!animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
            {
                yield return null;
            }

            // Wait until the animation finishes
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            while (info.shortNameHash == stateHash && info.normalizedTime < 1f)
            {
                yield return null;
                info = animator.GetCurrentAnimatorStateInfo(0);
            }
        }
    }
}
