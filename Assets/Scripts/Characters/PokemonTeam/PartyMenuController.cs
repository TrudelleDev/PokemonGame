using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class PartyMenuController : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;

        private readonly List<Button> interactables = new();
        private int currentButtonIndex;
        private int previousButtonIndex;


        private void Start()
        {
            foreach (Button button in buttons)
            {
                if (button.interactable)
                {
                    interactables.Add(button);
                }
            }

            interactables[0].Select();
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

            UpdateSelection();
           
        }

        private void UpdateSelection()
        {
            if (currentButtonIndex != previousButtonIndex)
            {
                interactables[currentButtonIndex].Select();
                previousButtonIndex = currentButtonIndex;
            }
        }
    }
}
