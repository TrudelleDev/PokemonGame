using System.Collections;
using PokemonGame.Dialogue;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the full introduction sequence of a Pokémon-style battle.
    /// Transitions to <see cref="PlayerActionState"/> once complete.
    /// </summary>
    public class BattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleIntroState"/> class.
        /// </summary>
        /// <param name="machine">The active <see cref="BattleStateMachine"/> controlling the battle flow.</param>
        public BattleIntroState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when the state is entered.
        /// Starts the full intro coroutine, including dialogue and animations.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlayIntroSequence());
        }

        private IEnumerator PlayIntroSequence()
        {
            string opponentName = Battle.OpponentPokemon.Definition.DisplayName;
            string playerName = Battle.PlayerPokemon.Definition.DisplayName;

            Battle.DialogueBox.ShowDialogue(new[]
            {
                $"Wild {opponentName} appeared!",
                $"Go {playerName}!"
            });

            Battle.BattleAnimation.ResetIntro();
            Battle.BattleAnimation.PlayIntro();

            yield return Battle.BattleAnimation.PlayOpponentPlatformEnter();
            yield return Battle.BattleAnimation.PlayOpponentHudEnter();
            yield return WaitForLineTypingComplete();

            Battle.BattleAnimation.PlayPlayerExit();

            yield return Battle.BattleAnimation.PlayPlayerThrowBall();
            yield return Battle.BattleAnimation.PlayPlayerSendPokemonEnter();
            yield return Battle.BattleAnimation.PlayPlayerHudEnter();

            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator WaitForLineTypingComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.DialogueBox.OnLineTypingComplete -= OnComplete;
            }

           Battle.DialogueBox.OnLineTypingComplete += OnComplete;

            yield return new WaitUntil(() => done);
        }

        public void Update() { }

        public void Exit() { }
    }
}
