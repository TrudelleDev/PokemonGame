using System;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// A serializable container that holds references to UI components
    /// for displaying a move's details,
    /// including power, accuracy, and effect description.
    /// </summary>
    [Serializable]
    public class MoveDescriptionUI
    {
        [Tooltip("Displays the move's base power.")]
        [SerializeField] private TextMeshProUGUI powerText;

        [Tooltip("Displays the move's accuracy percentage.")]
        [SerializeField] private TextMeshProUGUI accuracyText;

        [Tooltip("Displays the move's effect description.")]
        [SerializeField] private TextMeshProUGUI effectText;

        /// <summary>
        /// Text field showing the move's power.
        /// </summary>
        public TextMeshProUGUI PowerText => powerText;

        /// <summary>
        /// Text field showing the move's accuracy.
        /// </summary>
        public TextMeshProUGUI AccuracyText => accuracyText;

        /// <summary>
        /// Text field showing the move's effect description.
        /// </summary>
        public TextMeshProUGUI EffectText => effectText;
    }
}
