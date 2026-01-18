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
    public sealed class PartyMenuOptionsController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the Party Menu Options view that raises option events.")]
        private PartyMenuOptionsView view;

        public event Action SwapSelected;
        public event Action InfoSelected;
        public event Action ReturnSelected;

        private void OnEnable()
        {
            view.SwapRequested += OnSwapRequested;
            view.InfoRequested += OnInfoRequested;
            view.CancelRequested += OnReturnRequested;
        }

        private void OnDisable()
        {
            view.SwapRequested -= OnSwapRequested;
            view.InfoRequested -= OnInfoRequested;
            view.CancelRequested -= OnReturnRequested;
        }

        private void OnSwapRequested() => SwapSelected?.Invoke();
        private void OnInfoRequested() => InfoSelected?.Invoke();
        private void OnReturnRequested() => ReturnSelected?.Invoke();
    }
}
