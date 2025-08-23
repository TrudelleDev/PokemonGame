using UnityEngine;

namespace PokemonGame.Pause
{
    public class PauseControl : MonoBehaviour
    {
        private void OnEnable()
        {
            PauseManager.OnPauseStateChanged += HandlePauseChange;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseStateChanged -= HandlePauseChange;
        }

        private void HandlePauseChange(bool isPaused)
        {
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
}
