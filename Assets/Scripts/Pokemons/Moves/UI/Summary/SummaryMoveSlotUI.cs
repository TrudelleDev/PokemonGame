using PokemonGame.Shared.Interfaces;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    /// <summary>
    /// UI component that displays a move's name, remaining power points, and type.
    /// </summary>
    public class SummaryMoveSlotUI : MonoBehaviour, IMoveBind, IUnbind
    {
        [SerializeField] private MoveSlotUI moveSlot;

        public Move Move { get; private set; }

        /// <summary>
        /// Binds a move to this UI element and displays its data.
        /// </summary>
        /// <param name="move">The move to display.</param>
        public void Bind(Move move)
        {
            if (move?.Data == null)
            {
                Unbind();
                return;
            }

            Move = move;

            moveSlot.NameText.text = move.Data.MoveName;
            moveSlot.PowerPointText.text = $"{move.PowerPointRemaining}/{move.Data.PowerPoint}";
            moveSlot.TypeImage.sprite = move.Data.Type.Sprite;
            moveSlot.TypeImage.enabled = true;
        }

        /// <summary>
        /// Clears the current move from the UI.
        /// </summary>
        public void Unbind()
        {
            Move = null;

            moveSlot.NameText.text = "-";
            moveSlot.PowerPointText.text = "--";
            moveSlot.TypeImage.enabled = false;
        }
    }
}
