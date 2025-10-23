using System;
using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Controls the full Pokémon-style battle intro sequence.
    /// Waits for each animation to finish before continuing to the next phase.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleAnimation : MonoBehaviour
    {
        private static readonly int EnterState = Animator.StringToHash("Enter");
        private static readonly int ExitState = Animator.StringToHash("Exit");
        private static readonly int ThrowState = Animator.StringToHash("Throw");

        [Title("Player Animators")]
        [SerializeField, Required] private Animator playerPlatformAnimator;
        [SerializeField, Required] private Animator playerSpriteAnimator;
        [SerializeField, Required] private Animator playerPokemonAnimator;
        [SerializeField, Required] private Animator playerHudAnimator;
        [SerializeField, Required] private Animator throwBallAnimator;

        [Title("Opponent Animators")]
        [SerializeField, Required] private Animator opponentPlatformAnimator;
        [SerializeField, Required] private Animator opponentSpriteAnimator;
        [SerializeField, Required] private Animator opponentHudAnimator;

        public event Action OnSequenceFinish;

        private bool canThrowPokeball = false;
        private bool canSendPokemon = false;

        private BattleView battleView;

        public void Initialize(BattleView battleView)
        {
            this.battleView = battleView;        
        }

        private void DialogueBox_OnDialogueFinished()
        {
            canSendPokemon = true;
        }

        public void PlayIntroSequence()
        {
            StartCoroutine(PlaySequenceCoroutine());
        }

        public void SetCanThrowPokeball()
        {
            canThrowPokeball = true;
        }

        private IEnumerator PlaySequenceCoroutine()
        {
            DialogueBoxView dialogueBox = ViewManager.Instance.Get<DialogueBoxView>();
            dialogueBox.OnLineFinished += DialogueBox_OnDialogueFinished;

            dialogueBox.ShowDialogue(new[] { 
                $"Wild {battleView.OpponentPokemon.Definition.DisplayName} just appeared!",
                $"Go {battleView.PlayerPokemon.Definition.DisplayName}!"
            });

            // --- Phase 1: Platforms + Sprites ---
            playerPlatformAnimator.Play(EnterState);
            playerSpriteAnimator.Play(EnterState);
            opponentPlatformAnimator.Play(EnterState);
            opponentSpriteAnimator.Play(EnterState);

            yield return WaitForAnimation(opponentPlatformAnimator, EnterState);

            // --- Phase 2: Opponent HUD appears ---
            opponentHudAnimator.Play(EnterState);
            yield return WaitForAnimation(opponentHudAnimator, EnterState);

            // --- Phase 3: Wait for player input (press any key to continue) ---
            yield return new WaitUntil(() => canSendPokemon);

            // --- Phase 4: Player throws Pokéball ---
            playerSpriteAnimator.Play(ExitState);

            yield return new WaitUntil(() => canThrowPokeball);

            throwBallAnimator.Play(ThrowState);
            yield return WaitForAnimation(throwBallAnimator, ThrowState);

            // --- Phase 5: Pokémon appears + HUD ---
            playerPokemonAnimator.Play(EnterState);
            yield return WaitForAnimation(playerPokemonAnimator, EnterState);

            playerHudAnimator.Play(EnterState);
            yield return WaitForAnimation(playerHudAnimator, EnterState);

            OnSequenceFinish?.Invoke();
        }

        /// <summary>
        /// Waits until the given animator finishes playing the specified state.
        /// </summary>
        private static IEnumerator WaitForAnimation(Animator animator, int stateHash)
        {
            // Wait until the animator transitions into the target state
            while (!animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
            {
                yield return null;
            }

            // Wait until animation reaches the end (normalizedTime >= 1)
            var info = animator.GetCurrentAnimatorStateInfo(0);
            while (info.shortNameHash == stateHash && info.normalizedTime < 1f)
            {
                yield return null;
                info = animator.GetCurrentAnimatorStateInfo(0);
            }
        }

        public void PlayPlayerHudIdle()
        {
            playerHudAnimator.Play("Idle");
        }
    }
}
