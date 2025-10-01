using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Shows an item's description and icon in the inventory detail panel.
    /// </summary>
    public class InventoryItemDetailPanel : MonoBehaviour, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text field displaying the item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image displaying the item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Updates the panel with data from the given <see cref="IDisplayable"/>.
        /// Clears the panel if <paramref name="displayable"/> is null.
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
        /// Clears the panel, removing text and hiding the icon.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
