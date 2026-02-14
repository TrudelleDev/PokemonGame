using System;
using System.Collections;
using System.Collections.Generic;
using MonsterTamer.Audio;
using MonsterTamer.Config;
using MonsterTamer.Pause;
using MonsterTamer.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Dialogue
{
    /// <summary>
    /// Handles the display and sequencing of dialogue lines.
    /// Supports typewriter effect, player input, auto-pausing, and paging.
    /// Shows up to 2 lines at a time, even for long paragraphs.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DialogueBox : MonoBehaviour
    {
        [Title("Visual")]
        [SerializeField, Required, Space] private TextMeshProUGUI dialogueText;
        [SerializeField, Required] private Image boxImage;
        [SerializeField, Required] private GameObject content;
        [SerializeField, Required] private Image cursor;

        [Title("Settings")]
        [SerializeField, MinValue(0.01f)] private float characterDelay = 0.05f;
        [SerializeField] private bool autoClose = true;
        [SerializeField, Required] private Audio.UIAudioSettings audioSetting;

        private string[] pages;      // Dialogue pages (2 lines per page)
        private int pageIndex;
        private Coroutine dialogueCoroutine;
        private bool instantMode;
        private bool waitForInput;

        public event Action OnLineTypingComplete;
        public event Action DialogueFinished;

        private void Awake()
        {
            Clear();
            if (autoClose) content.SetActive(false);
            cursor.gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows dialogue from a single string.
        /// Automatically splits lines by \n and pages 2 lines at a time.
        /// </summary>
        public void ShowDialogue(string text, bool instant = false, bool waitForInput = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Log.Warning(nameof(DialogueBox), "Tried to show empty dialogue.");
                return;
            }

            // Split into pages (2 lines per page)
            pages = GetPages(text, 2);
            pageIndex = 0;
            instantMode = instant;
            this.waitForInput = waitForInput;

            content.SetActive(true);
            PauseManager.SetPaused(true);

            Clear();
            RestartCoroutine(ref dialogueCoroutine, RunDialogueSequence());
        }

        /// <summary>
        /// Shows a dialogue prompt instantly (no typewriter effect).
        /// </summary>
        public void ShowPrompt(string text)
        {
            ShowDialogue(text, instant: true);
        }

        public IEnumerator ShowDialogueAndWait(string text)
        {
            ShowDialogue(text);
            yield return WaitForTyping();
        }

        public IEnumerator ShowDialogueAndWaitForInput(string text)
        {
            ShowDialogue(text, instant: false, waitForInput: true);
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

            if (audioSetting != null)
            {
                AudioManager.Instance.PlayUISFX(audioSetting.ConfirmSfx);
            }
        }

        private IEnumerator RunDialogueSequence()
        {
            while (pageIndex < pages.Length)
            {
                yield return TypeLineCoroutine(pages[pageIndex]);
                OnLineTypingComplete?.Invoke();

                if (ShouldShowArrow)
                    cursor.gameObject.SetActive(true);

                yield return WaitForAdvance();

                cursor.gameObject.SetActive(false);
                pageIndex++;
            }

            FinishDialogue();
        }

        private IEnumerator TypeLineCoroutine(string page)
        {
            dialogueText.text = "";

            if (instantMode)
            {
                dialogueText.text = page;
                yield break;
            }

            WaitForSecondsRealtime delay = new(characterDelay);
            foreach (char letter in page)
            {
                dialogueText.text += letter;
                yield return delay;
            }
        }

        private bool ShouldShowArrow =>
            waitForInput || (pages.Length > 1 && pageIndex < pages.Length - 1);

        public void Clear()
        {
            dialogueText.text = " ";
            dialogueText.ForceMeshUpdate();
        }

        private void FinishDialogue()
        {
            DialogueFinished?.Invoke();
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

        /// <summary>
        /// Splits a string into pages with a maximum number of lines per page.
        /// </summary>
        private static string[] GetPages(string text, int linesPerPage)
        {
            string[] allLines = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

            List<string> pages = new List<string>();

            for (int i = 0; i < allLines.Length; i += linesPerPage)
            {
                int count = Mathf.Min(linesPerPage, allLines.Length - i);
                string page = string.Join("\n", allLines, i, count);
                pages.Add(page);
            }

            return pages.ToArray();
        }
    }
}
