using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class VerticalMenuController : MonoBehaviour
    {
        [SerializeField] private MenuButton[] menuItems;

        private int currentButtonIndex;
        private int previousButtonIndex;
        private readonly List<MenuButton> interactables = new();

        public event Action<GameObject> OnSelect;

        private void Start()
        {
            /*
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (menuItems[i].Interactable)
                {
                    interactables.Add(menuItems[i]);
                }
            }
            */

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponentInChildren<MenuButton>())
                {
                    interactables.Add(transform.GetChild(i).GetComponentInChildren<MenuButton>());
                }
            }

            interactables[0].Select();
        }

        public void ResetController()
        {
            interactables[previousButtonIndex].UnSelect();
            interactables[currentButtonIndex].UnSelect();

            currentButtonIndex = 0;
            previousButtonIndex = 0;
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
                interactables[currentButtonIndex].Click();
                OnSelect?.Invoke(interactables[currentButtonIndex].gameObject);

            }

            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (currentButtonIndex != previousButtonIndex)
            {
                interactables[previousButtonIndex].UnSelect();
                interactables[currentButtonIndex].Select();

                OnSelect?.Invoke(interactables[currentButtonIndex].gameObject);

                previousButtonIndex = currentButtonIndex;
            }
        }
    }
}
