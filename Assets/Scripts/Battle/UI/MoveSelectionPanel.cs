using System;
using PokemonGame.Menu;
using PokemonGame.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// View-only component that displays the player's available moves as buttons.
    /// Manages UI binding for move labels, selection, and confirmation,
    /// and raises events to notify when a move is highlighted or confirmed.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MoveSelectionPanel : MonoBehaviour
    {
        [Title("Move Buttons")]
        [SerializeField, Required, Tooltip("Button representing the first move slot.")]
        private MenuButton moveButton1;

        [SerializeField, Required, Tooltip("Button representing the second move slot.")]
        private MenuButton moveButton2;

        [SerializeField, Required, Tooltip("Button representing the third move slot.")]
        private MenuButton moveButton3;

        [SerializeField, Required, Tooltip("Button representing the fourth move slot.")]
        private MenuButton moveButton4;

        // Use a private property for the buttons array for lazy initialization and clarity
        private MenuButton[] Buttons => buttons ??= new[] { moveButton1, moveButton2, moveButton3, moveButton4 };
        private MenuButton[] buttons;

        // Arrays to store delegates, preventing issues with closures and allowing clean unbinding
        private readonly Action[] clickHandlers = new Action[4];
        private readonly Action[] selectHandlers = new Action[4];

        /// <summary>
        /// Invoked when a move is confirmed (clicked or accepted).
        /// </summary>
        public event Action<MoveInstance> OnMoveConfirmed;

        /// <summary>
        /// Invoked when a move is highlighted (hovered or navigated to).
        /// </summary>
        public event Action<MoveInstance> OnMoveHighlighted;

        // --- Public Binding Methods ---

        /// <summary>
        /// Binds the player's available moves to the corresponding buttons.
        /// Enables interactivity and registers event handlers for click and selection.
        /// </summary>
        /// <param name="moves">The list of moves to display (up to 4).</param>
        public void BindMoves(MoveInstance[] moves)
        {
            UnbindMoves(); // Always clean up before binding

            for (int i = 0; i < Buttons.Length; i++)
            {
                MenuButton button = Buttons[i];

                if (i < moves.Length && moves[i] != null)
                {
                    MoveInstance move = moves[i];
                    button.SetLabel(move.Definition.DisplayName);
                    button.SetInteractable(true);

                    // 1. Create and store the Click handler delegate
                    clickHandlers[i] = () => OnMoveConfirmedInternal(move);
                    button.OnClick += clickHandlers[i];

                    // 2. Create and store the Select handler delegate
                    selectHandlers[i] = () => OnMoveHighlightedInternal(move);
                    button.OnSelect += selectHandlers[i];
                }
                else
                {
                    // Ensure unused slots are visually disabled
                    button.SetLabel("-");
                    button.SetInteractable(false);
                }
            }
        }

        /// <summary>
        /// Clears all bindings and disables interactivity for the move buttons.
        /// </summary>
        public void UnbindMoves()
        {
            if (Buttons == null)
                return;

            for (int i = 0; i < Buttons.Length; i++)
            {
                MenuButton button = Buttons[i];

                // Unsubscribe Click Handler
                if (clickHandlers[i] != null)
                {
                    button.OnClick -= clickHandlers[i];
                    clickHandlers[i] = null;
                }

                // Unsubscribe Select Handler
                if (selectHandlers[i] != null)
                {
                    button.OnSelect -= selectHandlers[i];
                    selectHandlers[i] = null;
                }

                // Reset UI state
                button.SetLabel("-");
                button.SetInteractable(false);
            }
        }

        private void OnMoveConfirmedInternal(MoveInstance move)
        {
            if (move != null)
                OnMoveConfirmed?.Invoke(move);
        }

        private void OnMoveHighlightedInternal(MoveInstance move)
        {
            if (move != null)
                OnMoveHighlighted?.Invoke(move);
        }
    }
}