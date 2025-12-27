using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI.PartyOptions
{
    /// <summary>
    /// Handles user interaction for the Party Menu Options view.
    /// Raises events for the presenter when an option is selected.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuOptionsController : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Reference to the Party Menu Options view that raises option events.")]
        private PartyMenuOptionsView view;

        /// <summary>
        /// Raised when the 'Switch' option is selected.
        /// </summary>
        internal event Action SwitchSelected;

        /// <summary>
        /// Raised when the 'Summary' option is selected.
        /// </summary>
        internal event Action SummarySelected;

        /// <summary>
        /// Raised when the 'Cancel' option is selected.
        /// </summary>
        internal event Action CancelSelected;

        private void OnEnable()
        {
            view.SwitchRequested += OnSwitchRequested;
            view.SummaryRequested += OnSummaryRequested;
            view.CancelRequested += OnCancelRequested;
        }

        private void OnDisable()
        {
            view.SwitchRequested -= OnSwitchRequested;
            view.SummaryRequested -= OnSummaryRequested;
            view.CancelRequested -= OnCancelRequested;
        }

        private void OnSwitchRequested() => SwitchSelected?.Invoke();
        private void OnSummaryRequested() => SummarySelected?.Invoke();
        private void OnCancelRequested() => CancelSelected?.Invoke();
    }
}
