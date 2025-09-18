using System;
using System.Collections.Generic;
using PokemonGame.Characters.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Handles vertical navigation between <see cref="MenuButton"/>s
    /// using keyboard or controller input.
    /// </summary>
    public class VerticalMenuController : MonoBehaviour
    {
        [Title("Button Sources")]
        [Tooltip("Parents to scan for MenuButton components.")]
        [ValidateInput(nameof(HasSources), "Assign at least one button source.")]
        [ChildGameObjectsOnly]
        [SerializeField, Required]
        private List<Transform> buttonSources = new();

        private readonly List<MenuButton> buttons = new();
        private MenuButton currentButton;

        /// <summary>
        /// Raised when a new button is selected.
        /// </summary>
        public event Action<MenuButton> OnSelect;

        /// <summary>
        /// Raised when the current button is clicked.
        /// </summary>
        public event Action<MenuButton> OnClick;

        /// <summary>
        /// Raised when cancel input is pressed.
        /// </summary>
        public event Action<MenuButton> OnCancel;

        /// <summary>
        /// Gets the currently selected button, or null if none.
        /// </summary>
        public MenuButton CurrentButton => currentButton;

        /// <summary>
        /// Gets or sets whether this controller accepts input.
        /// </summary>
        public bool AcceptInput { get; set; } = true;

        /// <summary>
        /// Refreshes the button list when enabled.
        /// </summary>
        private void OnEnable()
        {
            RefreshButtons();
        }

        private void Start()
        {
            RefreshButtons();
        }

        /// <summary>
        /// Processes navigation, click, and cancel input.
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
        /// Collects all menu buttons from the assigned sources
        /// and selects the first interactable one.
        /// </summary>
        public void RefreshButtons()
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

            SelectFirst();
        }

        /// <summary>
        /// Selects the first interactable button.
        /// </summary>
        public void SelectFirst()
        {
            foreach (MenuButton button in buttons)
            {
                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }

            currentButton = null;
        }

        /// <summary>
        /// Validates that at least one button source is assigned.
        /// </summary>
        private bool HasSources()
        {
            return buttonSources != null && buttonSources.Count > 0;
        }

        /// <summary>
        /// Selects the given button and raises selection events.
        /// </summary>
        /// <param name="button">The button to select.</param>
        private void SelectButton(MenuButton button)
        {
            if (currentButton != null)
            {
                currentButton.SetSelected(false);
            }

            currentButton = button;
            currentButton.SetSelected(true);

            OnSelect?.Invoke(button);
        }

        /// <summary>
        /// Invokes the click action on the current button.
        /// </summary>
        private void TriggerClick()
        {
            if (currentButton != null)
            {
                currentButton.Click();
            }

            OnClick?.Invoke(currentButton);
        }

        /// <summary>
        /// Selects the next interactable button.
        /// Stops at the last button instead of looping.
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

            for (int nextIndex = index + 1; nextIndex < buttons.Count; nextIndex++)
            {
                MenuButton button = buttons[nextIndex];

                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }

        /// <summary>
        /// Selects the previous interactable button.
        /// Stops at the first button instead of looping.
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

            for (int previousIndex = index - 1; previousIndex >= 0; previousIndex--)
            {
                MenuButton button = buttons[previousIndex];

                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }
    }
}
