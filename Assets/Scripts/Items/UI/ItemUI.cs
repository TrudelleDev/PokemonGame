using System;
using PokemonGame.Menu;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    /// <summary>
    /// Displays the item's name and quantity.
    /// Automatically clears the UI when no valid item is assigned.
    /// </summary>
    public class ItemUI : MonoBehaviour, IMenuOptionDisplaySource
    {
        [SerializeField, Required]
        [Tooltip("Text element displaying the item's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text element displaying the item's quantity.")]
        private TextMeshProUGUI countText;

        private MenuButton button;

        public event Action<IDisplayable> OnSubmitted;
        public event Action<IDisplayable> OnHighlighted;

        public Item Item { get; private set; }

        public IDisplayable Displayable => Item.Definition;

        private void Awake()
        {
            button = GetComponent<MenuButton>();
        }
        private void OnEnable()
        {        
            button.OnSubmitted += HandleClick;
            button.OnHighlighted += HandleHighlighted;
        }

        private void OnDisable()
        {
            button.OnSubmitted -= HandleClick;
            button.OnHighlighted -= HandleHighlighted;
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
            countText.text = item.Quantity.ToString();
        }

        /// <summary>
        /// Unbinds the current item and clears the UI.
        /// </summary>
        public void Unbind()
        {
            Item = null;
            nameText.text = string.Empty;
            countText.text = string.Empty;
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
