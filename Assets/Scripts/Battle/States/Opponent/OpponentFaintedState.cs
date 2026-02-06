using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.States.Player;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States.Opponent
{
    /// <summary>
    /// Handles the sequence that plays when the opponent's active Monster faints.
    /// Plays faint animations, shows dialogue, and transitions to the next battle state.
    /// </summary>
    internal sealed class OpponentFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly PokemonInstance monster;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new state to handle an opponent's fainted Monster.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        /// <param name="monster">
        /// The opponent's Monster that just fainted.
        /// </param>
        internal OpponentFaintedState(BattleStateMachine machine, PokemonInstance monster)
        {
            this.machine = machine;
            this.monster = monster;
        }

        /// <summary>
        /// Enters the state and starts the faint sequence.
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
        /// Plays the opponent faint sequence:
        /// 1. Plays faint animation and HUD exit.
        /// 2. Shows faint dialogue.
        /// 3. Checks if the opponent has remaining Monster.
        /// 4. Transitions to either swapping the next opponent Monster or ending the battle.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;
            var message = string.Format(BattleMessages.FaintedMessage, monster.Definition.DisplayName);

            // Trigger visual faint effect
            animation.PlayOpponentMonsterDeath();

            // Sequence HUD exit and dialogue
            yield return animation.PlayOpponentHudExit();
            yield return dialogue.ShowDialogueAndWaitForInput(message);

            yield return Battle.TurnPauseYield;

            // Determine next state
            if (Battle.Opponent.Party.HasUsablePokemon())
            {
                machine.SetState(new OpponentSwapMonsterState(machine));
            }
            else
            {
                machine.SetState(new PlayerWildVictoryState(machine));
            }
        }
    }
}
