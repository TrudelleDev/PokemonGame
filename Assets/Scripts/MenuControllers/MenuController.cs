using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public abstract class MenuController : MonoBehaviour
    {
        [SerializeField] private bool setButtonsManually;

        [ShowIf("setButtonsManually")]
        [ListDrawerSettings(DraggableItems = false, ShowFoldout = true)]
        [SerializeField] protected List<MenuButton> buttons;

        protected MenuButton currentButton;
        protected MenuButton previousButton;

        public event Action<MenuButton> Select;
        public event Action<MenuButton> Click;

        private void Awake()
        {
            if (!setButtonsManually)
            {
                AssignButtonsAutomatically();
            }

            if (buttons.Count > 0)
            {
                currentButton = buttons[0];
                previousButton = buttons[0];
                currentButton.Select();
            }
        }

        private void OnEnable()
        {
            currentButton.Select();
        }

        protected abstract void Update();

        protected void AssignButtonsAutomatically()
        {
            if (buttons == null)
            {
                buttons = new List<MenuButton>();
            }

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

        public void ResetMenuController()
        {
            if (buttons.Count > 0)
            {
                previousButton?.UnSelect();
                currentButton?.UnSelect();

                currentButton = buttons[0];
                previousButton = buttons[0];

                currentButton.Select();
            }
        }

        public void ResetMenuItemList()
        {
            buttons.Clear();
            AssignButtonsAutomatically();
        }

        protected void OnSelect()
        {
            if (buttons.Count == 0 || currentButton == null) return;

            previousButton.UnSelect();
            currentButton.Select();
            Select?.Invoke(currentButton);
            previousButton = currentButton;
        }

        protected int GetButtonIndex(MenuButton button)
        {
            return buttons.IndexOf(button);
        }

        protected void NextButton()
        {
            int index = buttons.IndexOf(currentButton);

            if (index >= 0 && index < buttons.Count - 1)
            {
                currentButton = buttons[index + 1];
            }
        }

        protected void PreviousButton()
        {
            int index = buttons.IndexOf(currentButton);

            if (index > 0)
            {
                currentButton = buttons[index - 1];
            }
        }

        protected void OnClick()
        {
            currentButton.Click();
            Click?.Invoke(currentButton);
        }

        protected void UpdateSelection()
        {
            if (currentButton != previousButton)
            {
                OnSelect();
            }
        }
    }
}
