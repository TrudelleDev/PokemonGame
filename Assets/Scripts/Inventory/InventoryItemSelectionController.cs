using PokemonGame.Items.UI;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Handles navigation and selection of items within an inventory section.
    /// Uses a <see cref="VerticalMenuController"/> to move between items,
    /// and opens an <see cref="InventoryItemOptionsView"/> when an item is chosen.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryItemSelectionController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Controller for vertical navigation between inventory items.")]
        private VerticalMenuController menuController;

        private void OnEnable()
        {
            menuController.OnClick += HandleItemClicked;
        }

        private void OnDisable()
        {
            menuController.OnClick -= HandleItemClicked;
        }

        /// <summary>
        /// Handles when an item button is clicked in the menu.
        /// Binds the item to the detail panel and opens the options view.
        /// </summary>
        private void HandleItemClicked(MenuButton button)
        {
            if (button.TryGetComponent<ItemUI>(out ItemUI itemUI))
            {
                InventoryItemOptionsView optionsView = ViewManager.Instance.Show<InventoryItemOptionsView>();
                optionsView.SelectedItem = itemUI.Item;
            }
        }
    }
}
