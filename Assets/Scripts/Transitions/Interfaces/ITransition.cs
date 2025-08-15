using System;

namespace PokemonGame.Transitions.Interfaces
{
    /// <summary>
    /// Defines a contract for screen or UI transitions that can fade in or fade out.
    /// Implementations may perform alpha fades, masked fades, or other visual effects.
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// Begins a fade-in effect, optionally invoking a callback when the fade completes.
        /// </summary>
        /// <param name="onComplete">Callback invoked after the fade finishes.</param>
        void FadeIn(Action onComplete = null);

        /// <summary>
        /// Begins a fade-out effect, optionally invoking a callback when the fade completes.
        /// </summary>
        /// <param name="onComplete">Callback invoked after the fade finishes.</param>
        void FadeOut(Action onComplete = null);
    }
}
