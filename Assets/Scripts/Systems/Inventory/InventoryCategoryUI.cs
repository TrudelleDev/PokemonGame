using PokemonGame.Items.UI;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Displays a visual list of items from an InventoryCategory.
    /// Includes a cancel button to return to the previous view.
    /// </summary>
    public class InventoryCategoryUI : MonoBehaviour, IInventoryCategoryBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Prefab for displaying an individual item.")]
        private ItemUI itemUIPrefab;

        [SerializeField, Required]
        [Tooltip("Button used to cancel and return to the previous view.")]
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("Parent transform that holds all item UI elements.")]
        private Transform contentParent;

        private MenuButton activeCancelButton;

        /// <summary>
        /// Populates the UI with the given category's items and adds a cancel button.
        /// </summary>
        public void Bind(InventoryCategory category)
        {
            Unbind();

            if (category == null)
            {
                Debug.LogWarning("InventoryCategoryUI: Tried to bind a null category.");
                return;
            }

            foreach (var item in category.Items)
            {
                var itemUI = Instantiate(itemUIPrefab, contentParent);
                itemUI.Bind(item);
            }

            activeCancelButton = Instantiate(cancelButton, contentParent);
            activeCancelButton.transform.SetAsLastSibling();
            activeCancelButton.OnClick += HandleCancelClick;
        }

        /// <summary>
        /// Clears all UI elements and unsubscribes cancel button event.
        /// </summary>
        public void Unbind()
        {
            if (activeCancelButton != null)
            {
                activeCancelButton.OnClick -= HandleCancelClick;
                activeCancelButton = null;
            }

            for (int i = contentParent.childCount - 1; i >= 0; i--)
                Destroy(contentParent.GetChild(i).gameObject);
        }

        private void HandleCancelClick()
        {
            ViewManager.Instance.GoToPreviousView();
        }

        private void OnDestroy()
        {
            if (activeCancelButton != null)
            {
                activeCancelButton.OnClick -= HandleCancelClick;
                activeCancelButton = null;
            }
        }
    }
}
