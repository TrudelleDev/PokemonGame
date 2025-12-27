using System;
using PokemonGame.Menu;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI.PartyOptions
{
    /// <summary>
    /// UI view for the party Pokémon option menu.
    /// Raises intent events when the player requests to switch Pokémon,
    /// view the summary, or cancel the menu.
    /// Contains no game or flow logic.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuOptionsView : View
    {
        [SerializeField, Required]
        [Tooltip("Button used to open the selected Pokémon's summary.")]
        private MenuButton summaryButton;

        [SerializeField, Required]
        [Tooltip("Button used to initiate switching the selected Pokémon.")]
        private MenuButton switchButton;

        [SerializeField, Required]
        [Tooltip("Button used to cancel and close the option menu.")]
        private MenuButton cancelButton;

        /// <summary>
        /// Raised when the player requests to switch the selected Pokémon.
        /// </summary>
        internal event Action SwitchRequested;

        /// <summary>
        /// Raised when the player requests to switch the selected Pokémon.
        /// </summary>
        internal event Action SummaryRequested;

        /// <summary>
        /// Raised when the player requests to cancel and close the option menu.
        /// </summary>
        internal event Action CancelRequested;

        private void OnEnable()
        {
            summaryButton.OnSubmitted += OnSummaryRequested;
            switchButton.OnSubmitted += OnSwitchRequested;
            cancelButton.OnSubmitted += OnCancelRequested;

            // Base view event
            CancelKeyPressed += OnCancelRequested;

            ResetMenuController();
        }

        private void OnDisable()
        {
            summaryButton.OnSubmitted -= OnSummaryRequested;
            switchButton.OnSubmitted -= OnSwitchRequested;
            cancelButton.OnSubmitted -= OnCancelRequested;

            // Base view event
            CancelKeyPressed -= OnCancelRequested;
        }

        private void OnSummaryRequested()
        {
            SummaryRequested?.Invoke();
        }

        private void OnSwitchRequested()
        {
            SwitchRequested?.Invoke();
        }

        private void OnCancelRequested()
        {
            CancelRequested?.Invoke();
        }
    }
}
