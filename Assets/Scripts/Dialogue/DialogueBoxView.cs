using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Utilities;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// A UI view for displaying dialogue lines with a typewriter effect.
    /// Handles sequencing, input to advance lines, and auto-closing when finished.
    /// </summary>
    public class DialogueBoxView : View
    {
        [Title("UI Elements")]
        [SerializeField, Required, Space]
        [Tooltip("Text element that displays the dialogue lines.")]
        private TextMeshProUGUI dialogueText;

        [Title("Settings")]
        [SerializeField, Required]
        [Tooltip("Delay between characters during the typewriter effect (in seconds).")]
        private float characterDelay = 0.05f;

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect played when a new dialogue line starts.")]
        private AudioClip dialogueSfx;

        private string[] lines;
        private int lineIndex;

        /// <summary>
        /// Begins a new dialogue sequence with the given lines.
        /// Opens this view and starts the typewriter coroutine.
        /// </summary>
        /// <param name="lines">The dialogue lines to display in order.</param>
        public void ShowDialogue(string[] lines)
        {
            if (lines == null || lines.Length == 0)
            {
                Log.Warning(nameof(DialogueBoxView), "Tried to show empty or null dialogue.");
                return;
            }

            this.lines = lines;
            lineIndex = 0;
            dialogueText.text = "";

            StartCoroutine(RunDialogueSequence());
        }

        /// <summary>
        /// Runs the dialogue sequence, displaying each line one by one.
        /// Waits for player input before advancing to the next line.
        /// </summary>
        private IEnumerator RunDialogueSequence()
        {
            while (lineIndex < lines.Length)
            {
                // Play sound safely
                AudioManager.Instance.PlaySFX(dialogueSfx);

                yield return StartCoroutine(TypeLineCoroutine(lines[lineIndex]));
                lineIndex++;

                // Small yield so input can't be "missed" in the same frame
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
            }

            yield return null;

            ViewManager.Instance.CloseCurrentView();
            dialogueText.text = "";
        }

        /// <summary>
        /// Types out a single line of dialogue character by character
        /// using the configured character delay.
        /// </summary>
        /// <param name="line">The dialogue line to display.</param>
        private IEnumerator TypeLineCoroutine(string line)
        {
            dialogueText.text = "";
            var delay = new WaitForSecondsRealtime(characterDelay);

            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return delay;
            }
        }
    }
}
