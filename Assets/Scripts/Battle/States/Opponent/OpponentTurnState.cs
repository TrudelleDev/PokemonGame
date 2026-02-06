using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.States.Player;
using PokemonGame.Move.Models;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States.Opponent
{
    /// <summary>
    /// Handles the AI logic for the opponent's turn.
    /// Selects a move, executes the move sequence, and manages state transitions
    /// depending on fainted Pokémon or the end of the turn.
    /// </summary>
    internal sealed class OpponentTurnState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new state for the opponent's turn.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling the battle flow.
        /// </param>
        internal OpponentTurnState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and starts the opponent move sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic is required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Performs the opponent move sequence:
        /// 1. Waits for UI transitions to finish.
        /// 2. Selects a move (currently placeholder logic).
        /// 3. Displays the move announcement.
        /// 4. Executes the move animation, damage, and effects.
        /// 5. Checks for fainted Pokémon and transitions to the appropriate state.
        /// 6. Ends the turn by returning control to the player.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            // 1. Wait for UI transitions
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.OpponentActiveMonster;
            var target = Battle.PlayerActiveMonster;

            // 2. Placeholder AI: always uses first move
            var move = user.Moves.Moves[0];
            var context = new MoveContext(Battle, user, target, move);

            // 3. Announce move
            string message = string.Format(BattleMessages.UseMove, user.Definition.DisplayName, move.Definition.DisplayName);
            yield return Battle.DialogueBox.ShowDialogueAndWait(message);

            // 4. Execute move effect (animations, damage, status)
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            yield return Battle.TurnPauseYield;

            // 5. Post-move checks
            if (target.Health.CurrentHealth <= 0)
            {
                machine.SetState(new PlayerFaintedState(machine, target));
                yield break;
            }

            if (user.Health.CurrentHealth <= 0)
            {
                machine.SetState(new OpponentFaintedState(machine, user));
                yield break;
            }

            // 6. Return control to the player
            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}
