using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Moves.UI
{
    /// <summary>
    /// Displays basic details of a Pokémon move, including power, accuracy, and effect text.
    /// </summary>
    public class MoveDetailUI : MonoBehaviour, IBindable<Move>, IUnbind
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
        public void Bind(Move move)
        {
            if (move?.Definition == null)
            {
                Unbind();
                return;
            }

            powerText.text = move.Definition.Power.ToString();
            accuracyText.text = move.Definition.Accuracy.ToString();
            effectText.text = move.Definition.Effect;
        }

        /// <summary>
        /// Clears all move-related UI elements.
        /// </summary>
        public void Unbind()
        {
            powerText.text = string.Empty;
            accuracyText.text = string.Empty;
            effectText.text = string.Empty;
        }
    }
}
