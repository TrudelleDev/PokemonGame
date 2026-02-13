using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;
using MonsterTamer.Monster;
using MonsterTamer.Move;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the withdrawal of the current monster and the entry of a new one,
    /// then passes the turn to the opponent.
    /// </summary>
    internal sealed class PlayerSwapMonsterState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MonsterInstance newMonster;
        private readonly MoveInstance opponentMove;

        private BattleView Battle => machine.BattleView;

        internal PlayerSwapMonsterState(BattleStateMachine machine, MonsterInstance newMonster, MoveInstance opponentMove)
            => (this.machine, this.newMonster, this.opponentMove) = (machine, newMonster, opponentMove);

        public void Enter() => Battle.StartCoroutine(PlaySequence());

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            // 1. Withdraw Current Monster
            string currentName = Battle.PlayerActiveMonster.Definition.DisplayName;
            yield return dialogue.ShowDialogueAndWait(BattleMessages.MonsterReturnParty(currentName));

            animation.PlayPlayerHudExit();
            yield return animation.PlayPlayerMonsterExit();
            yield return Battle.TurnPauseYield;

            // 2. Data Swap
            Battle.SetNextPlayerMonster(newMonster);

            // 3. Send Out New Monster
            string newName = newMonster.Definition.DisplayName;
            dialogue.ShowDialogue(BattleMessages.PlayerSendMonster(newName));

            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();
            yield return Battle.TurnPauseYield;

            // 4. Opponent's Free Hit
            // isActingFirst is false because the player already used their turn to swap
            machine.SetState(new OpponentTurnState(machine, opponentMove, null, isActingFirst: false));
        }
    }
}
