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
    /// Handles vertical menu navigation for keyboard/controller input.
    /// Scans configured parents for <see cref="MenuButton"/>s,
    /// tracks the current selection, and raises events for selection and clicks.
    /// </summary>
    internal class VerticalMenuController : MenuController
    {
        [Title("Button Sources")]
        [SerializeField, Required, ChildGameObjectsOnly]
        [Tooltip("Parents to scan for MenuButton components.")]
        private List<Transform> buttonSources = new();

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

        /// <summary>
        /// Refreshes buttons and restores selection on enable.
        /// Uses a one-frame delay to allow UI to finish populating.
        /// </summary>
        private void OnEnable()
        {
            RebuildButtons();
            StartCoroutine(DelayedSelect());
        }

        /// <summary>
        /// Polls for input to move selection up/down or trigger a click.
        /// </summary>
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
        }

        private IEnumerator DelayedSelect()
        {
            // Wait one frame so that dynamic UI has finished populating
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
        /// Does not change the current selection; use <c>OnEnable</c>
        /// or explicit selection methods to restore or set a button.
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

            foreach (var button in buttons)
            {
                print(button.name + button.IsInteractable);
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
        /// Applies selection to the given button, plays audio if requested, and raises <see cref="OnSelect"/>.
        /// </summary>
        /// <param name="button">The button to select.</param>
        /// <param name="playSound">If true, plays the selection sound effect.</param>
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
            if (currentButton != null)
            {
                currentButton.Click();
                AudioManager.Instance.PlaySFX(selectSound);
            }

            OnClick?.Invoke(currentButton);
        }

        /// <summary>
        /// Moves selection down to the next interactable button, if one exists.
        /// </summary>
        private void MoveNext()
        {
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
        /// Moves selection up to the previous interactable button, if one exists.
        /// </summary>
        private void MovePrevious()
        {
            int index = buttons.IndexOf(currentButton);

            if (index < 0)
            {
                return;
            }

            for (int prevIndex = index - 1; prevIndex >= 0; prevIndex--)
            {
                MenuButton button = buttons[prevIndex];

                if (button != null && button.IsInteractable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }

        public override void ResetController()
        {
            SelectFirst();
        }
    }
}
