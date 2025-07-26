using System;
using System.Collections;
using UnityEngine;

namespace PokemonGame.Transitions
{
    /// <summary>
    /// Base class for screen or UI transitions that use fade-in and fade-out effects.
    /// Implement this in concrete transition controllers such as alpha or masked transitions.
    /// </summary>
    public abstract class Transition : MonoBehaviour
    {
        /// <summary>
        /// Starts a fade-in effect (typically from black to transparent).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-in completes.</param>
        public abstract void FadeIn(Action onComplete = null);

        /// <summary>
        /// Starts a fade-out effect (typically from transparent to black).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-out completes.</param>
        public abstract void FadeOut(Action onComplete = null);
    }
}
