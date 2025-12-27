using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays the four main player options (Fight, Bag, Pokémon, Run) during a battle.
    /// This view captures user input and raises specific events for the controlling state machine.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PlayerActionPanel : View
    {
        [SerializeField, Required, Tooltip("Button to initiate the move selection screen.")]
        private MenuButton fightButton;

        [SerializeField, Required, Tooltip("Button to open the inventory/item selection screen.")]
        private MenuButton bagButton;

        [SerializeField, Required, Tooltip("Button to open the party/switch Pokémon screen.")]
        private MenuButton partyButton;

        [SerializeField, Required, Tooltip("Button to attempt escaping the battle.")]
        private MenuButton runButton;

        /// <summary>
        /// Raised when the player selects the 'Fight' option.
        /// </summary>
        public event Action OnFightSelected;

        /// <summary>
        /// Raised when the player selects the 'Bag' option.
        /// </summary>
        public event Action OnBagSelected;

        /// <summary>
        /// Raised when the player selects the 'Party' option.
        /// </summary>
        public event Action OnPartySelected;

        /// <summary>
        /// Raised when the player selects the 'Run' option.
        /// </summary>
        public event Action OnRunSelected;

        private void OnEnable()
        {
            // Subscribing to component events is crucial for enabling interaction when the view is active.
            fightButton.OnSubmitted += HandleFightClicked;
            bagButton.OnSubmitted += HandleBagClicked;
            partyButton.OnSubmitted += HandlePartyClicked;
            runButton.OnSubmitted += HandleRunClicked;
        }

        private void OnDisable()
        {
            // Unsubscribing is essential for cleanup and preventing memory leaks/null reference exceptions
            // when the view is disabled or destroyed.
            fightButton.OnSubmitted -= HandleFightClicked;
            bagButton.OnSubmitted -= HandleBagClicked;
            partyButton.OnSubmitted -= HandlePartyClicked;
            runButton.OnSubmitted -= HandleRunClicked;
        }

        // --- Internal Event Handlers ---

        // Handlers translate the low-level MenuButton click into a high-level public event.
        private void HandleFightClicked() => OnFightSelected?.Invoke();
        private void HandleBagClicked() => OnBagSelected?.Invoke();
        private void HandlePartyClicked() => OnPartySelected?.Invoke();
        private void HandleRunClicked() => OnRunSelected?.Invoke();

        /// <summary>
        /// Freezes the UI by disabling the input controller component.
        /// </summary>
        public override void Freeze()
        {
            // Accessing the controller component is necessary to disable user input without hiding the panel.
            GetComponent<GridMenuController>().enabled = false;
        }

        /// <summary>
        /// Unfreezes the UI by enabling the input controller component.
        /// </summary>
        public override void Unfreeze()
        {
            GetComponent<GridMenuController>().enabled = true;
        }
    }
}