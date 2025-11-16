using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Displays an item's icon and description in the inventory detail panel.
    /// Can be bound to any object implementing <see cref="IDisplayable"/>.
    /// </summary>
    public class InventoryItemDetailPanel : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field displaying the item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image displaying the item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Updates the panel with data from the given <see cref="IDisplayable"/>.
        /// </summary>
        /// <param name="displayable">The object providing description and icon.</param>
        public void Bind(IDisplayable displayable)
        {
            if (displayable == null)
            {
                Unbind();
                return;
            }

            descriptionText.text = displayable.Description;
            iconImage.sprite = displayable.Icon;
            iconImage.enabled = iconImage.sprite != null;
        }

        /// <summary>
        /// Clears the panel's text and hides the icon image.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
