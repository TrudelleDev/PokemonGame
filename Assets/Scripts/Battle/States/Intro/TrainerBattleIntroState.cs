using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;

namespace MonsterTamer.Battle.States.Intro
{
    /// <summary>
    /// Manages the opening sequence of a trainer battle, coordinating dialogue,
    /// trainer animations, and initial Monster deployment.
    /// </summary>
    internal sealed class TrainerBattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal TrainerBattleIntroState(BattleStateMachine machine) => this.machine = machine;

        public void Enter()=> Battle.StartCoroutine(PlaySequence());

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
            var monster = Battle.OpponentActiveMonster;
            var trainer = Battle.Opponent.Definition;
            var introMessage = BattleMessages.TrainerIntro(trainer.DisplayName);
            var sendMessage = BattleMessages.TrainerSentOut(trainer.DisplayName, monster.Definition.DisplayName);

            // Visual entrance
            animation.PlayPlayerTrainerEnter();
            yield return animation.PlayOpponentTrainerEnter();

            // Dialogue
            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(introMessage);
            Battle.DialogueBox.ShowDialogue(sendMessage);

            // Transition to Monster
            yield return animation.PlayOpponentTrainerExit();
            yield return animation.PlayOpponentMonsterEnter();
            yield return animation.PlayOpponentHudEnter();
        }

        private IEnumerator PlayPlayerEntrance()
        {
            var anim = Battle.Components.Animation;
            var monster = Battle.PlayerActiveMonster;
            var sendMessage = BattleMessages.PlayerSendMonster(monster.Definition.DisplayName);

            Battle.DialogueBox.ShowDialogue(sendMessage);

            yield return anim.PlayPlayerTrainerExit();
            yield return anim.PlayPlayerMonsterEnter();
            yield return anim.PlayPlayerHudEnter();
        }
    }
}
