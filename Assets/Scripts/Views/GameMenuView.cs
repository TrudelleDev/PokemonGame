using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

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
        private Button partyButton;

        [SerializeField, Required]
        [Tooltip("Button to open the inventory.")]
        private Button inventoryButton;

        [SerializeField, Required]
        [Tooltip("Button to close the menu.")]
        private Button exitButton;

        /// <summary>
        /// Sets up button listeners. Called once before the view is shown.
        /// </summary>
        public override void Initialize()
        {
            partyButton.onClick.AddListener(OnPartyClick);
            inventoryButton.onClick.AddListener(OnInventoryClick);
            exitButton.onClick.AddListener(OnExitClick);
        }

        /// <summary>
        /// Cleans up listeners to prevent leaks or dangling references.
        /// </summary>
        private void OnDestroy()
        {
            partyButton.onClick.RemoveListener(OnPartyClick);
            inventoryButton.onClick.RemoveListener(OnInventoryClick);
            exitButton.onClick.RemoveListener(OnExitClick);
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
