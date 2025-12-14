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

        public override void Preload() { }

        private void Awake()
        {
            controller = GetComponent<VerticalMenuController>();
        }

        private void OnEnable()
        {
            partyButton.OnClick += OnPartyClick;
            inventoryButton.OnClick += OnInventoryClick;
            exitButton.OnClick += OnExitClick;
            OnCloseKeyPress += GameMenuView_OnCloseKeyPress;
        }

        private void GameMenuView_OnCloseKeyPress()
        {
            ViewManager.Instance.Close<GameMenuView>();
        }

        private void OnDisable()
        {
            partyButton.OnClick -= OnPartyClick;
            inventoryButton.OnClick -= OnInventoryClick;
            exitButton.OnClick -= OnExitClick;
            OnCloseKeyPress -= GameMenuView_OnCloseKeyPress;
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
            var party = ViewManager.Instance.Show<PartyMenuView>();

            party.OnCloseKeyPress -= OnPartyClose;
            party.OnCloseButtonPress -= OnPartyClose;

            party.OnCloseKeyPress += OnPartyClose;
            party.OnCloseButtonPress += OnPartyClose;
        }

        /// <summary>
        /// Handles party menu close event.
        /// Unsubscribes immediately to prevent multiple calls.
        /// </summary>
        private void OnPartyClose()
        {
            ViewManager.Instance.Close<PartyMenuView>();
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
