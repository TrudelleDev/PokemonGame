using System.Collections;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles successful escape from a wild Pokémon battle, returning the player to the overworld.
    /// </summary>
    public sealed class EscapeState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="EscapeState"/> class.
        /// </summary>
        /// <param name="machine">The battle state machine context.</param>
        public EscapeState(BattleStateMachine machine)
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

            yield return dialogue.ShowDialogueAndWaitForPlayerAdvance(
                "Got away safely!",
                manualArrowControl: true
            );

            dialogue.Clear();
            ViewManager.Instance.CloseTopView();
        }
    }
}