using System.Collections;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Views;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the victory flow for trainer battles, playing the defeat outro and trainer dialogue.
    /// </summary>
    internal sealed class PlayerTrainerVictoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal PlayerTrainerVictoryState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(PlaySequence());
        public void Exit() { }
        public void Update() { }

        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var opponent = Battle.Opponent.Definition;
            var dialogue = Battle.DialogueBox;

            // 1. Clear Battle UI for a cleaner cinematic feel
            animation.PlayPlayerHudExit();
            yield return animation.PlayOpponentHudExit();

            // 2. Trainer Outro Logic
            yield return animation.PlayOpponentTrainerDefeatOutro();
            yield return dialogue.ShowDialogueAndWaitForInput(opponent.PostBattleClosingDialogue);

            yield return Battle.TurnPauseYield;

            // 3. Return to Overworld
            Battle.CloseBattle();
        }
    }
}