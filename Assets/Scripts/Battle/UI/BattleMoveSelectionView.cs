using System;
using MonsterTamer.Move;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Composite view for move selection. Mediates user input from the panel 
    /// and displays details, raising a single event for the state machine.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleMoveSelectionView : View
    {
        [SerializeField, Required]
        [Tooltip("The panel that contains the 4 move buttons and handles all user input events.")]
        private BattleMoveSelectionPanel moveSelectionPanel;

        [SerializeField, Required]
        [Tooltip("The UI element that displays the type and PP of the currently highlighted move.")]
        private BattleMoveInfoPanel moveSelectionDetail;

        /// <summary>
        /// Raised when the player confirms a move selection
        /// </summary>
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
        /// Initializes the UI by populating the move buttons with the Pokémon's available moves.
        /// </summary>
        /// <param name="moves">The array of 1 to 4 available moves to display.</param>
        public void BindMoves(MoveInstance[] moves)
        {
            moveSelectionPanel.BindMoves(moves);

            // Initialize the detail panel immediately by highlighting the first available move.
            HandleMoveHighlighted(moves[0]);
        }

        private void HandleMoveHighlighted(MoveInstance move)
        {
            moveSelectionDetail.Bind(move);
        }

        private void HandleMoveConfirmed(MoveInstance move)
        {
            OnMoveConfirmed?.Invoke(move);
        }
    }
}