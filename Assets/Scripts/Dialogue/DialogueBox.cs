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
    /// Handles the display and sequencing of dialogue lines.
    /// Supports typewriter effect, player input, and auto-pausing.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DialogueBox : MonoBehaviour
    {
        private const string ArrowSpriteAsset = "<sprite name=Arrow>";

        [Title("Visual")]
        [SerializeField, Required, Space] private TextMeshProUGUI dialogueText;
        [SerializeField, Required] private Image boxImage;
        [SerializeField, Required] private DialogueBoxTheme defaultTheme;
        [SerializeField, Required] private GameObject content;

        [Title("Audio")]
        [SerializeField, Required] private AudioClip textAdvanceSfx;

        [Title("Settings")]
        [SerializeField, MinValue(0.01f)] private float characterDelay = 0.05f;
        [SerializeField] private bool autoClose = true;

        private string[] lines;
        private int lineIndex;
        private Coroutine dialogueCoroutine;
        private bool instantMode;
        private bool waitForInput;

        public event Action OnLineTypingComplete;
        public event Action OnDialogueFinished;

        private void Awake()
        {
            ApplyTheme(defaultTheme);
            Clear();
            if (autoClose) content.SetActive(false);
        }

        /// <summary>
        /// Shows normal dialogue lines with typewriter effect.
        /// </summary>
        public void ShowDialogue(string text, bool instant = false)
        {
            ShowDialogue(new[] { text }, instant);
        }

        public void ShowDialogue(string[] lines, bool instant = false, bool waitForInput = false)
        {
            if (lines == null || lines.Length == 0)
            {
                Log.Warning(nameof(DialogueBox), "Tried to show empty dialogue.");
                return;
            }

            content.SetActive(true);
            PauseManager.SetPaused(true);

            this.lines = lines;
            this.waitForInput = waitForInput;
            lineIndex = 0;
            instantMode = instant;

            Clear();
            RestartCoroutine(ref dialogueCoroutine, RunDialogueSequence());
        }

        public void ShowPrompt(string text)
        {
            ShowDialogue(text, true);
        }

        public IEnumerator ShowDialogueAndWait(string text)
        {
            ShowDialogue(text);
            yield return WaitForTyping();
        }

        public IEnumerator ShowDialogueAndWaitForInput(string text)
        {
            ShowDialogue(new[] { text }, instant: false, waitForInput: true);
            yield return WaitForTyping();
            yield return WaitForAdvance();
        }

        public IEnumerator WaitForTyping()
        {
            bool done = false;
            void OnComplete() { done = true; OnLineTypingComplete -= OnComplete; }
            OnLineTypingComplete += OnComplete;
            yield return new WaitUntil(() => done);
        }

        public IEnumerator WaitForAdvance()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
            AudioManager.Instance.PlaySFX(textAdvanceSfx);
        }

        private IEnumerator RunDialogueSequence()
        {
            while (lineIndex < lines.Length)
            {
                yield return TypeLineCoroutine(lines[lineIndex]);
                OnLineTypingComplete?.Invoke();

                if (ShouldShowArrow)
                    AppendArrow();

                yield return WaitForAdvance();
                lineIndex++;
            }

            yield return null; // Prevents immediate reopen
            FinishDialogue();
        }

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

        private bool ShouldShowArrow =>
         waitForInput || (lines.Length > 1 && lineIndex < lines.Length - 1);

        private void AppendArrow()
        {
            if (!dialogueText.text.EndsWith(ArrowSpriteAsset))
                dialogueText.text += ArrowSpriteAsset;
        }

        public void Clear()
        {
            dialogueText.text = " ";
            dialogueText.ForceMeshUpdate();
        }

        private void FinishDialogue()
        {
            OnDialogueFinished?.Invoke();
            if (!autoClose) return;

            Clear();
            content.SetActive(false);
            PauseManager.SetPaused(false);
        }

        private void RestartCoroutine(ref Coroutine routine, IEnumerator sequence)
        {
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(sequence);
        }

        public void ApplyTheme(DialogueBoxTheme theme)
        {
            if (theme == null)
            {
                Log.Warning(nameof(DialogueBox), "Tried to apply a null theme.");
                return;
            }

            boxImage.sprite = theme.BoxSprite;
            dialogueText.font = theme.Font;

            RectTransform rect = boxImage.GetComponent<RectTransform>();
            theme.RectPadding.ApplyTo(rect);
        }
    }
}
