using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Handles four-directional grid-based navigation for menu buttons.
    /// Scans configured parents for <see cref="MenuButton"/>s,
    /// supports keyboard/controller input, and raises selection and click events.
    /// </summary>
    [DisallowMultipleComponent]
    public class GridMenuController : MonoBehaviour
    {
        [Title("Button Sources")]
        [SerializeField, Required, ChildGameObjectsOnly]
        [Tooltip("Parents to scan for MenuButton components.")]
        private List<Transform> buttonSources = new();

        [Title("Grid Settings")]
        [SerializeField, MinValue(1)]
        [Tooltip("Number of columns in this grid. Buttons will wrap according to this width.")]
        private int columns = 2;

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect for selection changes and clicks.")]
        private AudioClip selectSound;

        private readonly List<MenuButton> buttons = new();
        private MenuButton currentButton;

        /// <summary>
        /// Raised when a new button becomes selected.
        /// </summary>
        public event Action<MenuButton> OnSelect;

        /// <summary>
        /// Raised when the currently selected button is clicked.
        /// </summary>
        public event Action<MenuButton> OnClick;

        /// <summary>
        /// The currently selected button, or null if none.
        /// </summary>
        public MenuButton CurrentButton => currentButton;

        private void OnEnable()
        {
            RebuildButtons();
            StartCoroutine(DelayedSelect());
        }

        private void Update()
        {
            if (ViewManager.Instance != null && ViewManager.Instance.IsTransitioning)
            {
                return;
            }

            if (buttons.Count == 0 || currentButton == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Right)) MoveRight();
            else if (Input.GetKeyDown(KeyBinds.Left)) MoveLeft();
            else if (Input.GetKeyDown(KeyBinds.Down)) MoveDown();
            else if (Input.GetKeyDown(KeyBinds.Up)) MoveUp();
            else if (Input.GetKeyDown(KeyBinds.Interact)) TriggerClick();
        }

        private IEnumerator DelayedSelect()
        {
            yield return null;

            if (currentButton != null && buttons.Contains(currentButton) && currentButton.IsInteractable)
            {
                SelectButton(currentButton, false);
            }
            else
            {
                SelectFirst();
            }
        }

        /// <summary>
        /// Rebuilds the internal button list by scanning all configured sources
        /// for <see cref="MenuButton"/> components.
        /// </summary>
        public void RebuildButtons()
        {
            buttons.Clear();

            foreach (Transform source in buttonSources)
            {
                if (source != null)
                {
                    buttons.AddRange(source.GetComponentsInChildren<MenuButton>(true));
                }
            }
        }

        /// <summary>
        /// Selects the first interactable button in the list, if any exist.
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
        /// Applies selection to the given button, optionally plays audio, and raises <see cref="OnSelect"/>.
        /// </summary>
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
        /// Triggers a click on the current button, plays audio, and raises <see cref="OnClick"/>.
        /// </summary>
        private void TriggerClick()
        {
            if (currentButton == null)
            {
                return;
            }

            currentButton.Click();
            AudioManager.Instance.PlaySFX(selectSound);
            OnClick?.Invoke(currentButton);
        }

        // ─────────────────────────────────────────────
        // NAVIGATION LOGIC
        // ─────────────────────────────────────────────

        private void MoveRight()
        {
            int index = buttons.IndexOf(currentButton);
            if (index < 0 || (index % columns) == columns - 1) return;

            int nextIndex = index + 1;
            if (IsValidButton(nextIndex)) SelectButton(buttons[nextIndex]);
        }

        private void MoveLeft()
        {
            int index = buttons.IndexOf(currentButton);
            if (index < 0 || (index % columns) == 0) return;

            int prevIndex = index - 1;
            if (IsValidButton(prevIndex)) SelectButton(buttons[prevIndex]);
        }

        private void MoveDown()
        {
            int index = buttons.IndexOf(currentButton);
            int nextIndex = index + columns;
            if (IsValidButton(nextIndex)) SelectButton(buttons[nextIndex]);
        }

        private void MoveUp()
        {
            int index = buttons.IndexOf(currentButton);
            int prevIndex = index - columns;
            if (IsValidButton(prevIndex)) SelectButton(buttons[prevIndex]);
        }

        private bool IsValidButton(int index)
        {
            return index >= 0 && index < buttons.Count && buttons[index] != null && buttons[index].IsInteractable;
        }
    }
}
