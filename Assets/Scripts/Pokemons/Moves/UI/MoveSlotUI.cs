using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// A serializable container that holds references to UI components
    /// for displaying a single Pokémon move's information, such as name,
    /// power points (PP), and type icon.
    /// </summary>
    [Serializable]
    public class MoveSlotUI
    {
        [SerializeField]
        [Tooltip("UI text element displaying the move's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField]
        [Tooltip("UI text element displaying the move's current and maximum PP.")]
        private TextMeshProUGUI powerPointText;

        [SerializeField]
        [Tooltip("UI image element displaying the move's type icon.")]
        private Image typeImage;

        /// <summary>
        /// Gets the text component used to display the move's name.
        /// </summary>
        public TextMeshProUGUI NameText => nameText;

        /// <summary>
        /// Gets the text component used to display the move's PP.
        /// </summary>
        public TextMeshProUGUI PowerPointText => powerPointText;

        /// <summary>
        /// Gets the image component used to display the move's type icon.
        /// </summary>
        public Image TypeImage => typeImage;
    }
}
