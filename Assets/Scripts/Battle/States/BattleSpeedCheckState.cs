using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;
using MonsterTamer.Battle.States.Player;
using MonsterTamer.Move;
using UnityEngine;

namespace MonsterTamer.Battle.States
{
    /// <summary>
    /// Compares Speed stats to determine which turn state goes first.
    /// </summary>
    internal sealed class BattleSpeedCheckState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MoveInstance playerMove;
        private BattleView Battle => machine.BattleView;

        internal BattleSpeedCheckState(BattleStateMachine machine, MoveInstance playerMove)
            => (this.machine, this.playerMove) = (machine, playerMove);

        public void Enter()
        {
            var playerMonster = Battle.PlayerActiveMonster;
            var opponentMonster = Battle.OpponentActiveMonster;
            var opponentMove = opponentMonster.GetRandomMove();

            // 2. Speed Comparison
            bool playerGoesFirst = playerMonster.Stats.Core.Speed >= opponentMonster.Stats.Core.Speed;

            // 3. Hand off to the faster monster's turn
            if (playerGoesFirst)
            {
                machine.SetState(new PlayerTurnState(machine, playerMove, opponentMove, isFirst: true));
            }
            else
            {
                machine.SetState(new OpponentTurnState(machine, opponentMove, playerMove, isActingFirst: true));
            }           
        }

        public void Update() { }
        public void Exit() { }
    }
}
