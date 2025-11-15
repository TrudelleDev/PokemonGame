using PokemonGame.Battle.UI;
using PokemonGame.Dialogue;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Displays the player's action menu and waits for input selection.
    /// </summary>
    public class PlayerActionState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerActionState"/> class.
        /// </summary>
        /// <param name="machine">The active <see cref="BattleStateMachine"/> controlling the battle flow.</param>
        public PlayerActionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when the state is entered.
        /// Displays the action prompt and enables the move selection UI.
        /// </summary>
        public void Enter()
        {
            Battle.DialogueBox.ShowDialogue($"What will\n{Battle.PlayerPokemon.Definition.DisplayName} do?", true);
            ViewManager.Instance.Show<PlayerActionPanel>();
        }

        public void Update() { }

        public void Exit() { }
    }
}
