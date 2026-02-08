using System;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Shared.UI.Core
{
    /// <summary>
    /// Base class for all menu buttons.
    /// Provides selection, interaction, click events, and visual refresh handling.
    /// Subclasses implement <see cref="RefreshVisual"/> to define how the visuals update.
    /// </summary>
    internal abstract class MenuButton : MonoBehaviour
    {
        [SerializeField, Tooltip("Whether this button starts as interactable.")]
        private bool interactable = true;

        [SerializeField, Tooltip("Optional Text label displayed on the button.")]
        private TextMeshProUGUI label;

        private bool lockSelectSprite;

        /// <summary>
        /// Raised when the button is clicked while interactable.
        /// </summary>
        internal event Action Confirmed;

        /// <summary>
        /// Raised when the button becomes selected.
        /// </summary>
        internal event Action Selected;

        /// <summary>
        /// Whether the button is currently interactable.
        /// </summary>
        internal bool IsInteractable => interactable;

        /// <summary>
        /// Whether the selected sprite is locked. Changing this automatically refreshes visuals.
        /// </summary>
        internal bool LockSelectSprite
        {
            get => lockSelectSprite;
            set => SetLockSelectSprite(value);
        }

        /// <summary>
        /// Whether the button is currently selected.
        /// </summary>
        internal bool IsSelected { get; private set; }

        /// <summary>
        /// Sets the visible text label of the button.
        /// </summary>
        /// <param name="text">The text to display.</param>
        internal void SetLabel(string text)
        {
            label.text = text;
        }

        /// <summary>
        /// Sets the selected state of the button and refreshes visuals.
        /// Raises the <see cref="Selected"/> event when selected.
        /// </summary>
        /// <param name="active">Whether the button is selected.</param>
        internal void SetSelected(bool active)
        {
            if (IsSelected == active)
            {
                return;
            }

            IsSelected = active;
            RefreshVisual();

            if (active)
            {
                Selected?.Invoke();
            }
        }

        /// <summary>
        /// Sets whether the button is interactable and refreshes visuals.
        /// </summary>
        /// <param name="value">New interactable state.</param>
        internal void SetInteractable(bool value)
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
        /// <param name="value">New lock state.</param>
        internal void SetLockSelectSprite(bool value)
        {
            if (lockSelectSprite == value)
            {
                return;
            }

            lockSelectSprite = value;
            RefreshVisual();
        }

        /// <summary>
        /// Invokes the click/confirm event if the button is interactable.
        /// </summary>
        internal void HandleConfirmation()
        {
            if (IsInteractable)
            {
                Confirmed?.Invoke();
            }
        }

        /// <summary>
        /// Updates visuals (sprites, colors, backgrounds, etc.).
        /// Called whenever the button state changes.
        /// </summary>
        protected abstract void RefreshVisual();
    }
}
