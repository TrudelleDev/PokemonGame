using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Controller that keeps the item detail panel in sync with the currently selected inventory menu option.
    /// </summary>
    public class InventoryItemDetailController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("UI element that displays the description and icon for the currently selected item.")]
        private InventoryItemDetailPanel menuOptionDetailPanel;

        [SerializeField, Required]
        [Tooltip("Menu controller that manages item navigation and selection events.")]
        private VerticalMenuController menuController;

        /// <summary>
        /// Subscribes to menu selection events when enabled.
        /// </summary>
        private void OnEnable()
        {
            menuController.OnSelect += OnMenuSelect;

            if (menuController.CurrentButton != null)
            {
                OnMenuSelect(menuController.CurrentButton);
            }
            else
            {
                menuOptionDetailPanel.Unbind();
            }
        }

        /// <summary>
        /// Unsubscribes from menu selection events when disabled.
        /// </summary>
        private void OnDisable()
        {
            menuController.OnSelect -= OnMenuSelect;
        }

        /// <summary>
        /// Updates the detail panel to reflect the selected menu button.
        /// </summary>
        /// <param name="menuButton">The currently selected menu button.</param>
        private void OnMenuSelect(MenuButton menuButton)
        {
            if (menuButton.TryGetComponent<IMenuOptionDisplaySource>(out var source) && source.Displayable != null)
            {
                menuOptionDetailPanel.Bind(source.Displayable);
            }
            else
            {
                menuOptionDetailPanel.Unbind();
            }
        }
    }
}
