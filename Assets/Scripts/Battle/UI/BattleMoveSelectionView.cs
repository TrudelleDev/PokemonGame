using System;
using MonsterTamer.Move;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Composite view for move selection. Mediates input from the panel 
    /// and displays move details, raising a single event for the state machine.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleMoveSelectionView : View
    {
        [SerializeField, Required, Tooltip("The panel with 4 move buttons that handles user input events.")]
        private BattleMoveSelectionPanel moveSelectionPanel;

        [SerializeField, Required, Tooltip("Displays type and PP for the currently highlighted move.")]
        private BattleMoveInfoPanel moveSelectionDetail;

        internal event Action<MoveInstance> OnMoveConfirmed;

        private void OnEnable()
        {
            moveSelectionPanel.MoveConfirmed += HandleMoveConfirmed;
            moveSelectionPanel.MoveHighlighted += HandleMoveHighlighted;
        }

        private void OnDisable()
        {
            moveSelectionPanel.MoveConfirmed -= HandleMoveConfirmed;
            moveSelectionPanel.MoveHighlighted -= HandleMoveHighlighted;

            ResetMenuController();
        }

        /// <summary>
        /// Populates the move buttons with the given moves and highlights the first move.
        /// </summary>
        /// <param name="moves">Array of 1–4 available moves.</param>
        public void BindMoves(MoveInstance[] moves)
        {
            if (moves == null || moves.Length == 0) return;

            moveSelectionPanel.BindMoves(moves);

            // Highlight the first move by default
            HandleMoveHighlighted(moves[0]);
        }

        private void HandleMoveHighlighted(MoveInstance move) => moveSelectionDetail.Bind(move);

        private void HandleMoveConfirmed(MoveInstance move) => OnMoveConfirmed?.Invoke(move);
    }
}
