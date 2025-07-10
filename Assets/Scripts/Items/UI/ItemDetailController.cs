using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Controls the item detail UI based on the current menu selection.
    /// Updates the item description and icon when the selection changes.
    /// </summary>
    public class ItemDetailController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected item's description and icon.")]
        private ItemDetailUI itemInfoUI;

        [SerializeField, Required]
        [Tooltip("Controls the vertical menu and dispatches selection events.")]
        private VerticalMenuController menuController;

        private void Awake()
        {
            menuController.OnSelect += OnMenuSelect;
        }

        private void OnEnable()
        {
            menuController.ResetToFirstElement();
        }

        private void OnDestroy()
        {
            menuController.OnSelect -= OnMenuSelect;
        }

        /// <summary>
        /// Handles selection changes from the menu.
        /// Binds the selected item or cancel data to the detail UI.
        /// </summary>
        /// <param name="menuButton">The selected menu button.</param>
        private void OnMenuSelect(MenuButton menuButton)
        {
            if (menuButton.TryGetComponent<ItemUI>(out var itemUI))
            {
                itemInfoUI.Bind(itemUI.Item);
            }
            else if (menuButton.TryGetComponent<CancelMenuButton>(out var cancelButton))
            {
                itemInfoUI.Bind(cancelButton.Data);
            }
        }
    }
}
