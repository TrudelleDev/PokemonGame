using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Characters.Player;
using PokemonGame.MapEntry;
using PokemonGame.Views;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Handles the blackout sequence when the player has no usable monsters remaining.
    /// </summary>
    internal sealed class PlayerBlackoutState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player blackout state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerBlackoutState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins the blackout sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlayBlackoutSequence());
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
        /// Executes the blackout sequence, including dialogue,
        /// party restoration, and player relocation.
        /// </summary>
        private IEnumerator PlayBlackoutSequence()
        {
            var dialogue = Battle.DialogueBox;

            yield return dialogue.ShowDialogueAndWaitForInput(BattleMessages.BlackoutMessage);

            // Party restoration
            Battle.Player.Party.HealAll();

            // Relocation logic
            MapEntryRegistry.SetNextEntry(MapEntryID.ForestEntrance);
            PlayerRelocator.Instance.RelocatePlayer();

            yield return dialogue.ShowDialogueAndWait(BattleMessages.CheckpointRelocationMessage);

            yield return Battle.TurnPauseYield;

            // Cleanly terminate the battle view
            ViewManager.Instance.Close<BattleView>();
        }
    }
}
