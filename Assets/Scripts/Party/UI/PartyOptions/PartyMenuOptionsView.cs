using System;
using PokemonGame.Shared.UI.Core;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI.PartyOptions
{
    /// <summary>
    /// UI view for the party Monster option menu.
    /// Raises intent events when the player requests to swap Monster,
    /// view the info, or cancel the menu.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PartyMenuOptionsView : View
    {
        [SerializeField, Required]
        [Tooltip("Button used to open the selected Monster's info.")]
        private MenuButton summaryButton;

        [SerializeField, Required]
        [Tooltip("Button used to initiate swaping the selected Monster.")]
        private MenuButton switchButton;

        public event Action SwapRequested;
        public event Action InfoRequested;
        public event Action CancelRequested;

        private void OnEnable()
        {
            summaryButton.Confirmed += OnInfoRequested;
            switchButton.Confirmed += OnSwapRequested;

            // Base view event
            ReturnKeyPressed += OnReturnRequested;

            ResetMenuController();
        }

        private void OnDisable()
        {
            summaryButton.Confirmed -= OnInfoRequested;
            switchButton.Confirmed -= OnSwapRequested;

            // Base view event
            ReturnKeyPressed -= OnReturnRequested;
        }

        private void OnInfoRequested()
        {
            InfoRequested?.Invoke();
        }

        private void OnSwapRequested()
        {
            SwapRequested?.Invoke();
        }

        private void OnReturnRequested()
        {
            CancelRequested?.Invoke();
        }
    }
}
