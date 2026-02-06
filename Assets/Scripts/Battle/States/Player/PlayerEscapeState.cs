using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Handles the player's successful escape from a wild battle,
    /// including dialogue display and battle termination.
    /// </summary>
    internal sealed class PlayerEscapeState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player escape state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerEscapeState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and plays the escape sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic is required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Displays the escape success message, waits briefly, and closes the battle view.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            yield return Battle.DialogueBox.ShowDialogueAndWait(BattleMessages.EscapeSuccess);
            yield return Battle.TurnPauseYield;

            Battle.CloseBattle();
        }
    }
}
