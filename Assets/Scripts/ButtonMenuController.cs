using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PokemonGame
{
    public class ButtonMenuController : MonoBehaviour
    {
        private readonly List<Button> interactables = new();
        private int currentButtonIndex;
        private int previousButtonIndex;

        public event Action<GameObject> OnSelect;
        public event Action OnCancel;

        private void Start()
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponentInChildren<Button>())
                {
                    if (transform.GetChild(i).GetComponentInChildren<Button>().interactable)
                    {
                        interactables.Add(transform.GetChild(i).GetComponentInChildren<Button>());
                    }                    
                }
            }

            interactables[currentButtonIndex].Select();
        }

        private void OnEnable()
        {
            //interactables[currentButtonIndex].Select();
        }



        public void ResetController()
        {
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
            if (currentButtonIndex != previousButtonIndex)
            {
                interactables[currentButtonIndex].Select();
                OnSelect?.Invoke(interactables[currentButtonIndex].gameObject);
                previousButtonIndex = currentButtonIndex;
            }
        }
    }
}
