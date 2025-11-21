using PokemonGame.Items.Enums;
using PokemonGame.Pokemon;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Abstract base definition for all game items.
    /// Stores identity, visuals, description, and category.
    /// </summary>
    public abstract class ItemDefinition : ScriptableObject, IDisplayable
    {
        // ---- Identity ----

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this item.")]
        [SerializeField, Required]
        private ItemId id;

        [BoxGroup("Identity")]
        [Tooltip("Display name shown in UI.")]
        [SerializeField, Required]
        private string displayName;

        // ---- Visuals ----

        [BoxGroup("Visuals")]
        [Tooltip("Icon displayed for this item.")]
        [SerializeField]
        private Sprite icon;

        // ---- Description ----

        [BoxGroup("Description")]
        [Tooltip("Description text shown to the player.")]
        [SerializeField, TextArea(5, 10)]
        private string description;

        // ---- Classification ----

        [BoxGroup("Classification")]
        [Tooltip("High-level item category (e.g., Healing, Battle, Key).")]
        [SerializeField, Required]
        private ItemCategory category;

        // ---- Properties ----

        /// <summary>
        /// Unique identifier used to reference this item.
        /// </summary>
        public ItemId ItemId => id;

        /// <summary>
        /// Display name of the item.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Description shown to the player.
        /// </summary>
        public string Description => description;

        /// <summary>
        /// Icon representing the item.
        /// </summary>
        public Sprite Icon => icon;

        /// <summary>
        /// Item category.
        /// </summary>
        public ItemCategory Category => category;

        public abstract ItemUseResult Use(PokemonInstance target);
    }
}
