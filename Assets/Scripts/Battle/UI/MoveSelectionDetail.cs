using PokemonGame.Move;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays detailed information about the currently selected move,
    /// including its remaining Power Points (PP), elemental type icon,
    /// and damage category icon (Physical, Special, or Status).
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

        [SerializeField, Required]
        [Tooltip("Image showing the move's damage category icon (Physical, Special, or Status).")]
        private Image categoryImage;

        /// <summary>
        /// Binds the given move data to the detail panel, updating PP, type icon, 
        /// and damage category icon.
        /// </summary>
        /// <param name="move">The move to display information for.</param>
        public void Bind(MoveInstance move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            powerPointText.text = $"{move.PowerPointRemaining}/{move.Definition.MoveInfo.PowerPoint}";
            typeImage.sprite = move.Definition.Classification.TypeDefinition.Icon;
            categoryImage.sprite = move.Definition.Classification.CategoryDefinition.Icon;
        }

        /// <summary>
        /// Clears the panel, removing all displayed move information.
        /// </summary>
        public void Unbind()
        {
            powerPointText.text = string.Empty;
            typeImage.sprite = null;
            categoryImage.sprite = null;
        }
    }
}
