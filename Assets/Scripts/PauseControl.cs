using PokemonGame.Dialogues;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class PauseControl : MonoBehaviour
    {
        public static bool IsGamePaused;

        private void Update()
        {
            // Pause the game when any views is open.
            if (ViewManager.Instance.IsHistoryEmpty() && !DialogueBoxController.Instance.IsDialogueBoxOpen())
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void Resume()
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
        }
    }
}
