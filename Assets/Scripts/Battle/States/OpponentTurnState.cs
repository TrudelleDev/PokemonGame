using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Moves;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the opponent's turn during battle.
    /// Executes the selected move, waits for animations,
    /// and transitions back to the player's action state or ends the battle.
    /// </summary>
    public class OpponentTurnState : IBattleState
    {
        private const float TurnPause = 1f;

        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpponentTurnState"/> class.
        /// </summary>
        /// <param name="machine">The active <see cref="BattleStateMachine"/> controlling the battle flow.</param>
        public OpponentTurnState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when the state is entered.
        /// Begins executing the opponent's turn sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(ExecuteTurn());
        }

        private IEnumerator ExecuteTurn()
        {
            Pokemon opponent = Battle.OpponentPokemon;
            Pokemon player = Battle.PlayerPokemon;

            // TODO: replace with AI move selection later
            Move move = opponent.Moves[0];

            Battle.DialogueBox.ShowDialogue($"{opponent.Definition.DisplayName} used {move.Definition.DisplayName}!");

            yield return WaitForLineTypingComplete();
            yield return Battle.BattleAnimation.PlayPlayerTakeDamage();

            int damage = opponent.Attack(move, player);
            player.TakeDamage(damage);

            yield return WaitForHealthAnimationComplete();

            // Check if the player's Pokémon fainted
            if (player.HealthRemaining <= 0)
            {
                yield return new WaitForSecondsRealtime(TurnPause);
                Battle.DialogueBox.ShowDialogue($"{player.Definition.DisplayName} fainted!");

                yield return WaitForLineTypingComplete();
                yield return Battle.BattleAnimation.PlayPlayerDeath();

                Battle.DialogueBox.OnDialogueFinished += HandlePlayerFaint;
                yield break;
            }

            yield return new WaitForSecondsRealtime(TurnPause);
            machine.SetState(new PlayerActionState(machine));
        }

        private void HandlePlayerFaint()
        {
            Battle.DialogueBox.OnDialogueFinished -= HandlePlayerFaint;
           // ViewManager.Instance.CloseTopView(); // Return to overworld
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

        private IEnumerator WaitForHealthAnimationComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.PlayerBattleHud.HealthBar.OnHealthAnimationFinished -= OnComplete;
            }

            Battle.PlayerBattleHud.HealthBar.OnHealthAnimationFinished += OnComplete;
            yield return new WaitUntil(() => done);
        }

        public void Update() { }

        public void Exit() { }
    }
}
