using System.Collections;
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

            Battle.BattleAnimation.ResetIntro();
            Battle.BattleAnimation.PlayIntro();

            yield return Battle.BattleAnimation.PlayOpponentPlatformEnter();
            yield return Battle.BattleAnimation.PlayOpponentHudEnter();
            Battle.BattleAudio.PlayPokemonCry(Battle.OpponentPokemon);

            Battle.DialogueBox.ShowDialogue($"Wild {opponentName} appeared!", manualArrowControl: true);

            yield return WaitForDialogueComplete();

            Battle.DialogueBox.ShowDialogue($"Go {playerName}!");

            Battle.BattleAnimation.PlayPlayerExit();

            yield return Battle.BattleAnimation.PlayPlayerThrowBall();

            Battle.BattleAudio.PlayOpenPokeballSfx();
            yield return new WaitForSecondsRealtime(0.25f);
            Battle.BattleAudio.PlayPokemonCry(Battle.PlayerPokemon);

            yield return Battle.BattleAnimation.PlayPlayerSendPokemonEnter();
            yield return Battle.BattleAnimation.PlayPlayerHudEnter();

            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator WaitForDialogueComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.DialogueBox.OnDialogueFinished -= OnComplete;
            }

            Battle.DialogueBox.OnDialogueFinished += OnComplete;

            yield return new WaitUntil(() => done);
        }

        public void Update() { }

        public void Exit() { }
    }
}
