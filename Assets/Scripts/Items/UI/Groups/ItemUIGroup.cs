using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Items.UI.Groups
{
    /// <summary>
    /// Contains references to UI elements used to display an item's name and quantity.
    /// </summary>
    [Serializable]
    public class ItemUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Text component displaying the item's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text component displaying the item's quantity.")]
        private TextMeshProUGUI countText;

        public TextMeshProUGUI NameText => nameText;
        public TextMeshProUGUI CountText => countText;
    }
}
