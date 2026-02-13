using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;
using MonsterTamer.Monster;

namespace MonsterTamer.Battle.States.Opponent
{
    /// <summary>
    /// Handles the visual sequence of an opponent fainting and hands off to the experience state.
    /// </summary>
    internal sealed class OpponentFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MonsterInstance monster;
        private BattleView Battle => machine.BattleView;

        internal OpponentFaintedState(BattleStateMachine machine, MonsterInstance monster)
            => (this.machine, this.monster) = (machine, monster);

        public void Enter() => Battle.StartCoroutine(PlaySequence());

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var faintMessage = BattleMessages.FaintedMessage(monster.Definition.DisplayName);

            // 1. Visual Faint
            animation.PlayOpponentMonsterDeath();
            yield return animation.PlayOpponentHudExit();

            // 2. Announcement
            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(faintMessage);
            yield return Battle.TurnPauseYield;

            // 3. Hand off to Experience State
            machine.SetState(new PlayerGainExperienceState(machine));
        }
    }
}
