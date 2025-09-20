using System;
using PokemonGame.Menu;
using PokemonGame.Summary;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Displays the option menu when selecting a Pokémon in the party.
    /// Provides actions such as opening the summary screen or canceling.
    /// </summary>
    public class PartyMenuOption : View
    {
        [SerializeField, Required]
        [Tooltip("Inner ViewManager used to close this option menu.")]
        private ViewManager innerViewManager;

        [Title("Buttons")]
        [SerializeField, Required]
        [Tooltip("Button to open the SummaryView.")]
        private MenuButton summaryButton;

        [SerializeField, Required]
        [Tooltip("Button to close the option menu and return to the party menu.")]
        private MenuButton cancelButton;

        /// <summary>
        /// Raised when the cancel button is pressed.
        /// </summary>
        public event Action OnCancel;

        private void Awake()
        {
            summaryButton.OnClick += OnSummaryClick;
            cancelButton.OnClick += OnCancelClick;
        }

        private void OnDestroy()
        {
            summaryButton.OnClick -= OnSummaryClick;
            cancelButton.OnClick -= OnCancelClick;
        }

        private void OnSummaryClick()
        {
            ViewManager.Instance.Show<SummaryView>();
        }

        private void OnCancelClick()
        {
            innerViewManager.CloseCurrentView();
            OnCancel?.Invoke();
        }
    }
}
