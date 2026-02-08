using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Shared.UI.Core
{
    /// <summary>
    /// Base class for menu controllers.
    /// Automatically detects child MenuButtons and manages
    /// selection state, audio feedback, and button activation.
    /// Derived classes define navigation rules and visual behavior.
    /// </summary>
    internal abstract class MenuController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("UI audio settings used for menu navigation and confirmation sounds.")]
        protected UIAudioSettings audioSettings;

        /// <summary>
        /// List of all detected MenuButtons under this GameObject.
        /// </summary>
        protected readonly List<MenuButton> buttons = new();

        /// <summary>
        /// Index of the currently selected button.
        /// </summary>
        protected int currentIndex;

        /// <summary>
        /// Index of the previously selected button.
        /// </summary>
        protected int previousIndex;

        /// <summary>
        /// Raised when a new MenuButton becomes selected.
        /// </summary>
        internal event Action<MenuButton> Selected;

        /// <summary>
        /// Raised when the currently selected MenuButton is confirmed.
        /// </summary>
        internal event Action<MenuButton> Confirmed;

        /// <summary>
        /// The currently selected MenuButton.
        /// </summary>
        internal MenuButton CurrentButton { get; private set; }

        /// <summary>
        /// Total number of detected buttons.
        /// </summary>
        protected int ItemCount => buttons.Count;

        private void Awake()
        {
            RebuildButtons();
            StartCoroutine(InitializeSelection());
        }

        /// <summary>
        /// Resets the controller to its initial state.
        /// Rebuilds buttons and reapplies the initial selection.
        /// </summary>
        internal void ResetController()
        {
            RebuildButtons();
            ApplySelectionState(0);
        }

        /// <summary>
        /// Applies visual selection changes between buttons.
        /// Implemented by derived classes.
        /// </summary>
        /// <param name="previousIndex">Previously selected button index.</param>
        /// <param name="currentIndex">Currently selected button index.</param>
        protected abstract void ApplySelection(int previousIndex, int currentIndex);


        /// <summary>
        /// Updates internal selection state, applies visual changes,
        /// and plays the UI selection sound.
        /// </summary>
        /// <param name="index">Index of the button to select.</param>
        protected void HandleSelection(int index)
        {
            ApplySelectionState(index);
            PlaySelectSfx();
        }

        /// <summary>
        /// Plays the UI confirmation sound and raises the Confirmed event
        /// to notify subscribers of the action.
        /// </summary>
        protected void HandleConfirmation()
        {
            if (CurrentButton == null)
            {
                return;
            }

            CurrentButton.HandleConfirmation();
            PlayConfirmSfx();
            Confirmed?.Invoke(CurrentButton);
        }

        private void ApplySelectionState(int index)
        {
            if (index < 0 || index >= ItemCount)
            {
                return;
            }

            previousIndex = currentIndex;
            currentIndex = index;
            CurrentButton = buttons[currentIndex];

            ApplySelection(previousIndex, currentIndex);
            Selected?.Invoke(CurrentButton);
        }

        private IEnumerator InitializeSelection()
        {
            yield return null;
            ApplySelectionState(0);
        }

        private void RebuildButtons()
        {
            buttons.Clear();
            buttons.AddRange(GetComponentsInChildren<MenuButton>(true));
        }

        private void PlaySelectSfx()
        {
            if (audioSettings != null)
            {
                AudioManager.Instance.PlayUISFX(audioSettings.SelectSfx);
            }
        }

        private void PlayConfirmSfx()
        {
            if (audioSettings != null)
            {
                AudioManager.Instance.PlayUISFX(audioSettings.ConfirmSfx);
            }
        }
    }
}
