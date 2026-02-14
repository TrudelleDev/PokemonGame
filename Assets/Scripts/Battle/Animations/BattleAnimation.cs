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
        internal IEnumerator PlayOpponentHudEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.HudAnimator, BattleAnimationStates.OpponentHudEnter);
        internal IEnumerator PlayOpponentHudExit() => AnimatorHelper.PlayAndWait(opponentAnimations.HudAnimator, BattleAnimationStates.OpponentHudExit);
        internal IEnumerator PlayOpponentMonsterTakeDamage() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationStates.OpponentMonsterTakeDamage);
        internal IEnumerator PlayOpponentTrainerEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationStates.OpponentTrainerEnter);
        internal IEnumerator PlayOpponentTrainerExit() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationStates.OpponentTrainerExit);
        internal IEnumerator PlayOpponentMonsterEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationStates.OpponentMonsterEnter);
        internal IEnumerator PlayOpponentTrainerDefeatOutro() => AnimatorHelper.PlayAndWait(opponentAnimations.TrainerAnimator, BattleAnimationStates.OpponentTrainerDefeatOutro);
        internal void PlayOpponentMonsterDeath() => opponentAnimations.MonsterAnimator.Play(BattleAnimationStates.OpponentMonsterDeath);

        // Player Animations
        internal IEnumerator PlayPlayerHudEnter() => AnimatorHelper.PlayAndWait(playerAnimations.HudAnimator, BattleAnimationStates.PlayerHudEnter);
        internal IEnumerator PlayPlayerTrainerExit() => AnimatorHelper.PlayAndWait(playerAnimations.TrainerSpriteAnimator, BattleAnimationStates.PlayerTrainerExit);
        internal IEnumerator PlayPlayerMonsterEnter() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationStates.PlayerMonsterEnter);
        internal IEnumerator PlayPlayerMonsterExit() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationStates.PlayerMonsterExit);
        internal IEnumerator PlayPlayerMonsterTakeDamage() => AnimatorHelper.PlayAndWait(playerAnimations.MonsterAnimator, BattleAnimationStates.PlayerMonsterTakeDamage);
        internal void PlayPlayerHudExit() => playerAnimations.HudAnimator.Play(BattleAnimationStates.PlayerHudExit);
        internal void PlayPlayerTrainerEnter() => playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationStates.PlayerTrainerEnter);
        internal void PlayPlayerMonsterDeath() => playerAnimations.MonsterAnimator.Play(BattleAnimationStates.PlayerMonsterDeath);

        // Wild
        internal IEnumerator PlayWildMonsterEnter() => AnimatorHelper.PlayAndWait(opponentAnimations.MonsterAnimator, BattleAnimationStates.WildMonsterEnter);

        internal void ResetIntro()
        {
            AnimatorHelper.RebindAll(
                playerAnimations.HudAnimator, playerAnimations.MonsterAnimator, playerAnimations.TrainerSpriteAnimator,
                opponentAnimations.HudAnimator, opponentAnimations.MonsterAnimator, opponentAnimations.TrainerAnimator
            );
        }
    }
}