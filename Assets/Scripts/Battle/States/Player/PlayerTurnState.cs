using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;
using MonsterTamer.Move;
using MonsterTamer.Move.Models;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Executes the player's move and determines whether to pass the turn to the opponent or end the round.
    /// </summary>
    internal sealed class PlayerTurnState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MoveInstance playerMove;
        private readonly MoveInstance opponentMove;
        private readonly bool isFirst;

        private BattleView Battle => machine.BattleView;

        internal PlayerTurnState(BattleStateMachine machine, MoveInstance playerMove, MoveInstance opponentMove, bool isFirst)
            => (this.machine, this.playerMove, this.opponentMove, this.isFirst)
               = (machine, playerMove, opponentMove, isFirst);

        public void Enter() => Battle.StartCoroutine(PlaySequence());

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.PlayerActiveMonster;
            var target = Battle.OpponentActiveMonster;
            var context = new MoveContext(Battle, user, target, playerMove);
            var useMoveMessage = BattleMessages.UseMove(user.Definition.DisplayName, playerMove.Definition.DisplayName);

            yield return Battle.DialogueBox.ShowDialogueAndWait(useMoveMessage);
            yield return playerMove.Definition.MoveEffect.PerformMoveSequence(context);
            yield return Battle.TurnPauseYield;

            // 2. Faint Checks
            if (target.IsFainted)
            {
                machine.SetState(new OpponentFaintedState(machine, target));
                yield break;
            }

            if (user.IsFainted)
            {
                machine.SetState(new PlayerFaintedState(machine, user));
                yield break;
            }

            // 3. Round Logic
            if (isFirst)
            {
                // Pass to opponent turn, providing the moves and setting isFirst to false
                machine.SetState(new OpponentTurnState(machine, opponentMove, playerMove, false));
            }
            else
            {
                machine.SetState(new PlayerActionMenuState(machine));
            }
        }
    }
}
