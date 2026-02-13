using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;

namespace MonsterTamer.Battle.States.Opponent
{
    /// <summary>
    /// Handles the transition and animation sequence for the opponent sending out their next Monster.
    /// </summary>
    internal sealed class OpponentSendOutState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal OpponentSendOutState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(PlaySequence());

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var monster = Battle.Opponent.Party.GetFirstUsableMonster();

            if (monster is null)
            {
                machine.SetState(new PlayerWildVictoryState(machine));
                yield break;
            }

            Battle.SetNextOpponentMonster(monster);

            // Messages
            var trainerName = Battle.Opponent.Definition.DisplayName;
            var monsterName = monster.Definition.DisplayName;
            var sendMessage = BattleMessages.TrainerSentOut(trainerName, monsterName);

            yield return Battle.DialogueBox.ShowDialogueAndWait(sendMessage);

            // Visual Sequence
            var anim = Battle.Components.Animation;
            yield return anim.PlayOpponentMonsterEnter();
            yield return anim.PlayOpponentHudEnter();

            yield return Battle.TurnPauseYield;

            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}
