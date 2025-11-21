using PokemonGame.Battle.States;
using PokemonGame.Move;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Controls the logic and interaction between the <see cref="MoveSelectionPanel"/> and <see cref="MoveSelectionDetail"/>.
    /// Handles move selection and confirmation, and transitions the battle to the player’s turn.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MoveSelectionView : View
    {
        [SerializeField, Required]
        [Tooltip("Panel displaying the player's available moves for selection.")]
        private MoveSelectionPanel moveSelectionPanel;

        [SerializeField, Required]
        [Tooltip("UI element showing additional details (PP, type) of the selected move.")]
        private MoveSelectionDetail moveSelectionDetail;

        private BattleStateMachine stateMachine;

        /// <summary>
        /// Initializes the move selection controller with the provided state machine and moves.
        /// Binds event handlers for selection and confirmation.
        /// </summary>
        /// <param name="stateMachine">The active battle state machine controlling the battle flow.</param>
        /// <param name="moves">The list of moves available to the player.</param>
        public void Initialize(BattleStateMachine stateMachine, MoveInstance[] moves)
        {
            this.stateMachine = stateMachine;
            moveSelectionPanel.Bind(moves);
        }

        private void OnEnable()
        {
            moveSelectionPanel.OnMoveClick += OnMoveConfirmed;
            moveSelectionPanel.OnMoveSelect += OnMoveSelected;
        }

        public void OnDisable()
        {
            if (moveSelectionPanel != null)
            {
                moveSelectionPanel.OnMoveClick -= OnMoveConfirmed;
                moveSelectionPanel.OnMoveSelect -= OnMoveSelected;
            }
        }

        public override void Close()
        {
            stateMachine.SetState(new PlayerActionState(stateMachine));
        }

        private void OnMoveSelected(MoveInstance move)
        {
            moveSelectionDetail.Bind(move);
        }

        private void OnMoveConfirmed(MoveInstance move)
        {
            if (move == null || stateMachine == null)
            {
                return;
            }

            ViewManager.Instance.CloseTopView();
            stateMachine.SetState(new PlayerTurnState(stateMachine, move));
        }
    }
}
