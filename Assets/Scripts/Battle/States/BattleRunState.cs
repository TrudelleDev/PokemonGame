using PokemonGame.Dialogue;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the player choosing to run from battle and returns to the overworld.
    /// </summary>
    public class BattleRunState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleRunState"/> class.
        /// </summary>
        /// <param name="machine">The active <see cref="BattleStateMachine"/> managing the battle flow.</param>
        public BattleRunState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when the state is entered.
        /// Displays the escape dialogue and waits for completion before closing the battle view.
        /// </summary>
        public void Enter()
        {
            Battle.DialogueBox.ShowDialogue("Got away safely!");
            Battle.DialogueBox.OnDialogueFinished += OnDialogueFinished;
        }

        private void OnDialogueFinished()
        {
            Battle.DialogueBox.OnDialogueFinished -= OnDialogueFinished;
            ViewManager.Instance.CloseTopView(); // Return to overworld
        }

        public void Update() { }

        public void Exit() { }
    }
}
