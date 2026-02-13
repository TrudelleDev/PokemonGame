using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Characters.Player;
using MonsterTamer.MapEntry;
using MonsterTamer.Views;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the blackout sequence: dialogue, party healing, and teleporting 
    /// the player back to a checkpoint when all monsters have fainted.
    /// </summary>
    internal sealed class PlayerBlackoutState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal PlayerBlackoutState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(PlayBlackoutSequence());
        public void Update() { }
        public void Exit() { }

        private IEnumerator PlayBlackoutSequence()
        {
            var dialogue = Battle.DialogueBox;

            // 1. Initial Message
            yield return dialogue.ShowDialogueAndWaitForInput(BattleMessages.BlackoutMessage);

            // 2. Party Restoration
            Battle.Player.Party.HealAll();

            // 3. Relocation Logic
            MapEntryRegistry.SetNextEntry(MapEntryID.ForestEntrance);
            PlayerRelocator.Instance.RelocatePlayer();

            // 4. Confirmation & Cleanup
            yield return dialogue.ShowDialogueAndWait(BattleMessages.CheckpointRelocationMessage);
            yield return Battle.TurnPauseYield;

            ViewManager.Instance.Close<BattleView>();
        }
    }
}
