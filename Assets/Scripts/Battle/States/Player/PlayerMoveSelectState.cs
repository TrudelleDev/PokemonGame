using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.UI;
using PokemonGame.Move;
using PokemonGame.Views;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Handles the move selection flow, including UI binding and player input.
    /// </summary>
    internal sealed class PlayerMoveSelectState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleMoveSelectionView moveSelectionView;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new move selection state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerMoveSelectState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and displays the move selection UI.
        /// </summary>
        public void Enter()
        {
            OpenMoveSelection();
        }

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Exits the state and cleans up the move selection UI.
        /// </summary>
        public void Exit()
        {
            CloseMoveSelection();
        }

        /// <summary>
        /// Displays the move selection UI and subscribes to input events.
        /// </summary>
        private void OpenMoveSelection()
        {
            moveSelectionView = ViewManager.Instance.Show<BattleMoveSelectionView>();
            moveSelectionView.BindMoves(Battle.PlayerActiveMonster.Moves.Moves);

            moveSelectionView.OnMoveConfirmed += HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed += HandleCancel;
        }

        /// <summary>
        /// Unsubscribes from input events and closes the move selection UI.
        /// </summary>
        private void CloseMoveSelection()
        {
            if (moveSelectionView == null)
            {
                return;
            }

            moveSelectionView.OnMoveConfirmed -= HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed -= HandleCancel;

            ViewManager.Instance.Close<BattleMoveSelectionView>();
            moveSelectionView = null;
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionMenuState(machine));
        }

        private void HandleMoveConfirmed(MoveInstance move)
        {
            machine.SetState(new PlayerTurnState(machine, move));
        }
    }
}
