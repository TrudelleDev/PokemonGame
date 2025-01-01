using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PokemonGame.MenuControllers
{
    public class ButtonMenuController : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;

        private readonly List<Button> interactables = new();
        private int currentButtonIndex;
        private int previousButtonIndex;

        public event Action<GameObject> OnSelect;
        public event Action OnCancel;

        private void Start()
        {
            foreach (Button button in buttons)
            {
                if (button.interactable) 
                {
                    // Populate the list with only interactable buttons, ignore non-interactable buttons
                    interactables.Add(button);
                }
            }

            interactables[currentButtonIndex].Select();
        }


        public void ResetController()
        {
            // This will reset the state of the button to unselected
            EventSystem.current.SetSelectedGameObject(null);
            currentButtonIndex = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(Keybind.DownKey) && currentButtonIndex < interactables.Count - 1)
            {
                currentButtonIndex++;
            }
            if (Input.GetKeyDown(Keybind.UpKey) && currentButtonIndex > 0)
            {
                currentButtonIndex--;
            }
            if (Input.GetKeyDown(Keybind.AcceptKey))
            {
                interactables[currentButtonIndex].onClick.Invoke();
            }
            if (Input.GetKeyDown(Keybind.CancelKey))
            {
                OnCancel?.Invoke();
            }

            UpdateSelection();

        }

        private void UpdateSelection()
        {
            // Only update if the selection change
            if (currentButtonIndex != previousButtonIndex)
            {
                interactables[currentButtonIndex].Select();
                OnSelect?.Invoke(interactables[currentButtonIndex].gameObject);
                previousButtonIndex = currentButtonIndex;
            }
        }
    }
}
