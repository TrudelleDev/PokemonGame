using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Monster;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the forced send-out sequence when a player must replace a fainted monster.
    /// </summary>
    internal sealed class PlayerForcedSendOutState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MonsterInstance selectedMonster;
        private BattleView Battle => machine.BattleView;

        internal PlayerForcedSendOutState(BattleStateMachine machine, MonsterInstance selectedMonster)
            => (this.machine, this.selectedMonster) = (machine, selectedMonster);

        public void Enter() => Battle.StartCoroutine(PlaySequence());
        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;

            // 1. Data Update
            Battle.SetNextPlayerMonster(selectedMonster);

            // 2. Dialogue & Visuals
            var monsterName = selectedMonster.Definition.DisplayName;
            var sendMessage = BattleMessages.PlayerSendMonster(monsterName);
            Battle.DialogueBox.ShowDialogue(sendMessage);

            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();

            yield return Battle.TurnPauseYield;

            // 3. Next State
            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}
