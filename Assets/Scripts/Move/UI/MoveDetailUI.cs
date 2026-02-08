using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Move.UI
{
    /// <summary>
    /// Displays basic details of a Monster move, including power, accuracy, and effect text.
    /// </summary>
    internal sealed class MoveDetailUI : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text element displaying the move's base power.")]
        private TextMeshProUGUI powerText;

        [SerializeField, Required]
        [Tooltip("Text element displaying the move's accuracy percentage.")]
        private TextMeshProUGUI accuracyText;

        [SerializeField, Required]
        [Tooltip("Text element displaying the move's effect description.")]
        private TextMeshProUGUI effectText;

        /// <summary>
        /// Binds the given move's data to the UI elements.
        /// </summary>
        /// <param name="move">The move to display.</param>
        internal void Bind(MoveInstance move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            powerText.text = move.Definition.MoveInfo.Power.ToString();
            accuracyText.text = move.Definition.MoveInfo.Accuracy.ToString();
            effectText.text = move.Definition.Effect;
        }

        /// <summary>
        /// Clears all move-related UI elements.
        /// </summary>
        internal void Unbind()
        {
            powerText.text = string.Empty;
            accuracyText.text = string.Empty;
            effectText.text = string.Empty;
        }
    }
}
