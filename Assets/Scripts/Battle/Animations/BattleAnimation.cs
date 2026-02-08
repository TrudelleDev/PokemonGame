using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Utilities;
using UnityEngine;

namespace MonsterTamer.Battle.Animations
{
    /// <summary>
    /// Centralized controller responsible for coordinating all battle-related animations
    /// for both player and opponent sides.
    /// Acts as the single animation entry point for the battle state machine.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleAnimation : MonoBehaviour
    {
        [SerializeField, Tooltip("Animators for the player's battle elements (HUD, Monster, trainer).")]
        private PlayerAnimations playerAnimations;

        [SerializeField, Tooltip("Animators for the opponent's battle elements (HUD, Monster, trainer).")]
        private OpponentAnimations opponentAnimations;

        // ─────────────────────────────
        //    Opponent Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the opponent HUD enter animation.
        /// </summary>
        internal IEnumerator PlayOpponentHudEnter()
        {
            yield return PlayAnimation(opponentAnimations.HudAnimator, BattleAnimationState.OpponentHudEnter);
        }

        /// <summary>
        /// Plays the opponent HUD exit animation.
        /// </summary>
        internal IEnumerator PlayOpponentHudExit()
        {
            yield return PlayAnimation(opponentAnimations.HudAnimator, BattleAnimationState.OpponentHudExit);
        }

        /// <summary>
        /// Plays the opponent Monster damage reaction animation.
        /// </summary>
        internal IEnumerator PlayOpponentMonsterTakeDamage()
        {
            yield return PlayAnimation(opponentAnimations.MonsterAnimator, BattleAnimationState.OpponentMonsterTakeDamage);
        }

        /// <summary>
        /// Plays the opponent trainer sprite entrance animation.
        /// </summary>
        internal IEnumerator PlayOpponentTrainerEnter()
        {
            yield return PlayAnimation(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerEnter);
        }

        /// <summary>
        /// Plays the opponent trainer sprite exit animation.
        /// </summary>
        internal IEnumerator PlayOpponentTrainerExit()
        {
            yield return PlayAnimation(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerExit);
        }

        /// <summary>
        /// Plays the opponent Monster sprite enter animation (trainer battle).
        /// </summary>
        internal IEnumerator PlayOpponentMonsterEnter()
        {
            yield return PlayAnimation(opponentAnimations.MonsterAnimator, BattleAnimationState.OpponentMonsterEnter);
        }

        /// <summary>
        /// Plays the opponent trainer end-of-battle animation.
        /// </summary>
        internal IEnumerator PlayOpponentTrainerDefeatOutro()
        {
            yield return PlayAnimation(opponentAnimations.TrainerAnimator, BattleAnimationState.OpponentTrainerDefeatOutro);
        }

        /// <summary>
        /// Instantly triggers the opponent Monster faint animation.
        /// </summary>
        internal void PlayOpponentMonsterDeath()
        {
            opponentAnimations.MonsterAnimator.Play(BattleAnimationState.OpponentMonsterDeath);
        }

        // ─────────────────────────────
        //    Player Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the player HUD enter animation.
        /// </summary>
        internal IEnumerator PlayPlayerHudEnter()
        {
            yield return PlayAnimation(playerAnimations.HudAnimator, BattleAnimationState.PlayerHudEnter);
        }

        /// <summary>
        /// Instantly plays the player HUD exit animation.
        /// </summary>
        internal void PlayPlayerHudExit()
        {
            playerAnimations.HudAnimator.Play(BattleAnimationState.PlayerHudExit);
        }

        /// <summary>
        /// Instantly plays the player trainer sprite enter animation.
        /// </summary>
        internal void PlayPlayerTrainerEnter()
        {
            playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationState.PlayerTrainerEnter);
        }

        /// <summary>
        /// Plays the player trainer sprite exit animation.
        /// </summary>
        internal IEnumerator PlayPlayerTrainerExit()
        {
            yield return PlayAnimation(playerAnimations.TrainerSpriteAnimator, BattleAnimationState.PlayerTrainerExit);
        }

        /// <summary>
        /// Plays the player Monster sprite enter animation.
        /// </summary>
        internal IEnumerator PlayPlayerMonsterEnter()
        {
            yield return PlayAnimation(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterEnter);
        }

        /// <summary>
        /// Plays the player Monster sprite withdraw animation.
        /// </summary>
        internal IEnumerator PlayPlayerMonsterExit()
        {
            yield return PlayAnimation(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterExit);
        }

        /// <summary>
        /// Plays the player Monster damage reaction animation.
        /// </summary>
        internal IEnumerator PlayPlayerMonsterTakeDamage()
        {
            yield return PlayAnimation(playerAnimations.MonsterAnimator, BattleAnimationState.PlayerMonsterTakeDamage);
        }

        /// <summary>
        /// Instantly triggers the player Monster sprite faint animation.
        /// </summary>
        internal void PlayPlayerMonsterDeath()
        {
            playerAnimations.MonsterAnimator.Play(BattleAnimationState.PlayerMonsterDeath);
        }


        // ─────────────────────────────
        //    Wild Monster Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the wild Monster entrance animation.
        /// </summary>
        internal IEnumerator PlayWildMonsterEnter()
        {
            yield return PlayAnimation(opponentAnimations.MonsterAnimator, BattleAnimationState.WildMonsterEnter);
        }

        // ─────────────────────────────
        //    Global Animations
        // ─────────────────────────────

        /// <summary>
        /// Resets all battle-related animators to their default bind pose.
        /// Called before starting any battle intro sequence.
        /// </summary>
        internal void ResetIntro()
        {
            RebindAll(
                playerAnimations.HudAnimator,
                playerAnimations.MonsterAnimator,
                playerAnimations.TrainerSpriteAnimator,
                opponentAnimations.HudAnimator,
                opponentAnimations.MonsterAnimator
            );
        }

        // ─────────────────────────────
        //    Private Helpers
        // ─────────────────────────────

        /// <summary>
        /// Plays a specific animation state and waits until it completes.
        /// </summary>
        private IEnumerator PlayAnimation(Animator animator, int state)
        {
            animator.Play(state, 0, 0f);

            // Allow one frame for the animator to transition
            yield return null;

            yield return AnimationUtility.WaitForAnimationSafe(animator, state);
        }

        /// <summary>
        /// Rebinds all provided animators in a single operation.
        /// </summary>
        private static void RebindAll(params Animator[] animators)
        {
            foreach (Animator animator in animators)
            {
                animator.Rebind();
            }
        }
    }
}
