using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Closes the current view when any of the assigned keys are pressed and play a sound.
    /// </summary>
    public class CloseView : MonoBehaviour
    {
        [Title("Keys")]
        [SerializeField, Required]
        [Tooltip("Keys that trigger this view to close.")]
        private KeyCode[] closeKeys = { KeyBinds.Cancel };

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect played when the view is closed.")]
        private AudioClip closeSound;

        private void Update()
        {
            if (closeKeys == null || closeKeys.Length == 0)
            {
                return;
            }

            foreach (KeyCode key in closeKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Closes the current view by returning to the previous view via the ViewManager.
        /// </summary>
        public void Close()
        {
            AudioManager.Instance.PlaySFX(closeSound);
            ViewManager.Instance.GoToPreviousView();
        }
    }
}
