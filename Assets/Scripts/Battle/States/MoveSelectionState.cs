using PokemonGame.Battle.UI;
using PokemonGame.Move;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// State where the player selects a move to use in battle.
    /// Transitions to <see cref="PlayerTurnState"/> upon confirmation or back to <see cref="PlayerActionState"/> on cancel.
    /// </summary>
    public sealed class MoveSelectionState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private MoveSelectionView moveSelectionView;

        private BattleView Battle => machine.BattleView;

        internal MoveSelectionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            OpenMoveSelection();
        }

        public void Update() { }

        public void Exit()
        {
            CloseMoveSelection();
        }

        private void OpenMoveSelection()
        {
            // Show UI and get the instance
            moveSelectionView = ViewManager.Instance.Show<MoveSelectionView>();

            // Populate the view with the active Pokémon's moves
            moveSelectionView.BindMoves(Battle.PlayerActivePokemon.Moves.Moves);

            // Subscribe to inputs
            moveSelectionView.OnMoveConfirmed += HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed += HandleCancel;
        }

        private void CloseMoveSelection()
        {
            if (moveSelectionView == null)
                return;

            // Unsubscribe events first to prevent dangling references
            moveSelectionView.OnMoveConfirmed -= HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed -= HandleCancel;

            // Close the view and clear the local reference
            ViewManager.Instance.Close<MoveSelectionView>();
            moveSelectionView = null;
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }

        private void HandleMoveConfirmed(MoveInstance move)
        {
            machine.SetState(new PlayerTurnState(machine, move));
        }
    }
}