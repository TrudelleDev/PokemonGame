using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Move.Enums;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon.Components;
using PokemonGame.Pokemon.Enums;
using UnityEngine;

namespace PokemonGame.Move.Effects
{
    [CreateAssetMenu(menuName = "PokemonGame/Move/Effects/Stat Change Effect")]
    public class StatChangeEffect : MoveEffect
    {
        [SerializeField, Tooltip("Which stat to modify")]
        private PokemonStat targetStat;

        [SerializeField, Range(StatStageComponent.MinStage, StatStageComponent.MaxStage)]
        [Tooltip("Positive = boost, Negative = drop")]
        private int stages;

        [SerializeField, Tooltip("Whether the stat change affects the user or the target")]
        private StatChangeTarget affected;

        protected override void ApplyDamage(MoveContext context)
        {
            var pokemon = affected == StatChangeTarget.User ? context.User : context.Target;
            pokemon.Stats.StatStage.ModifyStat(targetStat, stages);
        }

        protected override string GetEffectText(MoveContext context)
        {
            var pokemon = affected == StatChangeTarget.User ? context.User : context.Target;
            string action = stages > 0 ? "rose" : "fell";
            return $"{pokemon.Definition.DisplayName}'s {targetStat} {action}!";
        }

        protected override IEnumerator PlayHitAnimation(MoveContext context)
        {
            yield return context.Battle.BattleAnimation.PlayStatusEffect();
        }

        public override IEnumerator PerformMoveSequence(MoveContext context)
        {
            AudioManager.Instance.PlaySFX(effectSound);
            yield return PlayHitAnimation(context);
            
            ApplyDamage(context);
            yield return WaitForHealthAnimation(context);

            string battleText = GetEffectText(context);
            yield return context.Battle.DialogueBox.ShowDialogueAndWait(battleText);
        }
    }
}
