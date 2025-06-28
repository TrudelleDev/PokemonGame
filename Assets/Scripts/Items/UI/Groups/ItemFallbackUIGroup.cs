using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI.Groups
{
    /// <summary>
    /// Contains fallback text values displayed when no item data is bound.
    /// Used primarily for development-time UI safety.
    /// </summary>
    [Serializable]
    public class ItemFallbackUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Fallback text shown when the item name is unavailable.")]
        private string nameText = "-";

        [SerializeField, Required]
        [Tooltip("Fallback text shown when the item count is unavailable.")]
        private string countText = "-";

        public string NameText => nameText;
        public string CountText => countText;
    }
}
