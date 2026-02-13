using System;
using MonsterTamer.Move;
using MonsterTamer.Shared.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays the player's available moves as interactive buttons.
    /// Handles binding moves, highlighting, and confirming selection.
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

        private MenuButton[] buttons;
        private readonly Action[] clickHandlers = new Action[4];
        private readonly Action[] selectHandlers = new Action[4];

        internal event Action<MoveInstance> MoveConfirmed;
        internal event Action<MoveInstance> MoveHighlighted;

        private void Awake()
        {
            // Cache button references to avoid allocating array repeatedly
            buttons = new[] { firstMoveButton, secondMoveButton, thirdMoveButton, fourthMoveButton };
        }

        /// <summary>
        /// Binds moves to the panel buttons, enabling interaction and events.
        /// </summary>
        /// <param name="moves">Array of 1–4 moves for the active Monster.</param>
        internal void BindMoves(MoveInstance[] moves)
        {
            UnbindMoves();

            if (moves == null) return;

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];

                if (i < moves.Length && moves[i] != null)
                {
                    var move = moves[i];

                    button.SetLabel(move.Definition.DisplayName);
                    button.SetInteractable(true);

                    clickHandlers[i] = () => MoveConfirmed?.Invoke(move);
                    button.Confirmed += clickHandlers[i];

                    selectHandlers[i] = () => MoveHighlighted?.Invoke(move);
                    button.Selected += selectHandlers[i];
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
            if (buttons == null) return;

            for (int i = 0; i < buttons.Length; i++)
            {
                if (clickHandlers[i] != null)
                {
                    buttons[i].Confirmed -= clickHandlers[i];
                    clickHandlers[i] = null;
                }

                if (selectHandlers[i] != null)
                {
                    buttons[i].Selected -= selectHandlers[i];
                    selectHandlers[i] = null;
                }

                buttons[i].SetLabel("-");
                buttons[i].SetInteractable(false);
            }
        }
    }
}
