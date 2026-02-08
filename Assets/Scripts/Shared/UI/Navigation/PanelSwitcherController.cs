using MonsterTamer.Audio;
using MonsterTamer.Characters.Config;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Shared.UI.Navigation
{
    /// <summary>
    /// Handles switching between child panels using keyboard/controller input.
    /// Plays UI audio for selection.
    /// </summary>
    internal sealed class PanelSwitcherController : MonoBehaviour
    {
        [SerializeField, Tooltip("UI audio settings for selection and confirmation sounds.")]
        private UIAudioSettings audioSettings;

        private int currentIndex;
        private int previousIndex;

        private int ItemCount => transform.childCount;

        private void Awake()
        {
            ResetController();
        }

        private void Update()
        {
            if (ViewManager.Instance != null && ViewManager.Instance.IsTransitioning) return;

            if (Input.GetKeyDown(KeyBinds.Right) && currentIndex < ItemCount - 1)
            {
                SelectPanel(currentIndex + 1);
            }
            else if (Input.GetKeyDown(KeyBinds.Left) && currentIndex > 0)
            {
                SelectPanel(currentIndex - 1);
            }
        }

        /// <summary>
        /// Resets the controller to the first panel, hiding all others.
        /// </summary>
        public void ResetController()
        {
            for (int i = 0; i < ItemCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == 0);
            }

            currentIndex = 0;
            previousIndex = 0;
        }

        /// <summary>
        /// Selects a panel by index.
        /// Updates internal state, toggles panel visibility, and plays selection audio.
        /// </summary>
        /// <param name="newIndex">Index of the panel to select.</param>
        private void SelectPanel(int newIndex)
        {
            if (newIndex < 0 || newIndex >= ItemCount || newIndex == currentIndex)
            {
                return;
            }

            previousIndex = currentIndex;
            currentIndex = newIndex;

            ApplySelection(previousIndex, currentIndex);
            PlaySelectSfx();
        }

        /// <summary>
        /// Applies selection visuals by activating the current panel and deactivating the previous.
        /// </summary>
        /// <param name="previous">Previously active panel index.</param>
        /// <param name="current">Currently active panel index.</param>
        private void ApplySelection(int previous, int current)
        {
            if (previous >= 0 && previous < ItemCount)
            {
                transform.GetChild(previous).gameObject.SetActive(false);
            }

            if (current >= 0 && current < ItemCount)
            {
                transform.GetChild(current).gameObject.SetActive(true);
            }
        }

        private void PlaySelectSfx()
        {
            if (audioSettings != null)
            {
                AudioManager.Instance.PlayUISFX(audioSettings.SelectSfx);
            }
        }
    }
}
