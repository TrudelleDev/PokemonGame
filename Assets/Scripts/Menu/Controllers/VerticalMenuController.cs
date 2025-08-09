using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Controls vertical navigation between UI buttons using keyboard-style input.
    /// Automatically gathers buttons from configured sources and always starts at the first element.
    /// </summary>
    public class VerticalMenuController : MonoBehaviour
    {
        [Title("Auto Button Assignment")]
        [Tooltip("Parents to scan for Button components.")]
        [ValidateInput(nameof(HasSources), "Assign at least one button source.")]
        [ChildGameObjectsOnly]
        [SerializeField, Required]
        private List<Transform> buttonSources = new();

        private readonly List<Button> buttons = new();
        private Button currentButton;

        public event Action<Button> OnSelect;
        public event Action<Button> OnClick;
        public event Action<Button> OnCancel;

        private EventSystem ES => EventSystem.current;

        private void Awake()
        {
            RefreshButtons();
        }

        private void OnEnable()
        {
            RefreshButtons();
        }

        private void Update()
        {
            if (buttons.Count == 0 || currentButton == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBind.Down))
            {
                MoveNext();
            }
            else if (Input.GetKeyDown(KeyBind.Up))
            {
                MovePrevious();
            }
            else if (Input.GetKeyDown(KeyBind.Accept))
            {
                TriggerClick();
            }
            else if (Input.GetKeyDown(KeyBind.Cancel))
            {
                OnCancel?.Invoke(currentButton);
            }
        }

        /// <summary>
        /// Clears and repopulates the internal button list, then selects the first interactable.
        /// </summary>
        public void RefreshButtons()
        {
            buttons.Clear();

            for (int i = 0; i < buttonSources.Count; i++)
            {
                Transform source = buttonSources[i];
                if (source == null) continue;

                buttons.AddRange(source.GetComponentsInChildren<Button>(true));
            }

            SelectFirstElement();
        }

        private void SelectFirstElement()
        {
            foreach (Button button in buttons)
            {
                if (button != null && button.interactable)
                {
                    SelectButton(button);
                    return;
                }
            }

            currentButton = null;
            ES?.SetSelectedGameObject(null);
        }

        private bool HasSources() => buttonSources != null && buttonSources.Count > 0;

        private void SelectButton(Button button)
        {
            currentButton = button;
            ES?.SetSelectedGameObject(button.gameObject);
            OnSelect?.Invoke(button);
        }

        private void TriggerClick()
        {
            currentButton?.onClick.Invoke();
            OnClick?.Invoke(currentButton);
        }

        private void MoveNext()
        {
            int index = buttons.IndexOf(currentButton);
            if (index < 0) return;

            for (int i = index + 1; i < buttons.Count; i++)
            {
                Button button = buttons[i];

                if (button != null && button.interactable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }

        private void MovePrevious()
        {
            int index = buttons.IndexOf(currentButton);
            if (index < 0) return;

            for (int i = index - 1; i >= 0; i--)
            {
                Button button = buttons[i];

                if (button != null && button.interactable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }
    }
}
