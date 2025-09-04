using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Pause;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Dialogue box that displays lines with a typewriter effect.
    /// </summary>
    public class DialogueBox : Singleton<DialogueBox>
    {
        [SerializeField, Required]
        [Tooltip("Root UI object that holds the dialogue box content.")]
        private GameObject content;

        [SerializeField, Required]
        [Tooltip("Text element used to display the dialogue line.")]
        private TextMeshProUGUI displayText;

        [Title("Settings")]
        [SerializeField, Required]
        [Tooltip("Delay between each character for the typewriter effect (in seconds).")]
        private float timeDelay = 0.05f;

        [Header("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect that plays when a new dialogue line begins.")]
        private AudioClip interactClip;

        private string[] currentDialogue;
        private int currentIndex;

        /// <summary>
        /// Gets a value indicating whether the dialogue box is currently visible.
        /// </summary>
        public bool IsOpen => content.activeInHierarchy;

        /// <summary>
        /// Starts a dialogue sequence with the given lines.
        /// </summary>
        /// <param name="dialogue">The dialogue lines to display.</param>
        public void ShowDialogue(string[] dialogue)
        {
            if (dialogue == null || dialogue.Length == 0)
            {
                Log.Warning(nameof(DialogueBox), "Tried to show empty or null dialogue.");
                return;
            }

            if (IsOpen) return;

            currentDialogue = dialogue;
            currentIndex = 0;

            content.SetActive(true);
            PauseManager.SetPaused(true);
            StartCoroutine(DisplayNextLine());
        }

        private IEnumerator DisplayNextLine()
        {
            while (currentIndex < currentDialogue.Length)
            {
                // Play sound safely
                AudioManager.Instance.PlaySFX(interactClip);

                yield return StartCoroutine(TypeLine(currentDialogue[currentIndex]));
                currentIndex++;

                // Small yield so input can't be "missed" in the same frame
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
            }

            PauseManager.SetPaused(false);
            content.SetActive(false);
            displayText.text = "";
        }

        private IEnumerator TypeLine(string line)
        {
            displayText.text = "";
            var delay = new WaitForSecondsRealtime(timeDelay);

            foreach (char letter in line)
            {
                displayText.text += letter;
                yield return delay;
            }
        }
    }
}
