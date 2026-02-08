using MonsterTamer.Move;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays detailed information about the currently selected move,
    /// including its remaining Power Points (PP) and elemental type icon.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleMoveInfoPanel : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field displaying the move's remaining and total PP (e.g., '10/15').")]
        private TextMeshProUGUI ppText;

        [SerializeField, Required]
        [Tooltip("Image showing the move's elemental type (e.g., Fire, Water, Grass).")]
        private Image typeIcon;

        /// <summary>
        /// Updates the panel to display information for the given move.
        /// </summary>
        /// <param name="move">The move instance to display.</param>
        public void Bind(MoveInstance move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            ppText.text = $"{move.PowerPointRemaining}/{move.Definition.MoveInfo.PowerPoint}";
            typeIcon.sprite = move.Definition.Classification.TypeDefinition.Icon;
        }

        /// <summary>
        /// Clears the panel, removing all displayed move information.
        /// </summary>
        public void Unbind()
        {
            ppText.text = string.Empty;
            typeIcon.sprite = null;
        }
    }
}
