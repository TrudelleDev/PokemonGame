using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Pokemon;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the visual and logical sequence when the player's active monster faints.
    /// </summary>
    internal sealed class PlayerFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly PokemonInstance faintedMonster;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player fainted state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        /// <param name="faintedMonster">
        /// The monster that has fainted.
        /// </param>
        internal PlayerFaintedState(BattleStateMachine machine, PokemonInstance faintedMonster)
        {
            this.machine = machine;
            this.faintedMonster = faintedMonster;
        }

        /// <summary>
        /// Enters the state and begins the faint sequence.
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
        /// Executes the faint sequence and transitions to either
        /// forced party selection or blackout based on party state.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            animation.PlayPlayerMonsterDeath();
            animation.PlayPlayerHudExit();

            var message = string.Format(
                BattleMessages.FaintedMessage,
                faintedMonster.Definition.DisplayName);

            yield return dialogue.ShowDialogueAndWaitForInput(message);

            // Decision logic: can the player continue?
            if (Battle.Player.Party.HasUsablePokemon())
            {
                machine.SetState(new PlayerPartySelectState(machine, isForced: true));
            }
            else
            {
                machine.SetState(new PlayerBlackoutState(machine));
            }
        }
    }
}
