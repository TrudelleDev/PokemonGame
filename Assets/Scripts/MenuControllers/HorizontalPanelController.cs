using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public class HorizontalPanelController : MonoBehaviour
    {
        private int currentPanelIndex;
        private int previousPanelIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Right) && currentPanelIndex < transform.childCount - 1)
            {
                currentPanelIndex++;
            }
            if (Input.GetKeyDown(KeyBind.Left) && currentPanelIndex > 0)
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
