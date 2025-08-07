using PokemonGame.Items.Datas;
using PokemonGame.Items.UI.Interfaces;
using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Displays the description and icon of a selected item.
    /// If no item is selected or data is missing, resets the UI to an empty/default state.
    /// </summary>
    public class ItemDetailUI : MonoBehaviour, IItemBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text field used to display the item's description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image used to display the item's icon.")]
        private Image iconImage;

        /// <summary>
        /// Binds the given item to the UI.
        /// Displays its data if valid, or resets the UI if null.
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
        /// Binds the raw item data directly to the UI.
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
        /// Clears the UI elements and optionally sets a default icon.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
