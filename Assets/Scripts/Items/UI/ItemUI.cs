using PokemonGame.Items.UI.Groups;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Controls the visual representation of an item's name and count.
    /// Falls back to default developer-defined content when item data is unavailable.
    /// </summary>
    public class ItemUI : MonoBehaviour, IItemBind
    {
        [SerializeField, Required]
        [Tooltip("UI elements used to display the item's name and count.")]
        private ItemUIGroup itemUI;

        [SerializeField, Required]
        [Tooltip("Fallback UI text displayed when no item is bound or item data is invalid.")]
        private ItemFallbackUIGroup fallbackUI;

        /// <summary>
        /// The item currently bound to this UI, or null if unbound.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Binds an item to the UI, updating the name and count display.
        /// If the item or its data is invalid, fallback content is shown instead.
        /// </summary>
        /// <param name="item">The item to bind to the UI.</param>
        public void Bind(Item item)
        {
            if (item?.Data == null)
            {
                ShowFallback();
                return;
            }

            Item = item;
            itemUI.NameText.text = item.Data.name;
            itemUI.CountText.text = item.Count.ToString();
        }

        /// <summary>
        /// Displays fallback text for name and count.
        /// Used when the bound item is null or contains no valid data.
        /// </summary>
        private void ShowFallback()
        {
            Item = null;
            itemUI.NameText.text = fallbackUI.NameText;
            itemUI.CountText.text = fallbackUI.CountText;
        }
    }
}
