using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Definition;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Represents a cancel option in a menu that behaves like a regular
    /// selectable entry while exposing display data for detail panels.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MenuButton))]
    public class CancelMenuButton : MonoBehaviour, IDisplayable
    {
        [SerializeField, Required]
        [Tooltip("Definition providing display data for the cancel option.")]
        private CancelMenuOptionDefinition data;

        public string DisplayName => data.DisplayName;
        public string Description => data.Description;
        public Sprite Icon => data.Icon;

        /// <summary>
        /// Raised when the cancel option becomes highlighted.
        /// </summary>
        public event Action<IDisplayable> OnHighlighted;

        /// <summary>
        /// Raised when the cancel option is submitted.
        /// </summary>
        public event Action OnSubmitted;

        private MenuButton button;

        private void Awake()
        {
            button = GetComponent<MenuButton>();
        }

        private void OnEnable()
        {
            button.OnHighlighted += HandleHighlighted;
            button.OnSubmitted += HandleSubmitted;
        }

        private void OnDisable()
        {
            button.OnHighlighted -= HandleHighlighted;
            button.OnSubmitted -= HandleSubmitted;
        }

        private void HandleHighlighted()
        {
            OnHighlighted?.Invoke(this);
        }

        private void HandleSubmitted()
        {
            OnSubmitted?.Invoke();
        }
    }
}
