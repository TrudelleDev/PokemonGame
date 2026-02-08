using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Move.UI
{
    /// <summary>
    /// Displays a compact UI slot for a Monster move, showing its name,
    /// remaining PP, type icon, and damage category icon.
    /// </summary>
    internal sealed class MoveSlotUI : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text displaying the move's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text displaying the move's current and maximum PP.")]
        private TextMeshProUGUI powerPointText;

        [SerializeField, Required]
        [Tooltip("Image displaying the move's elemental type icon.")]
        private Image typeImage;

        public MoveInstance Move { get; private set; }

        /// <summary>
        /// Binds the given move data to the UI slot.
        /// </summary>
        /// <param name="move">The move to display.</param>
        internal void Bind(MoveInstance move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            Move = move;
            nameText.text = move.Definition.DisplayName;
            powerPointText.text = $"{move.PowerPointRemaining}/{move.Definition.MoveInfo.PowerPoint}";
            powerPointText.alignment = TextAlignmentOptions.Right;
            typeImage.sprite = move.Definition.Classification.TypeDefinition.Icon;
            typeImage.enabled = true;
        }

        /// <summary>
        /// Clears all UI elements and resets the slot to an empty state.
        /// </summary>
        internal void Unbind()
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
