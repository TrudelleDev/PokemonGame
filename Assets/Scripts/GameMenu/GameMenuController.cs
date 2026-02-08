using MonsterTamer.GameMenu;
using MonsterTamer.Inventory.UI;
using MonsterTamer.Party.Enums;
using MonsterTamer.Party.UI;
using UnityEngine;

namespace MonsterTamer.Views
{
    /// <summary>
    /// Controls the main game menu flow.
    /// Listens to <see cref="GameMenuView"/> intent events and
    /// opens or closes related menus (Party, Inventory),
    /// or closes the game menu itself.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GameMenuView))]
    internal sealed class GameMenuController : MonoBehaviour
    {
        private GameMenuView menuView;

        private void Awake()
        {
            menuView = GetComponent<GameMenuView>();
        }

        private void OnEnable()
        {
            menuView.PartyOpenRequested += OnPartyOpenRequested;
            menuView.InventoryOpenRequested += HandleInventoryOpenRequested;
            menuView.CloseRequested += HandleCloseRequested;
        }

        private void OnDisable()
        {
            menuView.PartyOpenRequested -= OnPartyOpenRequested;
            menuView.InventoryOpenRequested -= HandleInventoryOpenRequested;
            menuView.CloseRequested -= HandleCloseRequested;
        }

        private void OnPartyOpenRequested()
        {
            var partyMenuView = ViewManager.Instance.Show<PartyMenuView>();
            var partyMenuPresenter = partyMenuView.GetComponent<PartyMenuPresenter>();

            partyMenuPresenter.Setup(PartySelectionMode.Overworld);
        }

        private void HandleInventoryOpenRequested()
        {
            ViewManager.Instance.Show<InventoryView>();
        }

        private void HandleCloseRequested()
        {
            ViewManager.Instance.Close<GameMenuView>();
        }
    }
}
