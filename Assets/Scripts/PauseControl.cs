using PokemonGame.Dialogues;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Controls the pausing and resuming of the game based on UI state.
    /// The game is paused when any relevant view (such as a dialogue box) is open.
    /// </summary>
    public class PauseControl : MonoBehaviour
    {
        public static bool IsGamePaused;

        private void Update()
        {
            bool shouldPause = !(ViewManager.Instance.IsHistoryEmpty() && !DialogueBoxController.Instance.IsDialogueBoxOpen());

            if (shouldPause != IsGamePaused)
            {
                if (shouldPause)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
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
