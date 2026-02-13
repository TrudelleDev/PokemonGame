using System.Collections;
using UnityEngine;

namespace MonsterTamer.Utilities
{
    /// <summary>
    /// Provides low-level helper methods for managing Unity Animator states.
    /// </summary>
    internal static class AnimatorHelper
    {
        /// <summary>
        /// Plays a state and waits for completion.
        /// </summary>
        internal static IEnumerator PlayAndWait(Animator animator, int state)
        {
            if (animator == null) yield break;

            animator.Play(state, 0, 0f);
            yield return null; // Buffer frame to allow state transition
            yield return AnimationUtility.WaitForAnimationSafe(animator, state);
        }

        /// <summary>
        /// Resets multiple animators to their bind pose in a single call.
        /// </summary>
        internal static void RebindAll(params Animator[] animators)
        {
            foreach (var animator in animators)
            {
                animator?.Rebind();
            }
        }
    }
}