using MonsterTamer.Move;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays the currently selected move's details, including remaining PP and elemental type icon.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleMoveInfoPanel : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Text showing the move's remaining and total PP (e.g., '10/15').")]
        private TextMeshProUGUI ppText;

        [SerializeField, Required, Tooltip("Image showing the move's elemental type (e.g., Fire, Water, Grass).")]
        private Image typeIcon;

        /// <summary>
        /// Updates the panel to display information for the specified move.
        /// </summary>
        /// <param name="move">Move instance to display.</param>
        public void Bind(MoveInstance move)
        {
            if (move == null || move.Definition == null)
            {
                Unbind();
                return;
            }

            ppText.text = $"{move.PowerPointRemaining}/{move.Definition.MoveInfo.PowerPoint}";
            typeIcon.sprite = move.Definition.Classification.TypeDefinition.Icon;
        }

        /// <summary>
        /// Clears all displayed move information from the panel.
        /// </summary>
        public void Unbind()
        {
            ppText.text = "- / -";
            typeIcon.sprite = null;
        }
    }
}
