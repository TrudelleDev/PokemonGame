using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Centralized manager responsible for coordinating all visual battle animations
    /// for both player and opponent sides.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BattleAnimation : MonoBehaviour
    {
        [SerializeField, Tooltip("Data container holding all Animators for the player's battle elements.")]
        private PlayerAnimations playerAnimations;

        [SerializeField, Tooltip("Data container holding all Animators for the opponent's battle elements.")]
        private OpponentAnimations opponentAnimations;

        [SerializeField, Tooltip("Shader-based overlay animator used for status or screen effects.")]
        private CoverOffsetAnimator coverOffsetAnimator;

        /// <summary>
        /// Flag set by animation events to signal when the Pokéball should be thrown.
        /// </summary>
        public bool ThrowPokeball { get; set; }

        // ─────────────────────────────
        //    Opponent Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays a non-blocking visual status effect (e.g., burn, poison).
        /// </summary>
        public IEnumerator PlayStatusEffect()
        {
            yield return coverOffsetAnimator.AnimateOffsetCoroutine(Vector2.zero, new Vector2(0f, 1f), 1f);
        }

        public void PlayOpponentPokemonBarEnter() => opponentAnimations.PokemonBarAnimator.Play(BattleAnimationState.Enter);

        public IEnumerator PlayOpponentPlatformEnter() =>
            PlayAnimation(opponentAnimations.PlatformAnimator, BattleAnimationState.Enter);

        public IEnumerator PlayOpponentHudEnter() =>
            PlayAnimation(opponentAnimations.HudAnimator, BattleAnimationState.Enter);

        public IEnumerator PlayOpponentTakeDamage() =>
            PlayAnimation(opponentAnimations.PokemonAnimator, BattleAnimationState.TakeDamage);

        public void PlayOpponentDeath() =>
           opponentAnimations.PokemonAnimator.Play(BattleAnimationState.Death);

        public IEnumerator PlayOpponentHudExit() => PlayAnimation(opponentAnimations.HudAnimator, BattleAnimationState.Exit);

        public void PlayOpponentPokemonBarExit() => opponentAnimations.PokemonBarAnimator.Play(BattleAnimationState.Exit);

        public void PlayOpponentTrainerEnter() => opponentAnimations.TrainerAnimator.Play(BattleAnimationState.Enter);
        public IEnumerator PlayOpponentTrainerExit() => PlayAnimation(opponentAnimations.TrainerAnimator, BattleAnimationState.Exit);
        public IEnumerator PlayTrainerPokemonEnter() => PlayAnimation(opponentAnimations.PokemonAnimator, BattleAnimationState.TrainerPokemonEnter);

        // ─────────────────────────────
        //    Player Animations
        // ─────────────────────────────

        public void PlayPlayerTrainerExit() =>
            playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationState.Exit);

        public IEnumerator PlayPlayerHudEnter() =>
            PlayAnimation(playerAnimations.HudAnimator, BattleAnimationState.Enter);

        public IEnumerator PlayPlayerSendPokemonEnter() =>
            PlayAnimation(playerAnimations.PokemonAnimator, BattleAnimationState.Enter);

        public IEnumerator PlayPlayerTakeDamage() =>
            PlayAnimation(playerAnimations.PokemonAnimator, BattleAnimationState.TakeDamage);

        public void PlayPlayerDeath() =>
            playerAnimations.PokemonAnimator.Play(BattleAnimationState.Death);

        public void PlayPlayerPokemonIdle() =>
            playerAnimations.PokemonAnimator.Play(BattleAnimationState.Idle);

        public void PlayPlayerHUDIdle() =>
            playerAnimations.HudAnimator.Play(BattleAnimationState.Idle);

        public void PlayPlayerPokemonDefault() =>
            playerAnimations.PokemonAnimator.Play(BattleAnimationState.IdleStatic);

        public void PlayPlayerHUDDefault() =>
            playerAnimations.HudAnimator.Play(BattleAnimationState.IdleStatic);

        /// <summary>
        /// Plays the trainer's throw animation, waits for the Pokéball to be released,
        /// then plays the Pokéball animation and waits for it to complete.
        /// </summary>
        public IEnumerator PlayPlayerThrowBallSequence()
        {
            yield return new WaitUntil(() => ThrowPokeball);
            yield return PlayAnimation(playerAnimations.ThrowBallAnimator, BattleAnimationState.Throw);
            ThrowPokeball = false;
        }


        public IEnumerator PlayPokemonBars()
        {
            opponentAnimations.PokemonBarAnimator.Play(BattleAnimationState.Enter);
            yield return PlayAnimation(playerAnimations.PokemonBarAnimator, BattleAnimationState.Enter);
        }



        public IEnumerator PlayPlayerWithdrawPokemon() =>
            PlayAnimation(playerAnimations.PokemonAnimator, BattleAnimationState.Withdraw);

        public IEnumerator PlayPlayerBattleHudExit() =>
            PlayAnimation(playerAnimations.HudAnimator, BattleAnimationState.Exit);

        public void PlayPlayerPlatformEnter() => playerAnimations.PlatformAnimator.Play(BattleAnimationState.Enter);
        public void PlayPlayerTrainerSpriteEnter() => playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationState.Enter);
        public void PlayOpponentPlatformEnter2() => opponentAnimations.PlatformAnimator.Play(BattleAnimationState.Enter);
        public void PlayWildPokemonEnter() => opponentAnimations.PokemonAnimator.Play(BattleAnimationState.WildPokemonEnter); // changethis


        public IEnumerator PlayPlayerPokemonBarEnter() => PlayAnimation(playerAnimations.PokemonBarAnimator, BattleAnimationState.Enter);
        public void PlayPlayerPokemonBarExit() => playerAnimations.PokemonBarAnimator.Play(BattleAnimationState.Exit);

        // ─────────────────────────────
        //    Global Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the initial, non-blocking introduction animations (platforms and trainers/Pokémon).
        /// </summary>
        public void PlayIntro()
        {
            playerAnimations.PlatformAnimator.Play(BattleAnimationState.Enter);
            playerAnimations.TrainerSpriteAnimator.Play(BattleAnimationState.Enter);

            opponentAnimations.PlatformAnimator.Play(BattleAnimationState.Enter);
            //opponentAnimations.PokemonAnimator.Play(BattleAnimationState.Enter);
        }

        /// <summary>
        /// Resets all Animator components to their default bind pose and state.
        /// Essential cleanup before starting a new battle sequence.
        /// </summary>
        public void ResetIntro()
        {
            RebindAll(
                playerAnimations.HudAnimator,
                playerAnimations.PlatformAnimator,
                playerAnimations.PokemonAnimator,
                playerAnimations.TrainerSpriteAnimator,
                playerAnimations.ThrowBallAnimator,
                playerAnimations.PokemonBarAnimator,
                opponentAnimations.HudAnimator,
                opponentAnimations.PlatformAnimator,
                opponentAnimations.PokemonAnimator,
                opponentAnimations.PokemonBarAnimator

            );
        }

        // ─────────────────────────────
        //    Private Helpers
        // ─────────────────────────────

        /// <summary>
        /// Plays the specified animation state on the given Animator and waits until it finishes.
        /// </summary>
        private IEnumerator PlayAnimation(Animator animator, int state)
        {
            animator.Play(state, 0, 0f);

            // Ensure animator transitions before querying state info.
            yield return null;

            yield return AnimationUtility.WaitForAnimationSafe(animator, state);
        }

        /// <summary>
        /// Rebinds multiple animators in a single call.
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
