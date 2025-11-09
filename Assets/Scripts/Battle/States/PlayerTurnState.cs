using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Moves;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the player's attack turn.
    /// Executes the move, waits for animations,
    /// and transitions to the next state.
    /// </summary>
    public class PlayerTurnState : IBattleState
    {
        private const float TurnDelay = 1f;

        private readonly BattleStateMachine machine;
        private readonly Move move;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTurnState"/> class.
        /// </summary>
        /// <param name="machine">The active <see cref="BattleStateMachine"/> managing the battle flow.</param>
        /// <param name="move">The move selected by the player to execute.</param>
        public PlayerTurnState(BattleStateMachine machine, Move move)
        {
            this.machine = machine;
            this.move = move;
        }

        /// <summary>
        /// Called when the state is entered.
        /// Begins executing the player's attack turn.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(ExecuteTurn());
        }

        private IEnumerator ExecuteTurn()
        {
            Pokemon player = Battle.PlayerPokemon;
            Pokemon opponent = Battle.OpponentPokemon;

            Battle.DialogueBox.ShowDialogue($"{player.Definition.DisplayName} used {move.Definition.DisplayName}!");

            yield return WaitForLineTypingComplete();

            Battle.BattleAudio.PlayDoDamageNomral();
            yield return Battle.BattleAnimation.PlayOpponentTakeDamage();

            int damage = player.Attack(move, opponent);
            opponent.TakeDamage(damage);

            
            yield return WaitForHealthAnimationComplete();

            // Check if opponent fainted
            if (opponent.HealthRemaining <= 0)
            {
                yield return Battle.BattleAnimation.PlayOpponentDeath();
                Battle.DialogueBox.ShowDialogue($"Wild {opponent.Definition.DisplayName} fainted!", manualArrowControl: true);
                yield return WaitDialogueComplete();
               
                machine.SetState(new VictoryState(machine));
                yield break;
            }

            // Delay before next turn
            yield return new WaitForSecondsRealtime(TurnDelay);
            machine.SetState(new OpponentTurnState(machine));
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

        private IEnumerator WaitDialogueComplete()
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

        private IEnumerator WaitForHealthAnimationComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.OpponentBattleHud.HealthBar.OnHealthAnimationFinished -= OnComplete;
            }

            Battle.OpponentBattleHud.HealthBar.OnHealthAnimationFinished += OnComplete;
            yield return new WaitUntil(() => done);
        }

        

        public void Update() { }

        public void Exit() { }
    }
}
