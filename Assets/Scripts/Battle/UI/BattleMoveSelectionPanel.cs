using System;
using PokemonGame.Menu;
using PokemonGame.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays the player's available moves as interactive buttons.
    /// 
    /// Handles binding moves to UI buttons, highlighting moves,
    /// and confirming move selection.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BattleMoveSelectionPanel : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Button representing the first move slot.")]
        private MenuButton firstMoveButton;

        [SerializeField, Required, Tooltip("Button representing the second move slot.")]
        private MenuButton secondMoveButton;

        [SerializeField, Required, Tooltip("Button representing the third move slot.")]
        private MenuButton thirdMoveButton;

        [SerializeField, Required, Tooltip("Button representing the fourth move slot.")]
        private MenuButton fourthMoveButton;

        // Internal arrays to store temporary event handlers for each button
        private readonly Action[] clickHandlers = new Action[4];
        private readonly Action[] selectHandlers = new Action[4];

        /// <summary>
        /// Raised when a move is confirmed by the player.
        /// </summary>
        internal event Action<MoveInstance> MoveConfirmed;

        /// <summary>
        /// Raised when a move is highlighted (hovered or selected via navigation).
        /// </summary>
        internal event Action<MoveInstance> MoveHighlighted;

        /// <summary>
        /// Convenience property for accessing all four buttons as an array.
        /// </summary>
        private MenuButton[] Buttons => new[] { firstMoveButton, secondMoveButton, thirdMoveButton, fourthMoveButton };

        /// <summary>
        /// Bind an array of moves to the buttons, enabling interaction and events.
        /// </summary>
        /// <param name="moves">Array of 1–4 moves for the active Monster.</param>
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
        /// Clears all button bindings and disables interaction.
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
