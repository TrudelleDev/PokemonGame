using System.Collections;
using PokemonGame.Transitions.Interfaces;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Transitions.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ITransition"/> to run view-to-view transitions.
    /// </summary>
    public static class TransitionViewExtensions
    {
        /// <summary>
        /// Executes a transition from one view to another, including optional fades.
        /// </summary>
        /// <param name="transition">The transition effect to use. If null, views are switched instantly.</param>
        /// <param name="fromView">The view to hide.</param>
        /// <param name="toView">The view to show.</param>
        /// <param name="fadeDelay">Optional delay between fade-in and fade-out, in seconds.</param>
        public static IEnumerator RunViewTransition(this ITransition transition, View fromView, View toView, float fadeDelay = 1f)
        {
            if (toView == null)
            {
                fromView.Hide();
                yield break;
            }

            if (transition == null)
            {
                fromView.Hide();
                toView.Show();
                yield break;
            }

            yield return transition.FadeInCoroutine();

            if (fadeDelay > 0f)
            {
                yield return new WaitForSecondsRealtime(fadeDelay);
            }
              
            fromView.Hide();
            toView.Show();
            yield return transition.FadeOutCoroutine();
        }
    }
}
