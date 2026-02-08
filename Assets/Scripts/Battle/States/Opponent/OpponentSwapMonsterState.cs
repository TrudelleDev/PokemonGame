using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;

namespace MonsterTamer.Battle.States.Opponent
{
    /// <summary>
    /// Handles the transition and animation sequence for the opponent sending out their next available Monster.
    /// Plays entry animations, shows dialogue, and updates the battle state.
    /// </summary>
    internal sealed class OpponentSwapMonsterState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new state for swapping in the opponent's next Monster.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal OpponentSwapMonsterState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and starts the swap sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic required.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Performs the swap sequence:
        /// 1. Selects the next usable opponent Monster.
        /// 2. Updates the battle reference and shows dialogue.
        /// 3. Plays entry animations for the trainer and Monster HUD.
        /// 4. Pauses briefly before transitioning back to the player's action menu.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var monster = Battle.Opponent.Party.GetFirstUsablePokemon();

            // No usable monsters? Player wins.
            if (monster == null)
            {
                machine.SetState(new PlayerWildVictoryState(machine));
                yield break;
            }

            // Update opponent's active Monster
            Battle.SetNextOpponentMonster(monster);

            string trainerName = Battle.Opponent.Definition.DisplayName;
            string monsterName = monster.Definition.DisplayName;
            string message = string.Format(BattleMessages.TrainerSentOut, trainerName, monsterName);

            // Dialogue and animations
            yield return Battle.DialogueBox.ShowDialogueAndWait(message);
            yield return Battle.Components.Animation.PlayOpponentMonsterEnter();
            yield return Battle.Components.Animation.PlayOpponentHudEnter();

            yield return Battle.TurnPauseYield;

            // Transition to player's turn
            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}
