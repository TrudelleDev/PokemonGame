using PokemonGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// Displays a summary slot for a Pokémon move, including its name, PP, and type icon.
    /// </summary>
    public class MoveSlotUI : MonoBehaviour, IMoveBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text displaying the move's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text displaying the move's current and maximum PP.")]
        private TextMeshProUGUI powerPointText;

        [SerializeField, Required]
        [Tooltip("Image displaying the move's type icon.")]
        private Image typeImage;

        public Move Move { get; private set; }

        /// <summary>
        /// Binds the move data to UI elements.
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
            nameText.text = move.Data.MoveName;
            powerPointText.text = $"{move.PowerPointRemaining}/{move.Data.PowerPoint}";
            powerPointText.alignment = TextAlignmentOptions.Right;
            typeImage.sprite = move.Data.Type.Sprite;
            typeImage.enabled = true;
        }

        /// <summary>
        /// Clears all move UI elements.
        /// </summary>
        public void Unbind()
        {
            Move = null;
            nameText.text = "-";
            powerPointText.text = "--";
            powerPointText.alignment = TextAlignmentOptions.Left;
            typeImage.sprite = null;
            typeImage.enabled = false;
        }
    }
}
