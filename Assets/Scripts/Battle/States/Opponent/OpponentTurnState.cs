using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;
using MonsterTamer.Move;
using MonsterTamer.Move.Models;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Battle.States.Opponent
{
    /// <summary>
    /// Executes the opponent's move and determines whether to pass the turn back to the player or end the round.
    /// </summary>
    internal sealed class OpponentTurnState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MoveInstance opponentMove;
        private readonly MoveInstance playerMove;
        private readonly bool isActingFirst;

        private BattleView Battle => machine.BattleView;

        internal OpponentTurnState(BattleStateMachine machine, MoveInstance opponentMove, MoveInstance playerMove, bool isActingFirst)
            => (this.machine, this.opponentMove, this.playerMove, this.isActingFirst) 
               = (machine, opponentMove, playerMove, isActingFirst);

        public void Enter() => Battle.StartCoroutine(PlaySequence());
        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            // Wait for UI transitions
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.OpponentActiveMonster;
            var target = Battle.PlayerActiveMonster;
            var context = new MoveContext(Battle, user, target, opponentMove);

            // 1. Announce & Execute
            var useMoveMessage = BattleMessages.UseMove(user.Definition.DisplayName, opponentMove.Definition.DisplayName);
            yield return Battle.DialogueBox.ShowDialogueAndWait(useMoveMessage);

            yield return opponentMove.Definition.MoveEffect.PerformMoveSequence(context);
            yield return Battle.TurnPauseYield;

            // 2. Post-move checks: Target (Player) faints
            if (target.IsFainted)
            {
                machine.SetState(new PlayerFaintedState(machine, target));
                yield break;
            }

            // 3. Post-move checks: User (Opponent) faints (e.g. recoil)
            if (user.IsFainted)
            {
                machine.SetState(new OpponentFaintedState(machine, user));
                yield break;
            }

            // 4. Round Logic
            if (isActingFirst)
            {
                // Opponent was first, now player gets their turn
                machine.SetState(new PlayerTurnState(machine, playerMove, opponentMove, false));
            }
            else
            {
                // Opponent was second, round is over
                machine.SetState(new PlayerActionMenuState(machine));
            }
        }
    }
}
