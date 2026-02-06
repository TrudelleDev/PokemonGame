using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Move.Enums;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon.Components;
using PokemonGame.Pokemon.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    /// <summary>
    /// Applies a stat stage increase or decrease to either the user or the target,
    /// and handles playing the associated animation, sound, and dialogue.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Move/Effects/Stat Change Effect")]
    public class StatChangeEffect : MoveEffect
    {
        [SerializeField, Required, Tooltip("Sound to play when the stat change occurs.")]
        protected AudioClip effectSound;

        [SerializeField, Tooltip("Which stat to modify.")]
        private PokemonStat targetStat;

        [SerializeField, Tooltip("Whether the stat change affects the user or the target.")]
        private StatChangeTarget affected;

        [SerializeField, Range(StatStageComponent.MinStage, StatStageComponent.MaxStage)]
        [Tooltip("Positive = boost, Negative = drop.")]
        private int stages;

        /// <summary>
        /// Applies the actual stat stage change to the correct Pokémon.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override void ApplyEffect(MoveContext context)
        {
            var pokemon = affected == StatChangeTarget.User ? context.User : context.Target;
            pokemon.Stats.StatStage.ModifyStat(targetStat, stages);
        }

        /// <summary>
        /// Returns the text describing the stat change for the battle dialogue.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override string GetEffectText(MoveContext context)
        {
            var pokemon = affected == StatChangeTarget.User ? context.User : context.Target;
            string action = stages > 0 ? "rose" : "fell";
            return $"{pokemon.Definition.DisplayName}'s {targetStat} {action}!";
        }

        /// <summary>
        /// Plays the status-effect animation used for stat changes.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override IEnumerator PlayEffectAnimation(MoveContext context)
        {
            yield return null;
         //   yield return context.Battle.Components.Animation.PlayStatusEffect();
        }

        /// <summary>
        /// Executes the full sequence for a stat change move, including sound,
        /// animation, applying the effect, waiting for required animations,
        /// and showing the resulting battle dialogue.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        public override IEnumerator PerformMoveSequence(MoveContext context)
        {
            AudioManager.Instance.PlaySFX(effectSound);

            yield return PlayEffectAnimation(context);
            ApplyEffect(context);

            yield return WaitForHealthAnimation(context);

            string battleText = GetEffectText(context);
            yield return context.Battle.DialogueBox.ShowDialogueAndWait(battleText);
        }
    }
}
