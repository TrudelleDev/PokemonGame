using System.Collections;
using PokemonGame.Move.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    /// <summary>
    /// Base abstract class for all move effects (damage, status, stat changes, etc.).
    /// Inherits from ScriptableObject to allow effects to be created as reusable assets.
    /// </summary>
    public abstract class MoveEffect : ScriptableObject
    {
        /// <summary>
        /// Waits for the target's health bar animation to complete before proceeding.
        /// Overridable to allow specific effects to skip the wait.
        /// </summary>
        /// <param name="context">The context containing the user, target, and move data.</param>
        protected virtual IEnumerator WaitForHealthAnimation(MoveContext context)
        {
            yield break; // Default implementation immediately yields, doing nothing.
        }

        /// <summary>
        /// Provides the text to be displayed in the battle log for this effect.
        /// </summary>
        /// <param name="context">The context containing the user, target, and move data.</param>
        protected virtual string GetEffectText(MoveContext context) => string.Empty;

        /// <summary>
        /// Defines the logic for applying damage, stat changes, or other primary effects.
        /// </summary>
        /// <param name="context">The context containing the user, target, and move data.</param>
        protected abstract void ApplyEffect(MoveContext context);

        /// <summary>
        /// Plays the visual and audio feedback when the move connects.
        /// </summary>
        /// <param name="context">The context containing the user, target, and move data.</param>
        protected abstract IEnumerator PlayEffectAnimation(MoveContext context);

        /// <summary>
        /// The main coroutine that orchestrates the entire sequence of the move's effect.
        /// </summary>
        /// <param name="context">The context containing the user, target, and move data.</param>
        public abstract IEnumerator PerformMoveSequence(MoveContext context);
    }
}