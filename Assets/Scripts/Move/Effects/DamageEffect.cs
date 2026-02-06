using System.Collections;
using PokemonGame.Battle;
using PokemonGame.Move.Models;
using PokemonGame.Type;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    /// <summary>
    /// Executes a standard damaging move sequence.
    /// Handles damage calculation, hit animations, HP bar updates,
    /// sound playback, and effectiveness dialogue for Monsters.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Move/Effects/Damage Effect")]
    internal sealed class DamageEffect : MoveEffect
    {
        /// <summary>
        /// Returns true if the target Monster belongs to the player.
        /// Used to select the correct HUD and animation side.
        /// </summary>
        private static bool IsTargetPlayer(MoveContext context)
        {
            return context.Target == context.Battle.PlayerActiveMonster;
        }

        /// <summary>
        /// Calculates and applies damage to the target Monster.
        /// </summary>
        protected override void ApplyEffect(MoveContext context)
        {
            int damage = DamageCalculator.CalculateDamage(
                context.User,
                context.Target,
                context.Move);

            context.Target.Health.TakeDamage(damage);
        }

        /// <summary>
        /// Waits for the target Monster's HP bar animation to finish.
        /// Ensures the move sequence does not continue while the HUD is updating.
        /// </summary>
        protected override IEnumerator WaitForHealthAnimation(MoveContext context)
        {
            var healthBar = IsTargetPlayer(context)
                ? context.Battle.BattleHUDs.PlayerBattleHud.HealthBar
                : context.Battle.BattleHUDs.OpponentBattleHud.HealthBar;

            yield return healthBar.WaitForHealthAnimationComplete();
        }

        /// <summary>
        /// Plays the hit reaction animation on the target Monster.
        /// </summary>
        protected override IEnumerator PlayEffectAnimation(MoveContext context)
        {
            yield return IsTargetPlayer(context)
                ? context.Battle.Components.Animation.PlayPlayerMonsterTakeDamage()
                : context.Battle.Components.Animation.PlayOpponentMonsterTakeDamage();
        }

        /// <summary>
        /// Performs the full damaging move sequence:
        /// animation, sound, damage application, HP update,
        /// and effectiveness dialogue for the target Monster.
        /// </summary>
        internal override IEnumerator PerformMoveSequence(MoveContext context)
        {
            var moveType = context.Move.Definition.Classification.TypeDefinition;
            var targetType = context.Target.Definition.Types.FirstType;
            var effectiveness = moveType.EffectivenessGroups.GetEffectiveness(targetType);

            PlayEffectSound(context);
            yield return PlayEffectAnimation(context);

            ApplyEffect(context);
            yield return WaitForHealthAnimation(context);

            yield return context.Battle.DialogueBox.ShowDialogueAndWait(effectiveness.ToText());
        }
    }
}
