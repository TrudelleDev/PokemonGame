using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.States.Opponent;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Executes the player's selected move, handles animations, damage calculation,
    /// and transitions to the next appropriate battle state.
    /// </summary>
    internal sealed class PlayerTurnState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MoveInstance move;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player turn state with the chosen move.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling the battle flow.
        /// </param>
        /// <param name="move">
        /// The move selected by the player for this turn.
        /// </param>
        internal PlayerTurnState(BattleStateMachine machine, MoveInstance move)
        {
            this.machine = machine;
            this.move = move;
        }

        /// <summary>
        /// Enters the state and begins executing the move sequence.
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
        /// Performs the move sequence, including:
        /// 1. Waiting for transitions to finish.
        /// 2. Displaying the move announcement.
        /// 3. Playing animations and effects.
        /// 4. Checking for fainted status and handling state transitions.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            // 1. Wait for any UI transitions to finish
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.PlayerActiveMonster;
            var target = Battle.OpponentActiveMonster;
            var context = new MoveContext(Battle, user, target, move);

            // 2. Announce the move
            string message = string.Format(BattleMessages.UseMove, user.Definition.DisplayName, move.Definition.DisplayName);
            yield return Battle.DialogueBox.ShowDialogueAndWait(message);

            // 3. Execute move effect (animations, damage, status)
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            yield return Battle.TurnPauseYield;

            // 4. Post-move checks: Did the opponent faint?
            if (target.Health.CurrentHealth <= 0)
            {
                machine.SetState(new OpponentFaintedState(machine, target));
                yield break;
            }

            // 5. Post-move checks: Did the user faint (e.g., recoil damage)?
            if (user.Health.CurrentHealth <= 0)
            {
                machine.SetState(new PlayerFaintedState(machine, user));
                yield break;
            }

            // 6. Proceed to the opponent's turn
            machine.SetState(new OpponentTurnState(machine));
        }
    }
}
