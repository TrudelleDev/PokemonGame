using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Battle;
using PokemonGame.Move.Models;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    /// <summary>
    /// Handles the complete sequence for a damaging move, including animations,
    /// applying damage, waiting for health bar updates, and showing effectiveness dialogue.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Move/Effects/Damage Effect")]
    public class DamageEffect : MoveEffect
    {
        [SerializeField, Tooltip("Optional delay before the hit animation starts.")]
        private float preHitDelay = 0.5f;

        /// <summary>
        /// Returns true if the move's target is the player's Pokémon.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        private bool IsTargetUser(MoveContext context)
        {
            return context.Target == context.Battle.PlayerPokemon;
        }

        /// <summary>
        /// Calculates and applies damage to the target Pokémon.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override void ApplyEffect(MoveContext context)
        {
            int damage = DamageCalculator.CalculateDamage(context.User, context.Target, context.Move);
            context.Target.Health.TakeDamage(damage);
        }

        /// <summary>
        /// Waits until the target's health bar animation finishes updating.
        /// Ensures the sequence doesn't continue until UI is done animating.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override IEnumerator WaitForHealthAnimation(MoveContext context)
        {
            var healthBar = IsTargetUser(context)
                ? context.Battle.BattleHUDs.Player.HealthBar
                : context.Battle.BattleHUDs.Opponent.HealthBar;

            yield return healthBar.WaitForHealthAnimationComplete();
        }

        /// <summary>
        /// Plays the appropriate damage animation for the attacker or defender.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        protected override IEnumerator PlayEffectAnimation(MoveContext context)
        {
            var animationCoroutine = IsTargetUser(context)
                ? context.Battle.Components.Animation.PlayPlayerTakeDamage()
                : context.Battle.Components.Animation.PlayOpponentTakeDamage();

            yield return animationCoroutine;
        }

        /// <summary>
        /// Performs the full damage move sequence including delay, animations,
        /// damage calculation, health animation wait, and effectiveness text.
        /// </summary>
        /// <param name="context">Current move execution context.</param>
        public override IEnumerator PerformMoveSequence(MoveContext context)
        {
            if (preHitDelay > 0)
            {
                yield return new WaitForSecondsRealtime(preHitDelay);
            }

            TypeDefinition moveType = context.Move.Definition.Classification.TypeDefinition;
            TypeDefinition targetType = context.Target.Definition.Types.FirstType;
            TypeEffectiveness moveEffectiveness = moveType.EffectivenessGroups.GetEffectiveness(targetType);

            context.Battle.Components.Audio.PlayEffectivenessSound(moveEffectiveness);

            yield return PlayEffectAnimation(context);
            ApplyEffect(context);
            yield return WaitForHealthAnimation(context);
            yield return context.Battle.DialogueBox.ShowDialogueAndWait(moveEffectiveness.ToText());
        }
    }
}
