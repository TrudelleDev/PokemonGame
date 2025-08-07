using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// Displays basic details of a Pokémon move, including power, accuracy, and effect text,
    /// typically used in summary or battle UI screens.
    /// </summary>
    public class MoveDetailUI : MonoBehaviour, IMoveBind, IUnbind
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
            if (move?.Data == null)
            {
                Unbind();
                return;
            }

            powerText.text = move.Data.Power.ToString();
            accuracyText.text = move.Data.Accuracy.ToString();
            effectText.text = move.Data.Effect;
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
