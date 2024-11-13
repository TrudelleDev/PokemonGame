using PokemonClone.Config;
using PokemonClone.Menu;
using UnityEngine;

namespace PokemonClone.Controller
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
            if (Input.GetKeyDown(Configuration.DownKey) && currentbuttonIndex < transform.childCount - 1)
            {
                currentbuttonIndex++;
            }
            if (Input.GetKeyDown(Configuration.UpKey) && currentbuttonIndex > 0)
            {
                currentbuttonIndex--;
            }
            if (Input.GetKeyDown(Configuration.AcceptKey))
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
