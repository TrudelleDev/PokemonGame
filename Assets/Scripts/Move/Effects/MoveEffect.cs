using System.Collections;
using PokemonGame.Move.Models;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    /// <summary>
    /// Base class for all move effects (damage, status, stat changes, etc.).
    /// </summary>
    internal abstract class MoveEffect : ScriptableObject
    {
        /// <summary>
        /// Waits for the target Monster's health animation to complete.
        /// Override if the effect doesn't need to wait for health updates.
        /// </summary>
        /// <param name="context">The context containing user, target, and move info.</param>
        protected virtual IEnumerator WaitForHealthAnimation(MoveContext context)
        {
            yield break;
        }

        /// <summary>
        /// Applies the main effect (damage, status, or stat changes) to the target.
        /// </summary>
        /// <param name="context">The context containing user, target, and move info.</param>
        protected abstract void ApplyEffect(MoveContext context);

        /// <summary>
        /// Plays visual and audio feedback for the move.
        /// </summary>
        /// <param name="context">The context containing user, target, and move info.</param>
        protected abstract IEnumerator PlayEffectAnimation(MoveContext context);

        /// <summary>
        /// Plays the move's sound effect. Override for custom audio.
        /// </summary>
        /// <param name="context">The context containing user, target, and move info.</param>
        protected virtual void PlayEffectSound(MoveContext context)
        {
            context.Battle.Components.Audio.PlayMoveSound(context.Move.Definition);
        }

        /// <summary>
        /// Runs the full move sequence, including animations, effects, health updates, and dialogue.
        /// </summary>
        /// <param name="context">The context containing user, target, and move info.</param>
        internal abstract IEnumerator PerformMoveSequence(MoveContext context);
    }
}
