using System;
using System.Collections;
using PokemonGame.Audio;
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

        [Title("Audio")]
        [SerializeField, Required]
        private AudioClip textAdvanceSfx;

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
            Clear();

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
            if (theme == null)
            {
                Log.Warning(nameof(DialogueBox), "Tried to apply a null theme.");
                return;
            }

            currentTheme = theme;
            boxImage.sprite = currentTheme.BoxSprite;
            dialogueText.font = currentTheme.Font;

            RectTransform rect = boxImage.GetComponent<RectTransform>();
            currentTheme.RectPadding.ApplyTo(rect);
        }

        /// <summary>
        /// Clears the dialogue text field.
        /// </summary>
        public void Clear()
        {
            dialogueText.text = " ";
            dialogueText.ForceMeshUpdate();
        }

        /// <summary>
        /// Displays a single line of dialogue, instantly or with a typewriter effect.
        /// </summary>
        /// <param name="text">The dialogue text to display.</param>
        /// <param name="instant">If true, displays the text immediately without typing animation.</param>
        /// <param name="manualArrowControl">
        /// If true, allows external logic to manually control when the arrow indicator appears.
        /// </param>
        public void ShowDialogue(string text, bool instant = false, bool manualArrowControl = false)
        {
            ShowDialogue(new[] { text }, instant, manualArrowControl);
        }

        /// <summary>
        /// Begins displaying a dialogue sequence consisting of multiple lines.
        /// </summary>
        /// <param name="lines">An array of dialogue lines to display in order.</param>
        /// <param name="instant">If true, displays all lines instantly without the typewriter effect.</param>
        /// <param name="manualArrowControl">
        /// If true, allows external logic to manually control when the arrow indicator appears.
        /// </param>
        public void ShowDialogue(string[] lines, bool instant = false, bool manualArrowControl = false)
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
            Clear();

            RestartCoroutine(ref dialogueCoroutine, RunDialogueSequence(manualArrowControl));
        }

        public IEnumerator ShowDialogueAndWait(string text, bool instant = false, bool manualArrowControl = false)
        {
            ShowDialogue(text, instant, manualArrowControl);
            yield return WaitForTyping();
        }

        /// <summary>
        /// Waits until the current line of dialogue has finished typing out.
        /// </summary>
        private IEnumerator WaitForTyping()
        {
            bool done = false;

            // Local function to handle the event
            void OnComplete()
            {
                done = true;
                OnLineTypingComplete -= OnComplete;
            }

            OnLineTypingComplete += OnComplete;
            yield return new WaitUntil(() => done);
        }


        /// <summary>
        /// Handles dialogue flow manually controlled by external logic.
        /// Displays an arrow when waiting for player input between lines.
        /// </summary>
        private IEnumerator RunDialogueSequence(bool manualArrowControl = false)
        {
            while (lineIndex < lines.Length)
            {
                yield return StartCoroutine(TypeLineCoroutine(lines[lineIndex]));

                OnLineTypingComplete?.Invoke();

                if (lineIndex < lines.Length - 1 || manualArrowControl)
                {
                    ShowArrow();
                }

                yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
                AudioManager.Instance.PlaySFX(textAdvanceSfx);

                lineIndex++;
            }

            yield return null;
            OnDialogueFinished?.Invoke();

            if (autoClose)
            {
                Clear();
                content.SetActive(false);
                PauseManager.SetPaused(false);
            }
        }

        /// <summary>
        /// Renders a single line of dialogue text, either instantly or gradually
        /// using the configured typewriter delay between characters.
        /// </summary>
        private IEnumerator TypeLineCoroutine(string line)
        {
            dialogueText.text = string.Empty;

            if (instantMode)
            {
                dialogueText.text = line;
                yield break;
            }

            WaitForSecondsRealtime delay = new(characterDelay);

            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return delay;
            }
        }


        public IEnumerator WaitForPlayerAdvance()
        {
            // Then wait for key press
            yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
        }

        /// <summary>
        /// Stops an active coroutine (if any) and starts a new one to replace it.
        /// </summary>
        private void RestartCoroutine(ref Coroutine routine, IEnumerator sequence)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(sequence);
        }

        /// <summary>
        /// Displays an arrow sprite at the end of the dialogue text
        /// to indicate continuation or waiting for player input.
        /// </summary>
        private void ShowArrow()
        {
            if (!dialogueText.text.EndsWith(ArrowSpriteAsset))
            {
                dialogueText.text += ArrowSpriteAsset;
            }
        }
    }
}
