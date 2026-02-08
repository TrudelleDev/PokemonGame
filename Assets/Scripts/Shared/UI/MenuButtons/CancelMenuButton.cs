using System;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Shared.UI.Core;
using PokemonGame.Shared.UI.Definitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Shared.UI.MenuButtons
{
    /// <summary>
    /// Represents a cancel option in a menu.
    /// Behaves like a regular selectable entry while exposing
    /// display data for detail panels.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MenuButton))]
    internal sealed class CancelMenuButton : MonoBehaviour, IDisplayable
    {
        [SerializeField, Required]
        [Tooltip("Definition providing display data for the cancel option.")]
        private CancelMenuOptionDefinition definition;

        private MenuButton button;

        /// <summary>
        /// The display name of this cancel option.
        /// </summary>
        public string DisplayName => definition.DisplayName;

        /// <summary>
        /// The description text of this cancel option.
        /// </summary>
        public string Description => definition.Description;

        /// <summary>
        /// The icon associated with this cancel option.
        /// </summary>
        public Sprite Icon => definition.Icon;

        /// <summary>
        /// Raised when the cancel option becomes selected.
        /// </summary>
        internal event Action<IDisplayable> Selected;

        /// <summary>
        /// Raised when the cancel option is confirmed.
        /// </summary>
        internal event Action Confirmed;

        private void Awake()
        {
            button = GetComponent<MenuButton>();
        }

        private void OnEnable()
        {
            button.Selected += OnSelected;
            button.Confirmed += OnConfirmed;
        }

        private void OnDisable()
        {
            button.Selected -= OnSelected;
            button.Confirmed -= OnConfirmed;
        }

        private void OnSelected()
        {
            Selected?.Invoke(this);
        }

        private void OnConfirmed()
        {
            Confirmed?.Invoke();
        }
    }
}
        