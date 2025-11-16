using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Keeps the inventory item detail panel synchronized with the currently
    /// selected menu option. Updates the description and icon whenever
    /// the selection changes.
    /// </summary>
    public class InventoryItemDetailController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("UI element that displays the description and icon for the currently selected item.")]
        private InventoryItemDetailPanel menuOptionDetailPanel;

        [SerializeField, Required]
        [Tooltip("Menu controller that manages item navigation and selection events.")]
        private VerticalMenuController menuController;

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

        private void OnDisable()
        {
            menuController.OnSelect -= OnMenuSelect;
        }

        /// <summary>
        /// Updates the detail panel to display the item linked to the selected menu button.
        /// Clears the panel if no valid display source is found.
        /// </summary>
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
