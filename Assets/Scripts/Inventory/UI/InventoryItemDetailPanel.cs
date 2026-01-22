using PokemonGame.Shared.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Inventory.UI
{
    /// <summary>
    /// Displays detailed information for a selected inventory item,
    /// including description text and icon image.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InventoryItemDetailPanel : MonoBehaviour
    {
        [SerializeField, Tooltip("Text field displaying the item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Tooltip("Image displaying the item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Binds an item to the detail panel and updates the UI.
        /// </summary>
        /// <param name="item">The item to display.</param>
        public void Bind(IDisplayable item)
        {
            if (item == null)
            {
                Unbind();
                return;
            }

            descriptionText.text = item.Description;
            iconImage.sprite = item.Icon;
            iconImage.enabled = true;
        }

        /// <summary>
        /// Clears the detail panel UI and hides the icon.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.enabled = false;
        }
    }
}
