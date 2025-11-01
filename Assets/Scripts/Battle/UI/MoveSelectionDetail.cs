using PokemonGame.Moves;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays detailed information about the currently selected move,
    /// including its remaining Power Points (PP) and elemental type icon.
    /// </summary>
    [DisallowMultipleComponent]
    public class MoveSelectionDetail : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field displaying the move's remaining and total PP (e.g., '10/15').")]
        private TextMeshProUGUI powerPointText;

        [SerializeField, Required]
        [Tooltip("Image showing the move's elemental type (e.g., Fire, Water, Grass).")]
        private Image typeImage;

        /// <summary>
        /// Binds the given move data to the detail panel, updating its PP and type visuals.
        /// </summary>
        /// <param name="move">The move to display information for.</param>
        public void Bind(Move move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            powerPointText.text = $"{move.PowerPointRemaining}/{move.Definition.PowerPoint}";
            typeImage.sprite = move.Definition.Type.Sprite;
        }

        /// <summary>
        /// Clears the panel, removing all displayed move data.
        /// </summary>
        public void Unbind()
        {
            powerPointText.text = string.Empty;
            typeImage.sprite = null;
        }
    }
}
