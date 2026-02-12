using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;
using MonsterTamer.Monster;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles a voluntary Monster switch during battle.
    /// Withdraws the currently active Monster, sends out the selected
    /// replacement, and ends the player's turn.
    /// </summary>
    internal sealed class PlayerSwapMonsterState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MonsterInstance monster;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new monster swap state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        /// <param name="monster">
        /// The Monster selected to replace the active one.
        /// </param>
        internal PlayerSwapMonsterState(BattleStateMachine machine, MonsterInstance monster)
        {
            this.machine = machine;
            this.monster = monster;
        }

        /// <summary>
        /// Enters the state and begins the swap animation sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required when exiting this state.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Executes the withdraw and send-out sequence,
        /// then transitions to the opponent's turn.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;
            var activeMonsterName = Battle.PlayerActiveMonster.Definition.DisplayName;

            // Step 1: Withdraw current Monster
            var message = string.Format(BattleMessages.MonsterReturnParty, activeMonsterName);

            yield return dialogue.ShowDialogueAndWait(message);
            yield return Battle.TurnPauseYield;

            animation.PlayPlayerHudExit();
            yield return animation.PlayPlayerMonsterExit();

            // Step 2: Swap the active Monster reference
            Battle.SetNextPlayerMonster(monster);

            // Step 3: Send out the new Monster
            activeMonsterName = Battle.PlayerActiveMonster.Definition.DisplayName;
            var sendMessage = string.Format(BattleMessages.PlayerSendMonster, activeMonsterName);

            dialogue.ShowDialogue(sendMessage);

            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();
            yield return Battle.TurnPauseYield;

            // Switching consumes the player's turn
            machine.SetState(new OpponentTurnState(machine));
        }
    }
}
