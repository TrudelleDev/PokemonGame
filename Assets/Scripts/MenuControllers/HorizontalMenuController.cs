using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public class HorizontalMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject[] panels;

        private int currentPanelIndex;
        private int previousPanelIndex;

        private void Start()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }

            panels[0].SetActive(true);
        }

        public void ResetController()
        {
            currentPanelIndex = 0;
        }


        private void Update()
        {
            if (Input.GetKeyDown(Keybind.RightKey) && currentPanelIndex < panels.Length - 1)
            {
                currentPanelIndex++;
            }
            if (Input.GetKeyDown(Keybind.LeftKey) && currentPanelIndex > 0)
            {
                currentPanelIndex--;
            }

            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (currentPanelIndex != previousPanelIndex)
            {
                panels[currentPanelIndex].SetActive(true);
                panels[previousPanelIndex].SetActive(false);
                previousPanelIndex = currentPanelIndex;
            }
        }
    }
}
