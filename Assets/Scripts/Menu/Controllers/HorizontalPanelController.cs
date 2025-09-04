using PokemonGame.Characters.Inputs;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    public class HorizontalPanelController : MonoBehaviour
    {
        private int currentPanelIndex;
        private int previousPanelIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyBinds.Right) && currentPanelIndex < transform.childCount - 1)
            {
                currentPanelIndex++;
            }
            if (Input.GetKeyDown(KeyBinds.Left) && currentPanelIndex > 0)
            {
                currentPanelIndex--;
            }

            UpdateSelection();
        }

        public void ResetController()
        {
            currentPanelIndex = 0;
        }

        private void UpdateSelection()
        {
            if (currentPanelIndex != previousPanelIndex)
            {
                transform.GetChild(previousPanelIndex).gameObject.SetActive(false);
                transform.GetChild(currentPanelIndex).gameObject.SetActive(true);

                previousPanelIndex = currentPanelIndex;
            }
        }
    }
}
