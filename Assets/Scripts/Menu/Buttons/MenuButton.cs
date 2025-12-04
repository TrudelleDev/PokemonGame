using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Menu
{
    /// <summary>
    /// Base class for all menu buttons.
    /// Provides selection, interaction, click events, and visual refresh handling.
    /// Subclasses implement <see cref="RefreshVisual"/> to define how visuals update.
    /// </summary>
    public abstract class MenuButton : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Whether this button starts interactable.")]
        private bool interactable = true;

        [SerializeField, Required]
        private TextMeshProUGUI label;

        private bool lockSelectSprite;

        /// <summary>
        /// Invoked when the button is clicked while interactable.
        /// </summary>
        public event Action OnClick;

        /// <summary>
        /// Invoked when the button is selected.
        /// </summary>
        public event Action OnSelect;

        /// <summary>
        /// Gets whether the button is currently interactable.
        /// </summary>
        public bool IsInteractable => interactable;

        /// <summary>
        /// Gets or sets whether the selected sprite should be locked.
        /// When changed, visuals will automatically refresh.
        /// </summary>
        public bool LockSelectSprite
        {
            get => lockSelectSprite;
            set => SetLockSelectSprite(value);
        }

        /// <summary>
        /// Gets whether the button is currently selected.
        /// </summary>
        public bool IsSelected { get; private set; }


        /// <summary>
        /// Sets the visible text label of the button.
        /// </summary>
        public void SetLabel(string text)
        {
            label.text = text;
        }

        /// <summary>
        /// Sets the selected state of the button and refreshes visuals.
        /// </summary>
        public void SetSelected(bool active)
        {
            if (IsSelected == active)
            {
                return;
            }

            IsSelected = active;
            RefreshVisual();

            if (active)
            {
                OnSelect?.Invoke();
            }
        }

        /// <summary>
        /// Sets whether the button is interactable and refreshes visuals.
        /// </summary>
        public void SetInteractable(bool value)
        {
            if (interactable == value)
            {
                return;
            }

            interactable = value;
            RefreshVisual();
        }

        /// <summary>
        /// Sets whether the select sprite is locked and refreshes visuals.
        /// </summary>
        public void SetLockSelectSprite(bool value)
        {
            if (lockSelectSprite == value)
            {
                return;
            }

            lockSelectSprite = value;
            RefreshVisual();
        }

        /// <summary>
        /// Invokes the click event if the button is interactable.
        /// </summary>
        public void Click()
        {
            if (IsInteractable)
            {
                OnClick?.Invoke();
            }
        }

        /// <summary>
        /// Subclasses update their visuals (sprites, colors, backgrounds, etc.) here.
        /// Called whenever state changes.
        /// </summary>
        protected abstract void RefreshVisual();
    }
}
