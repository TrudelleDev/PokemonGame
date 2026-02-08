using System.Collections;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Views;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the victory flow for trainer battles.
    /// Plays end-of-battle animations, displays dialogue, and closes the battle view.
    /// </summary>
    internal sealed class PlayerTrainerVictoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player trainer victory state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling the battle flow.
        /// </param>
        internal PlayerTrainerVictoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins the trainer victory sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Plays the trainer victory sequence:
        /// 1. Plays the player's end-of-battle sprite animation.
        /// 2. Displays the trainer's end-of-battle dialogue.
        /// 3. Pauses briefly, then closes the battle view.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var opponent = Battle.Opponent.Definition;
            var dialogue = Battle.DialogueBox;

            yield return animation.PlayOpponentTrainerDefeatOutro();
            yield return dialogue.ShowDialogueAndWaitForInput(opponent.EndBattleDialogue);
            yield return Battle.TurnPauseYield;

            Battle.CloseBattle();
        }
    }
}
