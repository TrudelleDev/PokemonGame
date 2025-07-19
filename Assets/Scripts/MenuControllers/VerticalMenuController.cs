using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PokemonGame.MenuControllers
{
    /// <summary>
    /// Controls vertical navigation between UI buttons using keyboard input.
    /// Automatically collects buttons from specified source transforms and manages
    /// selection, interaction, and cancellation behavior.
    /// </summary>
    public class VerticalMenuController : MonoBehaviour
    {
        [Title("Auto Button Assignment")]
        [Tooltip("Sources (parents) from which to collect buttons automatically.")]
        [ValidateInput(nameof(HasSources), "You must assign at least one button source.")]
        [ChildGameObjectsOnly]
        [SerializeField] private List<Transform> buttonSources;

        private List<Button> buttons = new();
        private Button currentButton;

        public event Action<Button> OnSelect;
        public event Action<Button> OnClick;
        public event Action<Button> OnCancel;

        private void Awake()
        {
            RefreshButtons();
            ResetToFirstElement();
        }

        private void OnEnable()
        {
            if (currentButton != null)
            {
                EventSystem.current?.SetSelectedGameObject(currentButton.gameObject);
            }
        }

        private void Update()
        {
            if (buttons.Count == 0 || currentButton == null) return;

            if (Input.GetKeyDown(KeyBind.Down))
            {
                MoveNext();
            }
            else if (Input.GetKeyDown(KeyBind.Up))
            {
                MoveBack();
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
        /// Selects the first interactable button from the list.
        /// </summary>
        public void ResetToFirstElement()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];
                if (button.interactable)
                {
                    SelectButton(button);
                    return;
                }
            }
        }

        /// <summary>
        /// Clears and repopulates the internal button list by scanning the assigned sources.
        /// </summary>
        public void RefreshButtons()
        {
            buttons.Clear();

            for (int i = 0; i < buttonSources.Count; i++)
            {
                Transform source = buttonSources[i];
                if (source == null) continue;

                Button[] sourceButtons = source.GetComponentsInChildren<Button>(true);

                for (int j = 0; j < sourceButtons.Length; j++)
                {
                    buttons.Add(sourceButtons[j]);
                }
            }
        }

        /// <summary>
        /// Validates that at least one source is assigned in the inspector.
        /// </summary>
        private bool HasSources()
        {
            return buttonSources != null && buttonSources.Count > 0;
        }

        private void SelectButton(Button button)
        {
            currentButton = button;
            EventSystem.current?.SetSelectedGameObject(button.gameObject);
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
            if (index == -1) return;

            for (int i = index + 1; i < buttons.Count; i++)
            {
                Button nextButton = buttons[i];
                if (nextButton.interactable)
                {
                    SelectButton(nextButton);
                    return;
                }
            }
        }

        private void MoveBack()
        {
            int index = buttons.IndexOf(currentButton);
            if (index == -1) return;

            for (int i = index - 1; i >= 0; i--)
            {
                Button previousButton = buttons[i];
                if (previousButton.interactable)
                {
                    SelectButton(previousButton);
                    return;
                }
            }
        }
    }
}
