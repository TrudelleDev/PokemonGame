using System.Collections;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Handles all visual battle animations for both player and opponent sides.
    /// Controls platform, HUD, Pokémon, and Pokéball animations using Animator components.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleAnimation : MonoBehaviour
    {
        private static readonly int EnterState = Animator.StringToHash("Enter");
        private static readonly int ExitState = Animator.StringToHash("Exit");
        private static readonly int ThrowState = Animator.StringToHash("Throw");
        private static readonly int TakeDamageState = Animator.StringToHash("TakeDamage");
        private static readonly int DeathState = Animator.StringToHash("Death");

        [Title("Player Animators")]
        [SerializeField, Required, Tooltip("Animator controlling the player's battle platform.")]
        private Animator playerPlatformAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's trainer sprite.")]
        private Animator playerSpriteAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's active Pokémon sprite.")]
        private Animator playerPokemonAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the player's HUD display.")]
        private Animator playerHudAnimator;

        [SerializeField, Required, Tooltip("Animator used for the Pokéball throw animation.")]
        private Animator throwBallAnimator;

        [Title("Opponent Animators")]
        [SerializeField, Required, Tooltip("Animator controlling the opponent's battle platform.")]
        private Animator opponentPlatformAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's Pokémon sprite.")]
        private Animator opponentSpriteAnimator;

        [SerializeField, Required, Tooltip("Animator controlling the opponent's HUD display.")]
        private Animator opponentHudAnimator;

    //    [SerializeField, Required]
        public CoverOffsetAnimator coverOffsetAnimator;

        /// <summary>
        /// Flag used by the Pokéball throw sequence to signal when to start the animation.
        /// Set externally via <see cref="AnimationEventRelay"/>.
        /// </summary>
        public bool ThrowPokeball { get; set; }

        // ─────────────────────────────
        //   Opponent Animations
        // ─────────────────────────────

        public IEnumerator PlayStatusEffect()
        {
            yield return coverOffsetAnimator.AnimateOffsetCoroutine(new Vector2(0f, 0f), new Vector2(0f, 01f), 1f);
        }

        /// <summary>
        /// Plays the opponent's platform enter animation.
        /// </summary>
        public IEnumerator PlayOpponentPlatformEnter() => PlayAnimation(opponentPlatformAnimator, EnterState);

        /// <summary>
        /// Plays the opponent's HUD enter animation.
        /// </summary>
        public IEnumerator PlayOpponentHudEnter() => PlayAnimation(opponentHudAnimator, EnterState);

        /// <summary>
        /// Plays the opponent's damage animation.
        /// </summary>
        public IEnumerator PlayOpponentTakeDamage() => PlayAnimation(opponentSpriteAnimator, TakeDamageState);

        /// <summary>
        /// Plays the opponent's faint (death) animation.
        /// </summary>
        public IEnumerator PlayOpponentDeath() => PlayAnimation(opponentSpriteAnimator, DeathState);

        // ─────────────────────────────
        //   Player Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the player's trainer exit animation.
        /// </summary>
        public void PlayPlayerExit() => playerSpriteAnimator.Play(ExitState);

        /// <summary>
        /// Plays the player's HUD enter animation.
        /// </summary>
        public IEnumerator PlayPlayerHudEnter() => PlayAnimation(playerHudAnimator, EnterState);

        /// <summary>
        /// Plays the player's Pokémon send-out animation.
        /// </summary>
        public IEnumerator PlayPlayerSendPokemonEnter() => PlayAnimation(playerPokemonAnimator, EnterState);

        /// <summary>
        /// Plays the player's Pokémon damage animation.
        /// </summary>
        public IEnumerator PlayPlayerTakeDamage() => PlayAnimation(playerPokemonAnimator, TakeDamageState);

        /// <summary>
        /// Plays the player's Pokémon faint (death) animation.
        /// </summary>
        public IEnumerator PlayPlayerDeath() => PlayAnimation(playerPokemonAnimator, DeathState);

        /// <summary>
        /// Waits for a flag trigger before playing the Pokéball throw animation.
        /// </summary>
        public IEnumerator PlayPlayerThrowBall()
        {
            yield return new WaitUntil(() => ThrowPokeball);
            throwBallAnimator.Play(ThrowState);
            yield return AnimationUtility.WaitForAnimationSafe(throwBallAnimator, ThrowState);
            ThrowPokeball = false;
        }

        // ─────────────────────────────
        //   Global Animations
        // ─────────────────────────────

        /// <summary>
        /// Plays the intro animations for both player and opponent.
        /// </summary>
        public void PlayIntro()
        {
            playerPlatformAnimator.Play(EnterState);
            playerSpriteAnimator.Play(EnterState);
            opponentPlatformAnimator.Play(EnterState);
            opponentSpriteAnimator.Play(EnterState);
        }

        /// <summary>
        /// Resets all Animator components to their default bind pose.
        /// Useful before replaying the intro sequence.
        /// </summary>
        public void ResetIntro()
        {
            playerHudAnimator.Rebind();
            playerPlatformAnimator.Rebind();
            playerPokemonAnimator.Rebind();
            playerSpriteAnimator.Rebind();
            throwBallAnimator.Rebind();

            opponentHudAnimator.Rebind();
            opponentPlatformAnimator.Rebind();
            opponentSpriteAnimator.Rebind();
        }

        /// <summary>
        /// Plays the specified animation state on the given Animator and waits until it finishes.
        /// </summary>
        private IEnumerator PlayAnimation(Animator animator, int state)
        {
            animator.Play(state, 0, 0f);
            yield return null; // WAIT 1 FRAME or Animator won't update state
            yield return AnimationUtility.WaitForAnimationSafe(animator, state);
        }
    }
}
