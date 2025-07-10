using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Main menu where the player can open the party, inventory, or go back.
    /// </summary>
    public class GameMenuView : View
    {
        [SerializeField, Required]
        [Tooltip("Button to open the party menu.")]
        private MenuButton party;

        [SerializeField, Required]
        [Tooltip("Button to open the inventory.")]
        private MenuButton inventory;

        [SerializeField, Required]
        [Tooltip("Button to close the menu and return to the previous view.")]
        private MenuButton exit;

        /// <summary>
        /// Initializes event listeners for menu buttons.
        /// Called once before the view is first shown.
        /// </summary>
        public override void Initialize()
        {
            party.OnClick += OnPartyClick;
            inventory.OnClick += OnInventoryClick;
            exit.OnClick += OnExitClick;
        }

        private void OnDestroy()
        {
            party.OnClick -= OnPartyClick;
            inventory.OnClick -= OnInventoryClick;
            exit.OnClick -= OnExitClick;
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
