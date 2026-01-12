using System.Collections;
using PokemonGame.Characters;
using PokemonGame.MapEntry;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the "blackout" sequence when the player has no usable Pokémon remaining.
    /// Manages party healing, world relocation, and view termination.
    /// </summary>
    public sealed class LossState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal LossState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Begins the blackout sequence when the player has no remaining Pokémon.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput("All your Pokémon have fainted!\nYou blacked out...");  
            Battle.Player.Party.HealAll();

            MapEntryRegistry.SetNextEntry(MapEntryID.ViridianForest_Entrance);
            PlayerRelocator.Instance.RelocatePlayer();

            yield return Battle.DialogueBox.ShowDialogueAndWait("You were rushed to\nthe nearest checkpoint.");
            yield return new WaitForSecondsRealtime(1f);

            ViewManager.Instance.Close<BattleView>();
        }
    }
}
