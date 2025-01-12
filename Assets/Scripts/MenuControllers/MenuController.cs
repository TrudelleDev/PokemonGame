using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public abstract class MenuController : MonoBehaviour
    {
        protected List<MenuButton> interactables = new();
        protected int currentButtonIndex;
        protected int previousButtonIndex;

        public event Action<GameObject> Select;
        public event Action<GameObject> Click;

        private void Start()
        {
            PopulateMenuItemList();
            interactables[0].Select();
        }

        private void OnEnable()
        {
            if (interactables.Count > 0)
                OnSelect();
        }

        protected abstract void Update();

        protected void PopulateMenuItemList()
        {
            // Only add interactable menu button to the list
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MenuButton>() != null)
                {
                    if (transform.GetChild(i).transform.GetComponent<MenuButton>().Interactable)
                    {
                        interactables.Add(transform.GetChild(i).GetComponent<MenuButton>());
                    }
                }
            }
        }

        public void ResetMenuController()
        {
            if (interactables.Count > 0)
            {
                interactables[previousButtonIndex].UnSelect();
                interactables[currentButtonIndex].UnSelect();

                currentButtonIndex = 0;
                previousButtonIndex = 0;
            }
        }

        public void ResetMenuItemList()
        {
            interactables.Clear();
            PopulateMenuItemList();
        }

        protected void OnSelect()
        {
            interactables[previousButtonIndex].UnSelect();
            interactables[currentButtonIndex].Select();
            Select?.Invoke(interactables[currentButtonIndex].gameObject);
            previousButtonIndex = currentButtonIndex;
        }

        protected void OnClick()
        {
            interactables[currentButtonIndex].Click();
            Click?.Invoke(interactables[currentButtonIndex].gameObject);
        }

        protected void UpdateSelection()
        {
            if (currentButtonIndex != previousButtonIndex)
            {
                OnSelect();
            }
        }
    }
}
