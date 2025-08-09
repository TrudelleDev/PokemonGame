using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu.UI
{
    /// <summary>
    /// Syncs the detail panel with the currently selected menu option.
    /// Subscribes to selection changes and updates the description/icon accordingly.
    /// </summary>
    public class MenuOptionDetailUIController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected item's description and icon.")]
        private MenuOptionDetailUI menuOptionDetailUI;

        [SerializeField, Required]
        [Tooltip("Controls the vertical menu and dispatches selection events.")]
        private VerticalMenuController menuController;

        private void Awake()
        {
            menuController.OnSelect += OnMenuSelect;

            // Sync immediately so initial selection is displayed without execution order hacks
            if (menuController.CurrentButton != null)
            {
                OnMenuSelect(menuController.CurrentButton);
            }
            else
            {
                menuOptionDetailUI.Unbind();
            }
        }

        private void OnDestroy()
        {
            menuController.OnSelect -= OnMenuSelect;
        }

        private void OnMenuSelect(Button menuButton)
        {
            if (menuButton.TryGetComponent<IMenuOptionDisplaySource>(out var source) && source.Displayable != null)
            {
                menuOptionDetailUI.Bind(source.Displayable);
            }
            else
            {
                menuOptionDetailUI.Unbind();
            }
        }
    }
}
