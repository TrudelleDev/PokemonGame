using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Vertical menu controller for keyboard/controller input.
    /// Scans configured parents for buttons, selects the first interactable on enable,
    /// handles Up/Down navigation with wrap-around, plays a selection sound on change,
    /// and exposes events for select, click, and cancel actions.
    /// </summary>
    public class VerticalMenuController : MonoBehaviour
    {
        [Title("Button Sources")]
        [Tooltip("Parents to scan for MenuButton components.")]
        [ValidateInput(nameof(HasSources), "Assign at least one button source.")]
        [ChildGameObjectsOnly]
        [SerializeField, Required]
        private List<Transform> buttonSources = new();

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound played when the selection changes.")]
        private AudioClip selectSound;

        private readonly List<MenuButton> buttons = new();
        private MenuButton currentButton;

        /// <summary>
        /// Raised when a new button becomes selected.
        /// </summary>
        public event Action<MenuButton> OnSelect;

        /// <summary>
        /// Raised when the currently selected button is activated.
        /// </summary>
        public event Action<MenuButton> OnClick;

        /// <summary>
        /// Raised when cancel input is pressed while a button is selected.
        /// </summary>
        public event Action<MenuButton> OnCancel;

        /// <summary>
        /// The button that is currently selected, or null if none.
        /// </summary>
        public MenuButton CurrentButton => currentButton;

        /// <summary>
        /// Enables or disables input handling for this controller.
        /// </summary>
        public bool AcceptInput { get; set; } = true;

        /// <summary>
        /// Rebuilds the button list and schedules selection of the first interactable button.
        /// </summary>
        private void OnEnable()
        {
            RefreshButtons();
            StartCoroutine(DeferredSelectFirst());
        }

        /// <summary>
        /// Reads input and dispatches navigation (Up/Down), activation (Interact), and cancel actions.
        /// </summary>
        private void Update()
        {
            if (!AcceptInput || buttons.Count == 0 || currentButton == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Down))
            {
                MoveNext();
            }
            else if (Input.GetKeyDown(KeyBinds.Up))
            {
                MovePrevious();
            }
            else if (Input.GetKeyDown(KeyBinds.Interact))
            {
                TriggerClick();
            }
            else if (Input.GetKeyDown(KeyBinds.Cancel))
            {
                OnCancel?.Invoke(currentButton);
            }
        }

        /// <summary>
        /// Collects all buttons from the configured sources and clears the current selection.
        /// </summary>
        private void RefreshButtons()
        {
            if (currentButton != null)
            {
                currentButton.SetSelected(false);
                currentButton = null;
            }

            buttons.Clear();

            foreach (Transform source in buttonSources)
            {
                if (source == null)
                {
                    continue;
                }

                buttons.AddRange(source.GetComponentsInChildren<MenuButton>(true));
            }
        }

        /// <summary>
        /// Waits one frame (to allow layout/activation) and then selects the first interactable button.
        /// </summary>
        private IEnumerator DeferredSelectFirst()
        {
            yield return null; // wait one frame
            SelectFirst();
        }

        /// <summary>
        /// Selects the first interactable button in the list, if any; otherwise clears selection.
        /// </summary>
        public void SelectFirst()
        {
            foreach (MenuButton button in buttons)
            {
                if (button != null && button.IsInteractable)
                {
                    SelectButton(button, false);
                    return;
                }
            }

            currentButton = null;
        }

        /// <summary>
        /// Validation helper to ensure at least one button source is assigned.
        /// </summary>
        private bool HasSources()
        {
            return buttonSources != null && buttonSources.Count > 0;
        }

        /// <summary>
        /// Applies selection to the specified button, optionally plays the selection sound,
        /// and raises the selection event.
        /// </summary>
        /// <param name="button">Button to select.</param>
        /// <param name="playSound">Whether to play the selection sound.</param>
        private void SelectButton(MenuButton button, bool playSound = true)
        {
            if (currentButton != null)
            {
                currentButton.SetSelected(false);
            }

            currentButton = button;
            currentButton.SetSelected(true);

            if (playSound)
            {
                AudioManager.Instance.PlaySFX(selectSound);
            }

            OnSelect?.Invoke(button);
        }

        /// <summary>
        /// Activates the current button, plays the selection sound, and raises the click event.
        /// </summary>
        private void TriggerClick()
        {
            if (currentButton != null)
            {
                currentButton.Click();
            }

            AudioManager.Instance.PlaySFX(selectSound);
            OnClick?.Invoke(currentButton);
        }

        /// <summary>
        /// Moves selection to the next interactable button.
        /// Wraps to the first interactable button after reaching the end.
        /// </summary>
        private void MoveNext()
        {
            if (buttons.Count == 0)
            {
                return;
            }

            int index = buttons.IndexOf(currentButton);

            if (index < 0)
            {
                return;
            }

            // Forward search
            for (int nextIndex = index + 1; nextIndex < buttons.Count; nextIndex++)
            {
                MenuButton button = buttons[nextIndex];

                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }

            // Wrap-around search (start -> before current)
            for (int wrapIndex = 0; wrapIndex < index; wrapIndex++)
            {
                MenuButton button = buttons[wrapIndex];
                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }

        /// <summary>
        /// Moves selection to the previous interactable button.
        /// Wraps to the last interactable button after reaching the start.
        /// </summary>
        private void MovePrevious()
        {
            if (buttons.Count == 0)
            {
                return;
            }

            int index = buttons.IndexOf(currentButton);

            if (index < 0)
            {
                return;
            }

            // Backward search
            for (int previousIndex = index - 1; previousIndex >= 0; previousIndex--)
            {
                MenuButton button = buttons[previousIndex];

                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }

            // Wrap-around search (end -> after current)
            for (int wrapIndex = buttons.Count - 1; wrapIndex > index; wrapIndex--)
            {
                MenuButton button = buttons[wrapIndex];
                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }
    }
}
