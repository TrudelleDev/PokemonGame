using PokemonGame.Audio;
using PokemonGame.Characters.Config;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Handles horizontal navigation between child panels (left/right).
    /// Activates the newly selected panel, deactivates the previous one,
    /// and optionally plays a navigation sound on change.
    /// </summary>
    internal class HorizontalPanelController : MenuController
    {
        [SerializeField, Required]
        private AudioSetting audioSettings;

        private int currentPanelIndex;
        private int previousPanelIndex;

        /// <summary>
        /// Reads left/right input and updates the current panel index.
        /// </summary>
        private void Update()
        {
            if (ViewManager.Instance != null && ViewManager.Instance.IsTransitioning)
            {
                return;
            }

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

        /// <summary>
        /// Applies the panel change: toggles GameObjects and plays the navigation sound.
        /// </summary>
        private void UpdateSelection()
        {
            if (currentPanelIndex != previousPanelIndex)
            {
                transform.GetChild(previousPanelIndex).gameObject.SetActive(false);
                transform.GetChild(currentPanelIndex).gameObject.SetActive(true);

                if (audioSettings != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(audioSettings.UISelectClip);
                }

                previousPanelIndex = currentPanelIndex;
            }
        }

        public override void ResetController()
        {
            currentPanelIndex = 0;
        }
    }
}
