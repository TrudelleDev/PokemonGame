using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Items.UI.Groups
{
    /// <summary>
    /// Contains references to UI components used for displaying an item's description and icon.
    /// </summary>
    [Serializable]
    public class ItemInfoUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Text component displaying the item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image component displaying the item's icon.")]
        private Image iconImage;

        public TextMeshProUGUI DescriptionText => descriptionText;
        public Image IconImage => iconImage;
    }
}
