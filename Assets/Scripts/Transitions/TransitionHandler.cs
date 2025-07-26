using System.Collections;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Transitions
{
    /// <summary>
    /// Handles transitions between views, including fade in/out and an optional delay.
    /// </summary>
    public class TransitionHandler : MonoBehaviour
    {
        [Tooltip("Transition effect to use when switching between views.")]
        [SerializeField, Required]
        private Transition transition;

        /// <summary>
        /// Time to wait between fade in and fade out transitions.
        /// </summary>
        private const float Delay = 1f;

        /// <summary>
        /// Plays a transition between two views, optionally using fade-in, delay, and fade-out.
        /// </summary>
        /// <param name="currentView">The view to fade out and hide.</param>
        /// <param name="nextView">The view to show after the transition. Can be null.</param>
        public IEnumerator PlayTransition(View currentView, View nextView)
        {
            if (transition == null)
            {
                Log.Warning(this, " is missing a Transition. Skipping visual transition.");
                currentView.Hide();
                nextView?.Show();
                yield break;
            }

            yield return FadeIn();
            yield return new WaitForSecondsRealtime(Delay);
            currentView.Hide();
            nextView?.Show();
            yield return FadeOut();
        }

        /// <summary>
        /// Plays the fade-in transition and waits until it completes.
        /// </summary>
        private IEnumerator FadeIn()
        {
            bool done = false;
            transition.FadeIn(() => done = true);
            yield return new WaitUntil(() => done);
        }

        /// <summary>
        /// Plays the fade-out transition and waits until it completes.
        /// </summary>
        private IEnumerator FadeOut()
        {
            bool done = false;
            transition.FadeOut(() => done = true);
            yield return new WaitUntil(() => done);
        }
    }
}
