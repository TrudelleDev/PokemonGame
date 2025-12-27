using System;
using PokemonGame.Move;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Composite view for move selection. Mediates user input from the panel 
    /// and displays details, raising a single event for the state machine.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class MoveSelectionView : View
    {
        [SerializeField, Required]
        [Tooltip("The panel that contains the 4 move buttons and handles all user input events.")]
        private MoveSelectionPanel moveSelectionPanel;

        [SerializeField, Required]
        [Tooltip("The UI element that displays the type and PP of the currently highlighted move.")]
        private MoveSelectionDetail moveSelectionDetail;

        /// <summary>
        /// Raised when the player confirms a move selection
        /// </summary>
        public event Action<MoveInstance> OnMoveConfirmed;

        private void OnEnable()
        {
            if (moveSelectionPanel == null)
                return;

            moveSelectionPanel.OnMoveConfirmed += HandleMoveConfirmed;
            moveSelectionPanel.OnMoveHighlighted += HandleMoveHighlighted;
        }

        private void OnDisable()
        {
            if (moveSelectionPanel == null)
                return;

            moveSelectionPanel.OnMoveConfirmed -= HandleMoveConfirmed;
            moveSelectionPanel.OnMoveHighlighted -= HandleMoveHighlighted;
        }

        /// /// <summary>
        /// Initializes the UI by populating the move buttons with the Pokémon's available moves.
        /// </summary>
        /// <param name="moves">The array of 1 to 4 available moves to display.</param>
        public void BindMoves(MoveInstance[] moves)
        {
            moveSelectionPanel.BindMoves(moves);

            // Initialize the detail panel immediately for better user experience (UX)
            // by highlighting the first available move.
            if (moves.Length > 0 && moves[0] != null)
            {
                HandleMoveHighlighted(moves[0]);
            }
        }

        private void HandleMoveHighlighted(MoveInstance move)
        {
            // The view acts as a mediator, propagating the selection to the detail pane.
            moveSelectionDetail.Bind(move);
        }

        private void HandleMoveConfirmed(MoveInstance move)
        {
            if (move == null) return;

            // This is the primary event raised to notify the outside controller (the state)
            OnMoveConfirmed?.Invoke(move);
        }
    }
}