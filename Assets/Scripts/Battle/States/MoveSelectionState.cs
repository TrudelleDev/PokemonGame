using PokemonGame.Battle.UI;
using PokemonGame.Move;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// State where the player selects a move to use in battle.
    /// Transitions to <see cref="PlayerTurnState"/> upon move confirmation or back to 
    /// <see cref="PlayerActionState"/> upon cancellation.
    /// </summary>
    public sealed class MoveSelectionState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private MoveSelectionView moveSelectionView;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new move selection state.
        /// </summary>
        /// <param name="machine">The battle state machine context.</param>
        public MoveSelectionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Displays the move selection UI and waits for player input.
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
        /// Cleans up UI and event subscriptions when leaving the state.
        /// </summary>
        public void Exit()
        {
            CloseMoveSelection();
        }

        private void OpenMoveSelection()
        {
            // Show UI and get the instance
            moveSelectionView = ViewManager.Instance.Show<MoveSelectionView>();

            // Populate the view with the active Pokémon's moves
            moveSelectionView.BindMoves(Battle.PlayerPokemon.Moves.Moves);

            // Subscribe to inputs
            moveSelectionView.OnMoveConfirmed += OnMoveConfirmed;
            moveSelectionView.OnCloseKeyPress += OnCancel;
        }

        private void CloseMoveSelection()
        {
            if (moveSelectionView == null)
                return;

            // Unsubscribe events first to prevent dangling references
            moveSelectionView.OnMoveConfirmed -= OnMoveConfirmed;
            moveSelectionView.OnCloseKeyPress -= OnCancel;

            // Close the view and clear the local reference
            ViewManager.Instance.Close<MoveSelectionView>();
            moveSelectionView = null;
        }

        private void OnCancel()
        {
            // Transition back to the main action menuy
            machine.SetState(new PlayerActionState(machine));
        }

        private void OnMoveConfirmed(MoveInstance move)
        {
            machine.SetState(new PlayerTurnState(machine, move));
        }
    }
}