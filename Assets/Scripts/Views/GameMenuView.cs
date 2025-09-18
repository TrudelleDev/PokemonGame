using PokemonGame.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Main menu view that provides access to core in-game systems.
    /// </summary>
    public class GameMenuView : View
    {
        [Title("Menu Buttons")]

        [SerializeField, Required]
        [Tooltip("Button to open the party menu.")]
        private MenuButton partyButton;

        [SerializeField, Required]
        [Tooltip("Button to open the inventory.")]
        private MenuButton inventoryButton;

        [SerializeField, Required]
        [Tooltip("Button to close the menu.")]
        private MenuButton exitButton;

        /// <summary>
        /// Sets up button listeners. Called once before the view is shown.
        /// </summary>
        public override void Preload()
        {
            partyButton.OnClick += OnPartyClick;
            inventoryButton.OnClick += OnInventoryClick;
            exitButton.OnClick += OnExitClick;
        }

        /// <summary>
        /// Cleans up listeners to prevent leaks or dangling references.
        /// </summary>
        private void OnDestroy()
        {
            partyButton.OnClick -= OnPartyClick;
            inventoryButton.OnClick -= OnInventoryClick;
            exitButton.OnClick -= OnExitClick;
        }

        private void OnPartyClick()
        {
            ViewManager.Instance.Show<PartyMenuView>();
        }

        private void OnInventoryClick()
        {
            ViewManager.Instance.Show<InventoryView>();
        }

        private void OnExitClick()
        {
            ViewManager.Instance.GoToPreviousView();
        }
    }
}
