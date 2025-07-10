using PokemonGame.Items.Datas;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Displays an item's description and icon.
    /// Automatically falls back to a default state when no data is available.
    /// </summary>
    public class ItemDetailUI : MonoBehaviour, IItemBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Displays the selected item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Binds the given item to the UI.
        /// </summary>
        /// <param name="item">The item to display.</param>
        public void Bind(Item item)
        {
            if (item?.Data == null)
            {
                Unbind();
                return;
            }

            Bind(item.Data);
        }

        /// <summary>
        /// Binds item data directly to the UI.
        /// </summary>
        /// <param name="data">The item data to display.</param>
        public void Bind(ItemData data)
        {
            if (data == null)
            {
                Unbind();
                return;
            }

            descriptionText.text = data.Description;
            iconImage.sprite = data.Icon;
            iconImage.enabled = true;
        }

        /// <summary>
        /// Clears the UI and hides the icon.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
