using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Definition
{
    /// <summary>
    /// Defines a menu option that represents a "Cancel" action.
    /// Provides an icon and description for display in the UI.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCancelOption", menuName = "Menu Options/Cancel Option", order = -1)]
    public class CancelMenuOptionDefinition : ScriptableObject, IDisplayable
    {
        [SerializeField]
        [Tooltip("The icon displayed for the cancel option.")]
        private Sprite icon;

        [SerializeField, TextArea]
        [Tooltip("The description text displayed for the cancel option.")]
        private string description;

        [SerializeField, Required]
        private string displayName;

        public string DisplayName => displayName;

        /// <summary>
        /// The icon displayed for this cancel menu option.
        /// </summary>
        public Sprite Icon => icon;

        /// <summary>
        /// The description text shown for this cancel menu option.
        /// </summary>
        public string Description => description;
    }
}
