using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Menu.Controllers
{
    /// <summary>
    /// Handles horizontal navigation between child panels (left/right).
    /// Activates the newly selected panel, deactivates the previous one,
    /// and optionally plays a navigation sound on change.
    /// </summary>
    public class HorizontalPanelController : MonoBehaviour
    {
        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound played when the navigation changes.")]
        private AudioClip navigateSound;

        private int currentPanelIndex;
        private int previousPanelIndex;

        /// <summary>
        /// Reads left/right input and updates the current panel index.
        /// </summary>
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

        /// <summary>
        /// Resets selection to the first panel (index 0).
        /// </summary>
        public void ResetController()
        {
            currentPanelIndex = 0;
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

                if (navigateSound != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(navigateSound);
                }

                previousPanelIndex = currentPanelIndex;
            }
        }
    }
}
