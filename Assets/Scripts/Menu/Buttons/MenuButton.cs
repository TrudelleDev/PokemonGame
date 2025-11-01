using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Menu
{
    /// <summary>
    /// Base class for all menu buttons.
    /// Manages interactable and selection state.
    /// Subclasses implement <see cref="RefreshVisual"/> to handle visuals.
    /// </summary>
    public abstract class MenuButton : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Whether this button starts interactable.")]
        private bool interactable = true;

        [SerializeField, Required]
        private TextMeshProUGUI label;


        public event Action OnClick;
        public event Action OnSelect;

        /// <summary>
        /// Gets whether the button is currently interactable.
        /// </summary>
        public bool IsInteractable => interactable;

        /// <summary>
        /// Gets whether the button is currently selected.
        /// </summary>
        public bool IsSelected { get; private set; }


        public void SetLabel(string text)
        {
            label.text = text;
        }

        /// <summary>
        /// Sets the selected state of the button and refreshes visuals.
        /// </summary>
        /// <param name="active">True if selected, false if deselected.</param>
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
        /// <param name="value">True if interactable, false otherwise.</param>
        public void SetInteractable(bool value)
        {
            if (IsInteractable == value)
            {
                return;
            }

            interactable = value;
            RefreshVisual();
        }

        /// <summary>
        /// Invokes onClick event if the button is interactable.
        /// </summary>
        public void Click()
        {
            if (IsInteractable)
            {
                OnClick?.Invoke();
            }
        }

        /// <summary>
        /// Updates the visual state of the button to match
        /// </summary>
        protected abstract void RefreshVisual();
    }
}
