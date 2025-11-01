using PokemonGame.Characters.Inputs;
using PokemonGame.Inventory;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Party;
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

        private VerticalMenuController controller;

        /// <summary>
        /// Sets up button listeners. Called once before the view is shown.
        /// </summary>
        public override void Preload()
        {
            partyButton.OnClick += OnPartyClick;
            inventoryButton.OnClick += OnInventoryClick;
            exitButton.OnClick += OnExitClick;
        }

        private void Awake()
        {
            controller = GetComponent<VerticalMenuController>();
        }

        private void OnDestroy()
        {
            partyButton.OnClick -= OnPartyClick;
            inventoryButton.OnClick -= OnInventoryClick;
            exitButton.OnClick -= OnExitClick;
        }

        protected override void Update()
        {
            base.Update();

            // Allow closing the menu with the same key that opens it
            if (Input.GetKeyDown(KeyBinds.Menu))
            {
                ViewManager.Instance.CloseTopView();
            }
        }
        public override void Freeze()
        {
            controller.enabled = false;
            base.Freeze();
        }

        public override void Unfreeze()
        {
            controller.enabled = true;
            base.Unfreeze();
        }

        private void OnPartyClick()
        {
            PartyMenuView partyMenu = ViewManager.Instance.Show<PartyMenuView>();
        }

        private void OnInventoryClick()
        {
            ViewManager.Instance.Show<InventoryView>();
        }

        private void OnExitClick()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
