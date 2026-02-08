using System;
using MonsterTamer.Shared.Interfaces;
using MonsterTamer.Shared.UI.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Items.UI
{
    /// <summary>
    /// Displays the item's name and quantity.
    /// Automatically clears the UI when no valid item is assigned.
    /// </summary>
    internal class ItemUI : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Text element displaying the item's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required, Tooltip("Text element displaying the item's quantity.")]
        private TextMeshProUGUI quantityText;

        private MenuButton button;

        /// <summary>
        /// Raised when the item is submitted (confirmed) by the player.
        /// </summary>
        public event Action<IDisplayable> OnSubmitted;

        /// <summary>
        /// Raised when the item is highlighted by menu navigation.
        /// </summary>
        public event Action<IDisplayable> OnHighlighted;

        /// <summary>
        /// The runtime item currently bound to this UI element.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Displayable data source used by menu systems.
        /// </summary>
        public IDisplayable Displayable => Item.Definition;

        private void Awake()
        {
            button = GetComponent<MenuButton>();
        }
        private void OnEnable()
        {
            button.Confirmed += HandleClick;
            button.Selected += HandleHighlighted;
        }

        private void OnDisable()
        {
            button.Confirmed -= HandleClick;
            button.Selected -= HandleHighlighted;
        }

        /// <summary>
        ///  Binds the UI to the given item, or clears if invalid.
        /// </summary>
        /// <param name="item">The item to display.</param>
        public void Bind(Item item)
        {
            if (item == null || item.Definition == null)
            {
                Unbind();
                return;
            }

            Item = item;
            nameText.text = item.Definition.DisplayName;
            quantityText.text = item.Quantity.ToString();
        }

        /// <summary>
        /// Unbinds the current item and clears the UI.
        /// </summary>
        public void Unbind()
        {
            Item = null;
            nameText.text = string.Empty;
            quantityText.text = string.Empty;
        }

        private void HandleClick()
        {
            OnSubmitted?.Invoke(Item.Definition);
        }

        private void HandleHighlighted()
        {
            OnHighlighted?.Invoke(Item.Definition);
        }
    }
}
