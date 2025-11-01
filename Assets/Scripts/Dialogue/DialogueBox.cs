using System;
using System.Collections;
using PokemonGame.Characters.Inputs;
using PokemonGame.Pause;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Handles the visual display and sequencing of dialogue lines in the game.
    /// Supports both typewriter-style text and instant display, manages input flow,
    /// and pauses gameplay while dialogue is active.
    /// </summary>
    public class DialogueBox : MonoBehaviour
    {
        private const string ArrowSpriteAsset = "<sprite name=Arrow>";

        [Title("Visual")]

        [SerializeField, Required, Space]
        [Tooltip("Text field that displays the dialogue content.")]
        private TextMeshProUGUI dialogueText;

        [SerializeField, Required]
        [Tooltip("Background image for the dialogue box.")]
        private Image boxImage;

        [SerializeField, Required]
        [Tooltip("Default theme applied at startup.")]
        private DialogueBoxTheme defaultTheme;

        [SerializeField, Required]
        [Tooltip("Root object containing all dialogue UI elements.")]
        private GameObject content;

        [Title("Settings")]

        [SerializeField, MinValue(0.01f)]
        [Tooltip("Delay between characters during the typewriter effect (in seconds).")]
        private float characterDelay = 0.05f;

        [SerializeField]
        [Tooltip("If true, automatically closes the dialogue box when all lines are finished.")]
        private bool autoClose = true;

        private string[] lines;
        private int lineIndex;
        private Coroutine dialogueCoroutine;
        private DialogueBoxTheme currentTheme;
        private bool instantMode;

        /// <summary>
        /// Invoked when the typewriter effect completes for a single dialogue line.
        /// </summary>
        public event Action OnLineTypingComplete;

        /// <summary>
        /// Invoked when the entire dialogue sequence finishes displaying.
        /// </summary>
        public event Action OnDialogueFinished;

        /// <summary>
        /// Initializes the dialogue box by applying the default theme
        /// and hiding its content if auto-close is enabled.
        /// </summary>
        private void Awake()
        {
            ApplyTheme(defaultTheme);

            if (autoClose)
            {
                content.SetActive(false);
            }
        }

        /// <summary>
        /// Applies a visual theme to the dialogue box, updating its background, font, and layout padding.
        /// </summary>
        /// <param name="theme">The theme asset that defines appearance settings for the dialogue box.</param>
        public void ApplyTheme(DialogueBoxTheme theme)
        {
            currentTheme = theme;
            boxImage.sprite = currentTheme.BoxSprite;
            dialogueText.font = currentTheme.Font;

            RectTransform rect = boxImage.GetComponent<RectTransform>();
            currentTheme.RectPadding.ApplyTo(rect);
        }

        /// <summary>
        /// Displays a single line of dialogue, instantly or with a typewriter effect.
        /// </summary>
        /// <param name="text">The dialogue text to display.</param>
        /// <param name="instant">If true, displays the text immediately without typing animation.</param>
        public void ShowDialogue(string text, bool instant = false)
        {
            ShowDialogue(new[] { text }, instant);
        }

        /// <summary>
        /// Begins displaying a dialogue sequence consisting of multiple lines.
        /// </summary>
        /// <param name="lines">An array of dialogue lines to display in order.</param>
        /// <param name="instant">If true, displays all lines instantly without the typewriter effect.</param>
        public void ShowDialogue(string[] lines, bool instant = false)
        {
            if (lines == null || lines.Length == 0)
            {
                Log.Warning(nameof(DialogueBox), "Tried to show empty or null dialogue.");
                return;
            }

            content.SetActive(true);
            PauseManager.SetPaused(true);

            this.lines = lines;
            lineIndex = 0;
            instantMode = instant;
            dialogueText.text = string.Empty;

            RestartCoroutine(ref dialogueCoroutine, RunDialogueSequence());
        }

        /// <summary>
        /// Handles the dialogue flow by displaying each line sequentially
        /// and waiting for player input before proceeding to the next.
        /// </summary>
        private IEnumerator RunDialogueSequence()
        {
            while (lineIndex < lines.Length)
            {
                bool showArrow = lineIndex < lines.Length - 1;
                yield return StartCoroutine(TypeLineCoroutine(lines[lineIndex], showArrow));

                lineIndex++;
                OnLineTypingComplete?.Invoke();

                yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
            }

            OnDialogueFinished?.Invoke();

            // Prevents immediate retriggering from the same input frame.
            yield return null;

            if (autoClose)
            {
                content.SetActive(false);
                PauseManager.SetPaused(false);
            }
        }

        /// <summary>
        /// Renders a single line of dialogue text, either instantly or gradually
        /// using the configured typewriter delay between characters.
        /// </summary>
        /// <param name="line">The text content of the dialogue line.</param>
        /// <param name="showArrow">If true, displays a sprite arrow indicating there are more lines.</param>
        private IEnumerator TypeLineCoroutine(string line, bool showArrow = true)
        {
            dialogueText.text = string.Empty;

            if (instantMode)
            {
                dialogueText.text = line;

                if (showArrow)
                {
                    dialogueText.text += ArrowSpriteAsset;
                }

                yield break;
            }

            WaitForSecondsRealtime delay = new(characterDelay);

            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return delay;
            }

            if (showArrow)
            {
                dialogueText.text += ArrowSpriteAsset;
            }
        }

        /// <summary>
        /// Stops an active coroutine (if any) and starts a new one to replace it.
        /// </summary>
        /// <param name="routine">Reference to the coroutine being restarted.</param>
        /// <param name="sequence">The coroutine sequence to start.</param>
        private void RestartCoroutine(ref Coroutine routine, IEnumerator sequence)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(sequence);
        }
    }
}
