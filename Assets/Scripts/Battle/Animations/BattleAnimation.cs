using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Utilities;
using UnityEngine;

namespace MonsterTamer.Battle.Animations
{
    /// <summary>
    /// Orchestrates all battle-related animations for players and opponents.
    /// Acts as the high-level API for the battle state machine to trigger visual transitions.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleAnimation : MonoBehaviour
    {
        [SerializeField, Tooltip("Animators for player HUD, Monster, and trainer.")]
        private PlayerAnimations playerAnimations;

        [SerializeField, Tooltip("Animators for opponent HUD, Monster, and trainer.")]
        private OpponentAnimations opponentAnimations;

        // Opponent Animations
        internal IEnumerator PlayOpponentHudEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.HudAnimator, BattleAnimationState.OpponentHudEnter);
        internal IEnumerator PlayOpponentHudExit() => AnimatorHelper.PlayAndWait(opponentAnimations.HudAnimator, BattleAnimationState.OpponentHudExit);
        internal IEnumerator PlayOpponentMonsterTakeDamage() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationState.OpponentMonsterTakeDamage);
        internal IEnumerator PlayOpponentTrainerEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerEnter);
        internal IEnumerator PlayOpponentTrainerExit() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerExit);
        internal IEnumerator PlayOpponentMonsterEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationState.OpponentMonsterEnter);
        internal IEnumerator PlayOpponentTrainerDefeatOutro() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerDefeatOutro);
        internal void PlayOpponentMonsterDeath() => opponentAnimations.MonsterAnimator.Play(BattleAnimationState.OpponentMonsterDeath);

        // Player Animations
        internal IEnumerator PlayPlayerHudEnter() => AnimatorHelper.PlayAndWait(playerAnimations.HudAnimator, BattleAnimationState.PlayerHudEnter);
        internal IEnumerator PlayPlayerTrainerExit() => AnimatorHelper.PlayAndWait(playerAnimations.TrainerSpriteAnimator, BattleAnimationState.PlayerTrainerExit);
        internal IEnumerator PlayPlayerMonsterEnter() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterEnter);
        internal IEnumerator PlayPlayerMonsterExit() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterExit);
        internal IEnumerator PlayPlayerMonsterTakeDamage() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterTakeDamage);
        internal void PlayPlayerHudExit() => playerAnimations.HudAnimator.Play(BattleAnimationState.PlayerHudExit);
        internal void PlayPlayerTrainerEnter() => playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationState.PlayerTrainerEnter);
        internal void PlayPlayerMonsterDeath() => playerAnimations.MonsterAnimator.Play(BattleAnimationState.PlayerMonsterDeath);

        // Wild
        internal IEnumerator PlayWildMonsterEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationState.WildMonsterEnter);

        internal void ResetIntro()
        {
            AnimatorHelper.RebindAll(
                playerAnimations.HudAnimator, playerAnimations.MonsterAnimator, playerAnimations.TrainerSpriteAnimator,
                opponentAnimations.HudAnimator, opponentAnimations.MonsterAnimator, opponentAnimations.TrainerAnimator
            );
        }
    }
}