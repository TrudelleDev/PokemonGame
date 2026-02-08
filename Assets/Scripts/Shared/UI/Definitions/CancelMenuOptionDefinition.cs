using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Shared.UI.Definitions
{
    /// <summary>
    /// Defines a menu option representing a "Cancel" action.
    /// Provides a display name, icon, and description for UI presentation.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Menu Options/Cancel Option")]
    internal class CancelMenuOptionDefinition : ScriptableObject, IDisplayable
    {
        [SerializeField, Required]
        [Tooltip("The display name of the cancel option.")]
        private string displayName;

        [SerializeField]
        [Tooltip("The icon displayed for the cancel option.")]
        private Sprite icon;

        [SerializeField, TextArea]
        [Tooltip("The description text shown for the cancel option.")]
        private string description;

        /// <summary>
        /// The display name of this cancel menu option.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// The icon shown for this cancel menu option.
        /// </summary>
        public Sprite Icon => icon;

        /// <summary>
        /// The description text shown for this cancel menu option.
        /// </summary>
        public string Description => description;
    }
}
