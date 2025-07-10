using PokemonGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Displays the item's name and quantity.
    /// Automatically clears the UI when no valid item is assigned.
    /// </summary>
    public class ItemUI : MonoBehaviour, IItemBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text element displaying the item's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text element displaying the item's quantity.")]
        private TextMeshProUGUI countText;

        /// <summary>
        /// The item currently bound to this UI.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Binds the specified item to the UI.
        /// </summary>
        /// <param name="item">The item to display.</param>
        public void Bind(Item item)
        {
            if (item?.Data == null)
            {
                Unbind();
                return;
            }

            Item = item;
            nameText.text = item.Data.Name;
            countText.text = item.Count.ToString();
        }

        /// <summary>
        /// Clears the UI and resets the bound item.
        /// </summary>
        public void Unbind()
        {
            Item = null;
            nameText.text = string.Empty;
            countText.text = string.Empty;
        }
    }
}
