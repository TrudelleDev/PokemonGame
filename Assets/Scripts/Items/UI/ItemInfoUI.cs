using PokemonGame.Items.UI.Groups;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Controls the visual display of an item's description and icon.
    /// Falls back to default developer-defined content when item data is unavailable.
    /// </summary>
    public class ItemInfoUI : MonoBehaviour, IItemBind
    {
        [SerializeField, Required]
        [Tooltip("UI elements used to display the item's description and icon.")]
        private ItemInfoUIGroup itemUI;

        [SerializeField, Required]
        [Tooltip("Fallback UI content shown when item data is missing or invalid.")]
        private ItemInfoFallbackUIGroup fallbackUI;

        /// <summary>
        /// Binds an item to the UI, updating the description and icon.
        /// Shows fallback content if the item or its data is invalid.
        /// </summary>
        /// <param name="item">The item to bind.</param>
        public void Bind(Item item)
        {
            if (item?.Data == null)
            {
                ShowFallback();
                return;
            }

            itemUI.DescriptionText.text = item.Data.Description;
            itemUI.IconImage.sprite = item.Data.Icon;
        }

        /// <summary>
        /// Binds a cancel menu button to the UI, updating the description and icon.
        /// Shows fallback content if the button or its data is invalid.
        /// </summary>
        /// <param name="button">The cancel menu button to bind.</param>
        public void Bind(CancelMenuButton button)
        {
            if (button?.Data == null)
            {
                ShowFallback();
                return;
            }

            itemUI.DescriptionText.text = button.Data.Description;
            itemUI.IconImage.sprite = button.Data.Icon;
        }

        /// <summary>
        /// Displays fallback description and icon.
        /// Used when bound data is missing or invalid.
        /// </summary>
        private void ShowFallback()
        {
            itemUI.DescriptionText.text = fallbackUI.Description;
            itemUI.IconImage.sprite = fallbackUI.Icon;
        }
    }
}
