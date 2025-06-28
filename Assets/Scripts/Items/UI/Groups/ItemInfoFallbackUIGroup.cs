using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI.Groups
{
    /// <summary>
    /// Contains fallback values for item description and icon display.
    /// Used during development when valid item data is missing.
    /// </summary>
    [Serializable]
    public class ItemInfoFallbackUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Fallback text shown when item description data is unavailable.")]
        private string description = "-";

        [SerializeField, Required]
        [Tooltip("Fallback sprite shown when item icon data is unavailable.")]
        private Sprite icon;

        public string Description => description;
        public Sprite Icon => icon;
    }
}
