using UnityEngine;

namespace PokemonGame
{
    public class MenuController : MonoBehaviour
    {
        private int currentbuttonIndex;
        private int previousButtonIndex;

        private void Start()
        {
            transform.GetChild(0).GetComponent<SelectableUIElement>().Select();
        }

        private void Update()
        {
            if (Input.GetKeyDown(Keybind.DownKey) && currentbuttonIndex < transform.childCount - 1)
            {
                currentbuttonIndex++;
            }
            if (Input.GetKeyDown(Keybind.UpKey) && currentbuttonIndex > 0)
            {
                currentbuttonIndex--;
            }
            if (Input.GetKeyDown(Keybind.AcceptKey))
            {
                transform.GetChild(currentbuttonIndex).GetComponent<SelectableUIElement>().Click();
            }

            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (currentbuttonIndex != previousButtonIndex)
            {
                if (transform.GetChild(currentbuttonIndex).GetComponent<SelectableUIElement>() != null)
                {
                    transform.GetChild(previousButtonIndex).GetComponent<SelectableUIElement>().UnSelect();
                    transform.GetChild(currentbuttonIndex).GetComponent<SelectableUIElement>().Select();

                    previousButtonIndex = currentbuttonIndex;
                }
            }
        }
    }
}
