using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.UI;
using MonsterTamer.Move;
using MonsterTamer.Views;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the move selection flow, including UI binding and player input.
    /// </summary>
    internal sealed class PlayerMoveSelectState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleMoveSelectionView moveSelectionView;
        private BattleView Battle => machine.BattleView;

        internal PlayerMoveSelectState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => OpenMoveSelection();
        public void Update() { }
        public void Exit() => CloseMoveSelection();

        private void OpenMoveSelection()
        {
            moveSelectionView = ViewManager.Instance.Show<BattleMoveSelectionView>();
            moveSelectionView.BindMoves(Battle.PlayerActiveMonster.Moves.Moves);

            moveSelectionView.OnMoveConfirmed += HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed += HandleCancel;
        }

        private void CloseMoveSelection()
        {
            if (moveSelectionView == null) return;

            moveSelectionView.OnMoveConfirmed -= HandleMoveConfirmed;
            moveSelectionView.ReturnKeyPressed -= HandleCancel;

            ViewManager.Instance.Close<BattleMoveSelectionView>();
            moveSelectionView = null;
        }

        private void HandleCancel() => machine.SetState(new PlayerActionMenuState(machine));

        private void HandleMoveConfirmed(MoveInstance move) => machine.SetState(new BattleSpeedCheckState(machine, move));
    }
}
