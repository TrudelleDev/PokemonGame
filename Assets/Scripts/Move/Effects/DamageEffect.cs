using System.Collections;
using PokemonGame.Move.Models;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    [CreateAssetMenu(menuName = "PokemonGame/Move/Effects/Damage Effect")]
    public class DamageEffect : MoveEffect
    {
        [SerializeField]
        private float PreHitDelay = 0.5f;

        private bool IsTargetPlayer(MoveContext context)
        {
            return context.Target == context.Battle.PlayerPokemon;
        }

        protected override void ApplyDamage(MoveContext context)
        {
            int damage = context.User.Stats.CalculateDamage(context.User, context.Target, context.Move);
            context.Target.Health.TakeDamage(damage);
        }

        protected override IEnumerator WaitForHealthAnimation(MoveContext context)
        {
            var healthBar = IsTargetPlayer(context)
                ? context.Battle.PlayerBattleHud.HealthBar
                : context.Battle.OpponentBattleHud.HealthBar;

            yield return healthBar.WaitForHealthAnimationComplete();
        }

        protected override IEnumerator PlayHitAnimation(MoveContext context)
        {
            // Select the appropriate damage animation method
            var animationCoroutine = IsTargetPlayer(context)
                ? context.Battle.BattleAnimation.PlayPlayerTakeDamage()
                : context.Battle.BattleAnimation.PlayOpponentTakeDamage();

            // Wait for the animation to complete
            yield return animationCoroutine;
        }

        public override IEnumerator PerformMoveSequence(MoveContext context)
        {
            if (PreHitDelay > 0)
            {
                yield return new WaitForSecondsRealtime(PreHitDelay);
            }

            context.Battle.BattleAudio.PlayDoDamageNomral();
            yield return PlayHitAnimation(context);
            ApplyDamage(context);
            yield return WaitForHealthAnimation(context);
        }
    }
}