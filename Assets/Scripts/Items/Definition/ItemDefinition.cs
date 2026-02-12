using MonsterTamer.Items.Enums;
using MonsterTamer.Items.Models;
using MonsterTamer.Monster;
using MonsterTamer.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Items.Definition
{
    /// <summary>
    /// Abstract base definition for all game items.
    /// Stores identity, visuals, description, and category.
    /// </summary>
    internal abstract class ItemDefinition : ScriptableObject, IDisplayable
    {
        protected const string FailMessage = "But it failed...";
        protected const string NoEffectMessage = "It won't have any effect.";

        [SerializeField, Tooltip("Stable unique identifier for this item.")]
        private ItemId id;

        [SerializeField, Required, Tooltip("Display name shown in UI.")]
        private string displayName;

        [SerializeField, Required, Tooltip("Icon displayed for this item.")]
        private Sprite icon;

        [SerializeField, Required, TextArea(5, 10)]
        [Tooltip("Description text shown to the player.")]
        private string description;

        /// <summary>
        /// Stable identifier used for item comparison and serialization.
        /// </summary>
        public ItemId ItemId => id;

        /// <summary>
        /// Name displayed to the player in UI.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Description shown to the player.
        /// </summary>
        public string Description => description;

        /// <summary>
        /// Icon used in inventory and menus.
        /// </summary>
        public Sprite Icon => icon;

        /// <summary>
        /// Applies the item's effect to the target.
        /// </summary>
        /// <param name="target">Target Pokémon instance.</param>
        /// <returns>Result of the item usage.</returns>
        public abstract ItemUseResult Use(MonsterInstance target);
    }
}
