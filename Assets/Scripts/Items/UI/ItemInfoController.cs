using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Manages the item info UI based on menu selection.
    /// Listens for selection events and updates the item description and icon display.
    /// </summary>
    public class ItemInfoController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("UI component that displays the selected item's description and icon.")]
        private ItemInfoUI itemInfoUI;

        [SerializeField, Required]
        [Tooltip("Menu controller that dispatches selection events.")]
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
        /// Called when a menu button is selected.
        /// Binds the corresponding item or cancel action to the UI.
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
                itemInfoUI.Bind(cancelButton);
            }
        }
    }
}
