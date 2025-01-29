using UnityEngine;

namespace PokemonGame
{
    public static class Utility
    {
        public static float GetAnimationLength(Animator animator, string animationName)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == animationName)
                {
                    return clip.length;
                }
            }

            return 0;
        }
    }
}
