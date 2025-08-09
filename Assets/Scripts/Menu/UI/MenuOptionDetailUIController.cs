using PokemonGame.Menu.Controllers;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu.UI
{
    /// <summary>
    /// Controls the item detail UI based on the current menu selection.
    /// Updates the item description and icon when the selection changes.
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
        }

        private void OnEnable()
        {
          // menuController.RefreshButtons();
        }

        private void OnDestroy()
        {
            menuController.OnSelect -= OnMenuSelect;
        }

        private void OnMenuSelect(Button menuButton)
        {
            if (menuButton.TryGetComponent<IMenuOptionDisplaySource>(out var source))
            {
                if (source.Displayable != null)
                {
                    menuOptionDetailUI.Bind(source.Displayable);
                    return;
                }
            }

            menuOptionDetailUI.Unbind();
        }
    }
}
