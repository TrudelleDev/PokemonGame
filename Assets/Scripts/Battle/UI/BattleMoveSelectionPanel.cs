using System;
using PokemonGame.Menu;
using PokemonGame.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Display the player's available moves as buttons.
    /// Raises events when moves are highlighted or confirmed.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleMoveSelectionPanel : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Button representing the first move slot.")]
        private MenuButton moveButton1;

        [SerializeField, Required, Tooltip("Button representing the second move slot.")]
        private MenuButton moveButton2;

        [SerializeField, Required, Tooltip("Button representing the third move slot.")]
        private MenuButton moveButton3;

        [SerializeField, Required, Tooltip("Button representing the fourth move slot.")]
        private MenuButton moveButton4;

        private readonly Action[] clickHandlers = new Action[4];
        private readonly Action[] selectHandlers = new Action[4];

        /// <summary>
        /// Invoked when a move is confirmed (clicked or accepted).
        /// </summary>
        internal event Action<MoveInstance> MoveConfirmed;

        /// <summary>
        /// Invoked when a move is highlighted (hovered or navigated to).
        /// </summary>
        internal event Action<MoveInstance> MoveHighlighted;

        // Lazy property to access all buttons easily
        private MenuButton[] Buttons => new[] { moveButton1, moveButton2, moveButton3, moveButton4 };

        /// <summary>
        /// Bind moves to buttons and enable interaction.
        /// </summary>
        /// <param name="moves">Array of 1–4 moves.</param>
        public void BindMoves(MoveInstance[] moves)
        {
            UnbindMoves();

            for (int i = 0; i < Buttons.Length; i++)
            {
                var button = Buttons[i];

                if (i < moves.Length && moves[i] != null)
                {
                    var move = moves[i];
                    button.SetLabel(move.Definition.DisplayName);
                    button.SetInteractable(true);

                    clickHandlers[i] = () => MoveConfirmed?.Invoke(move);
                    button.OnSubmitted += clickHandlers[i];

                    selectHandlers[i] = () => MoveHighlighted?.Invoke(move);
                    button.OnHighlighted += selectHandlers[i];
                }
                else
                {
                    button.SetLabel("-");
                    button.SetInteractable(false);
                }
            }
        }

        /// <summary>
        /// Clear all bindings and disable buttons.
        /// </summary>
        public void UnbindMoves()
        {
            if (Buttons == null) return;

            for (int i = 0; i < Buttons.Length; i++)
            {
                if (clickHandlers[i] != null)
                {
                    Buttons[i].OnSubmitted -= clickHandlers[i];
                    clickHandlers[i] = null;
                }

                if (selectHandlers[i] != null)
                {
                    Buttons[i].OnHighlighted -= selectHandlers[i];
                    selectHandlers[i] = null;
                }

                Buttons[i].SetLabel("-");
                Buttons[i].SetInteractable(false);
            }
        }
    }
}
