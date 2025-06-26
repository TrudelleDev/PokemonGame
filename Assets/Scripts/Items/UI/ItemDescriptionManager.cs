using PokemonGame.MenuControllers;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    public class ItemDescriptionManager : MonoBehaviour
    {
        [SerializeField] private ItemDescription itemDescription;

       // private ScrollMenuController controller;

        private void Awake()
        {
           // controller = GetComponent<ScrollMenuController>();
          //  controller.Select += OnControllerSelect;
        }

        private void OnControllerSelect(MenuButton menuButton)
        {
            // Show item data.
            if (menuButton.GetComponent<ItemUI>() != null)
            {
                itemDescription.Bind(menuButton.GetComponent<ItemUI>().ItemReference.Data);
            }

            // Show cancel button data.
            if (menuButton.GetComponent<CancelMenuButton>() != null)
            {
                itemDescription.Bind(menuButton.GetComponent<CancelMenuButton>().Data);
            }
        }
    }
}
