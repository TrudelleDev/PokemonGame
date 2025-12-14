using System.Collections;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the opponent's turn during battle.
    /// Executes the opponent's chosen move, checks for fainting, and transitions back to the player.
    /// </summary>
    public sealed class OpponentTurnState : IBattleState
    {
        private const float TurnPause = 0.5f;

        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpponentTurnState"/> class.
        /// </summary>
        /// <param name="machine">The battle state machine context.</param>
        public OpponentTurnState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when entering the state. Resets HUD states and starts the move execution coroutine.
        /// </summary>
        public void Enter()
        {
            // Reset HUD and Pokemon visuals to default turn state
            Battle.Components.Animation.PlayPlayerHUDDefault();
            Battle.Components.Animation.PlayPlayerPokemonDefault();

            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            // Ensure any previous screen transitions are complete before starting the turn
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.OpponentPokemon;
            var target = Battle.PlayerPokemon;

            // NOTE: Opponent AI/Logic should determine the move here, 
            // but for simplicity, we assume the first move.
            var move = user.Moves.Moves[0];

            var context = new MoveContext(Battle, user, target, move);

            // 1. Announce the move
            yield return Battle.DialogueBox
                .ShowDialogueAndWait($"{user.Definition.DisplayName} used {move.Definition.DisplayName}!");

            // 2. Execute the move sequence (damage calculation, animation, effects)
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            // 3. Check for Faint
            if (target.Health.CurrentHealth <= 0)
            {
                yield return PlayFaintSequence(target);
                yield break;
            }

            // 4. End Turn and Transition
            yield return new WaitForSecondsRealtime(TurnPause);
            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator PlayFaintSequence(PokemonInstance player)
        {
            yield return Battle.DialogueBox
                .ShowDialogueAndWait(
                    $"{player.Definition.DisplayName}\nfainted!",
                    manualArrowControl: true
                );

            yield return Battle.Components.Animation.PlayPlayerDeath();
        }
    }
}