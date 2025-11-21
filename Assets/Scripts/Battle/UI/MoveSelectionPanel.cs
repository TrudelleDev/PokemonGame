using System;
using PokemonGame.Menu;
using PokemonGame.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// View-only component that displays the player's available moves as buttons.
    /// Handles UI binding for move labels, selection, and confirmation,
    /// and raises events to notify when a move is highlighted or confirmed.
    /// </summary>
    [DisallowMultipleComponent]
    public class MoveSelectionPanel : MonoBehaviour
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

        private MenuButton[] buttons;
        private readonly Action[] clickHandlers = new Action[4];
        private readonly Action[] selectHandlers = new Action[4];

        /// <summary>
        /// Invoked when a move is confirmed (clicked or accepted).
        /// </summary>
        public event Action<MoveInstance> OnMoveClick;

        /// <summary>
        /// Invoked when a move is highlighted (hovered or navigated to).
        /// </summary>
        public event Action<MoveInstance> OnMoveSelect;

        /// <summary>
        /// Binds the player's available moves to the corresponding buttons.
        /// Enables interactivity and registers event handlers for click and selection.
        /// </summary>
        /// <param name="moves">The list of moves to display.</param>
        public void Bind(MoveInstance[] moves)
        {
            buttons = new[] { moveButton1, moveButton2, moveButton3, moveButton4 };

            Unbind();

            for (int i = 0; i < buttons.Length; i++)
            {
                MenuButton button = buttons[i];

                if (i < moves.Length && moves[i] != null)
                {
                    MoveInstance move = moves[i];
                    button.SetLabel(move.Definition.DisplayName);
                    button.SetInteractable(true);

                    // Confirm click
                    clickHandlers[i] = () => OnMoveClicked(move);
                    button.OnClick += clickHandlers[i];

                    // Highlight / focus select
                    selectHandlers[i] = () => OnMoveSelected(move);
                    button.OnSelect += selectHandlers[i];
                }
            }
        }

        /// <summary>
        /// Clears all bindings and disables interactivity for the move buttons.
        /// </summary>
        public void Unbind()
        {
            if (buttons == null)
                return;

            for (int i = 0; i < buttons.Length; i++)
            {
                MenuButton button = buttons[i];

                if (clickHandlers[i] != null)
                {
                    button.OnClick -= clickHandlers[i];
                    clickHandlers[i] = null;
                }

                if (selectHandlers[i] != null)
                {
                    button.OnSelect -= selectHandlers[i];
                    selectHandlers[i] = null;
                }

                button.SetLabel("-");
                button.SetInteractable(false);
            }
        }

        private void OnMoveClicked(MoveInstance move)
        {
            if (move != null)
                OnMoveClick?.Invoke(move);
        }

        private void OnMoveSelected(MoveInstance move)
        {
            if (move != null)
                OnMoveSelect?.Invoke(move);
        }
    }
}
