using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;

namespace MonsterTamer.Battle.States.Intro
{
    /// <summary>
    /// Handles the introductory sequence for a wild battle encounter, 
    /// from the Monster appearing to the player's first action.
    /// </summary>
    internal sealed class WildBattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal WildBattleIntroState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(PlaySequence());

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            yield return PlayOpponentEntrance();
            yield return PlayPlayerEntrance();

            machine.SetState(new PlayerActionMenuState(machine));
        }

        private IEnumerator PlayOpponentEntrance()
        {
            var animation = Battle.Components.Animation;
            var monsterName = Battle.OpponentActiveMonster.Definition.DisplayName;
            var introMessage = BattleMessages.WildIntro(monsterName);

            animation.PlayPlayerTrainerEnter();

            // Visual deployment
            yield return animation.PlayWildMonsterEnter();
            yield return animation.PlayOpponentHudEnter();

            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(introMessage);
        }

        private IEnumerator PlayPlayerEntrance()
        {
            var animation = Battle.Components.Animation;
            var monsterName = Battle.PlayerActiveMonster.Definition.DisplayName;
            var sendMessage = BattleMessages.PlayerSendMonster(monsterName);

            Battle.DialogueBox.ShowDialogue(sendMessage);

            // Swap trainer for monster
            yield return animation.PlayPlayerTrainerExit();
            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();
        }
    }
}
