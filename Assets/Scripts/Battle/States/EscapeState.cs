using System.Collections;
using PokemonGame.Characters;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the sequence for successfully fleeing a wild Pokémon battle.
    /// This state terminates the battle loop and closes the battle view.
    /// </summary>
    internal sealed class EscapeState : IBattleState
    {
        private const float ClosePause = 1f;

        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        internal EscapeState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Starts the coroutine that executes the escape sequence.
        /// </summary>
        public void Enter()
        {
            BattleView.StartCoroutine(PlaySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var audio = BattleView.Components.Audio;
            var dialogue = BattleView.DialogueBox;

            audio.PlayRunAway();

            yield return dialogue.ShowDialogueAndWait("Got away safely!");
            yield return new WaitForSecondsRealtime(ClosePause);

            ViewManager.Instance.Close<BattleView>();
            BattleView.Player.GetComponent<CharacterStateController>().Unlock(); // Change this later
        }
    }
}