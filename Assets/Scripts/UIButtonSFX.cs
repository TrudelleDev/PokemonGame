using UnityEngine;
using UnityEngine.UI;
using PokemonGame.Audio; // Your AudioManager

namespace PokemonGame
{
    /// <summary>
    /// Plays a click sound whenever this button is pressed.
    /// If no sound is assigned, falls back to AudioManager's default click SFX.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UIButtonSFX : MonoBehaviour
    {
        [SerializeField, Tooltip("Sound played when this button is clicked. Leave empty to use global default.")]
        private AudioClip clickSound;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlayClick);
        }

        private void OnDestroy()
        {
            // Prevent leaks if object is destroyed
            if (button != null)
            {
                button.onClick.RemoveListener(PlayClick);
            }
        }

        private void PlayClick()
        {
            if ( clickSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(clickSound);
            }        
        }
    }
}
