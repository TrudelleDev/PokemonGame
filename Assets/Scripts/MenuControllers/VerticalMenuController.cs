using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public class VerticalMenuController : MonoBehaviour
    {
        [Title("Settings")]
        [SerializeField] private bool useManualButtonSetup;

        [ShowIf("useManualButtonSetup")]
        [ListDrawerSettings(DraggableItems = false, ShowFoldout = true)]
        [SerializeField] private List<MenuButton> buttons;

        private MenuButton currentButton;
        private MenuButton previousButton;

        public event Action<MenuButton> OnSelect;
        public event Action<MenuButton> OnClick;
        public event Action<MenuButton> OnCancel;

        private void Awake()
        {
            if (!useManualButtonSetup)
            {
                ClearAndRepopulate();
            }
            if (buttons.Count > 0)
            {
                foreach (MenuButton button in buttons)
                {
                    if (button.Interactable)
                    {
                        currentButton = button;
                        previousButton = button;

                        currentButton?.Select();

                        return;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"{nameof(VerticalMenuController)}: No buttons found during Awake.");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Down))
            {
                MoveNext();
            }
            if (Input.GetKeyDown(KeyBind.Up))
            {
                MoveBack();
            }
            if (Input.GetKeyDown(KeyBind.Accept))
            {
                TriggerClick();
            }
            if (Input.GetKeyDown(KeyBind.Cancel))
            {
                TriggerCancel();
            }


        }
        public void Initialize()
        {
            foreach (MenuButton button in buttons)
            {
                if (button.Interactable)
                {
                    previousButton.UnSelect();
                    currentButton.UnSelect();

                    currentButton = button;
                    previousButton = button;

                    currentButton.Select();
                    return;
                }
            }
        }

        public void ResetToFirstElement()
        {

            if (buttons.Count > 0)
            {
                previousButton?.UnSelect();
                currentButton?.UnSelect();

                currentButton = buttons[0];
                previousButton = buttons[0];

                UpdateSelection();
            }
        }

        public void ClearAndRepopulate()
        {
            buttons.Clear();
            AssignButtonsAutomatically();
            ResetToFirstElement();
        }

        private void AssignButtonsAutomatically()
        {
            buttons ??= new List<MenuButton>();

            // Only add interactable menu button to the list.
            foreach (Transform child in transform)
            {
                MenuButton button = child.GetComponent<MenuButton>();

                if (button != null && button.Interactable)
                {
                    buttons.Add(button);
                }
            }
        }

        private void UpdateSelection()
        {
            if (buttons == null || buttons.Count == 0 || currentButton == null) return;

            previousButton.UnSelect();
            currentButton.Select();
            OnSelect?.Invoke(currentButton);
            previousButton = currentButton;
        }

        private void TriggerClick()
        {
            currentButton.Click();
            OnClick?.Invoke(currentButton);
        }

        private void TriggerCancel()
        {
            OnCancel?.Invoke(currentButton);
        }

        private void MoveNext()
        {
            int index = buttons.IndexOf(currentButton);

            if (index == -1) return; // Early exit

            do
            {
                index++;
            } while (index < buttons.Count && !buttons[index].Interactable);

            if (index < buttons.Count)
            {
                currentButton = buttons[index];
                UpdateSelection();
            }
        }

        private void MoveBack()
        {
            int index = buttons.IndexOf(currentButton);

            if (index == -1) return; // Early exit

            do
            {
                index--;
            } while (index >= 0 && !buttons[index].Interactable);

            if (index >= 0)
            {
                currentButton = buttons[index];
                UpdateSelection();
            }
        }
    }
}
