using PokemonGame.Items;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Assets.Scripts.Inventory
{
    /// <summary>
    /// Panel that displays information about the currently selected item
    /// in the inventory. Updates with the item's description and icon when
    /// bound, and clears the panel when unbound.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryItemSelectionPanel : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field used to display the selected item's description or name.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image used to display the selected item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Binds this panel to the given <see cref="Item"/>, updating
        /// the description text and icon accordingly.
        /// </summary>
        public void Bind(Item item)
        {
            if (item == null)
            {
                Unbind();
                return;
            }

            descriptionText.text = $"{item.Definition.DisplayName} is\nselected.";
            iconImage.sprite = item.Definition.Icon;
            iconImage.enabled = iconImage.sprite != null;
        }

        /// <summary>
        /// Clears the panel, removing any text and hiding the icon.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
