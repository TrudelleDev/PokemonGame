using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Handles the forced "send out" sequence for a new monster
    /// after the previous monster has fainted.
    /// </summary>
    internal sealed class PlayerForcedSendOutState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly PokemonInstance selectedMonster;

        /// <summary>
        /// Shortcut to the active battle view.
        /// </summary>
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new forced send-out state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        /// <param name="selectedMonster">
        /// The monster to be sent into battle.
        /// </param>
        internal PlayerForcedSendOutState(BattleStateMachine machine, PokemonInstance selectedMonster)
        {
            this.machine = machine;
            this.selectedMonster = selectedMonster;
        }

        /// <summary>
        /// Enters the state and begins the send-out sequence.
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
        /// Plays the forced send-out animation sequence and
        /// transitions back to the player action menu.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var anim = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            // Update the active monster reference immediately
            Battle.SetNextPlayerMonster(selectedMonster);

            // Display the send-out dialogue
            string monsterName = selectedMonster.Definition.DisplayName;
            string sendMessage = string.Format(BattleMessages.PlayerSendMonster, monsterName);

            dialogue.ShowDialogue(sendMessage);

            yield return anim.PlayPlayerMonsterEnter();
            yield return anim.PlayPlayerHudEnter();

            yield return Battle.TurnPauseYield;

            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}
