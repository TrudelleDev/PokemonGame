using System.Collections;
using PokemonGame.Characters;
using PokemonGame.Pause;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Systems.Dialogue
{
    /// <summary>
    /// Displays dialogue lines with a typewriter effect and waits for player input to advance.
    /// Singleton for global access.
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
        [Tooltip("Delay between each character for the typewriter effect.")]
        private float timeDelay = 0.05f;

        private string[] currentDialogue;
        private int currentIndex;

        /// <summary>
        /// Starts a dialogue sequence with the given lines.
        /// </summary>
        /// <param name="dialogue">An array of dialogue lines to display.</param>
        public void ShowDialogue(string[] dialogue)
        {
            if (dialogue == null || dialogue.Length == 0)
            {
                Debug.LogWarning("[DialogueBox] Tried to show empty or null dialogue.");
                return;
            }

            if (content.activeInHierarchy)
                return;

            currentDialogue = dialogue;
            currentIndex = 0;

            content.SetActive(true);
            PauseManager.SetPaused(true);
            StartCoroutine(DisplayNextLine());
        }

        /// <summary>
        /// Displays each dialogue line one at a time, waiting for player input to continue.
        /// </summary>
        private IEnumerator DisplayNextLine()
        {
            while (currentIndex < currentDialogue.Length)
            {
                yield return StartCoroutine(TypeLine(currentDialogue[currentIndex]));
                currentIndex++;

                // Wait for player to press Accept key before continuing
                yield return new WaitUntil(() => Input.GetKeyDown(KeyBind.Accept));
            }

            PauseManager.SetPaused(false);
            content.SetActive(false);
        }

        /// <summary>
        /// Types out a single line with a typewriter effect.
        /// </summary>
        /// <param name="line">The line of dialogue to type.</param>
        private IEnumerator TypeLine(string line)
        {
            displayText.text = "";

            WaitForSecondsRealtime delay = new WaitForSecondsRealtime(timeDelay);

            foreach (char letter in line)
            {
                displayText.text += letter;
                yield return delay;
            }
        }

        /// <summary>
        /// Returns true if the dialogue box is currently visible.
        /// </summary>
        public bool IsOpen()
        {
            return content.activeInHierarchy;
        }
    }
}
