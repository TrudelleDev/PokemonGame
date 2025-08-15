using System.Collections;
using PokemonGame.Transitions.Interfaces;
using UnityEngine;

namespace PokemonGame.Transitions.Extensions
{
    /// <summary>
    /// Provides coroutine-based extensions for <see cref="ITransition"/> to enable fade effects 
    /// that can be awaited in Unity's coroutine system.
    /// </summary>
    public static class TransitionCoroutineExtension
    {
        /// <summary>
        /// Runs a fade-in effect as a coroutine, waiting until the transition completes.
        /// </summary>
        /// <param name="transition">The transition to perform the fade-in.</param>
        /// <returns>An enumerator that completes when the fade-in finishes.</returns>
        public static IEnumerator FadeInCoroutine(this ITransition transition)
        {
            bool done = false;
            transition.FadeIn(() => done = true);
            yield return new WaitUntil(() => done);
        }

        /// <summary>
        /// Runs a fade-out effect as a coroutine, waiting until the transition completes.
        /// </summary>
        /// <param name="transition">The transition to perform the fade-out.</param>
        /// <returns>An enumerator that completes when the fade-out finishes.</returns>
        public static IEnumerator FadeOutCoroutine(this ITransition transition)
        {
            bool done = false;
            transition.FadeOut(() => done = true);
            yield return new WaitUntil(() => done);
        }
    }
}
