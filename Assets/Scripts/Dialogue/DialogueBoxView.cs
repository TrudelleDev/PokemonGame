using System;
using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Utilities;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// A UI view for displaying dialogue lines with a typewriter effect.
    /// Handles sequencing, input to advance lines, and auto-closing when finished.
    /// </summary>
    public class DialogueBoxView : View
    {
        private const string ArrowSpriteAsset = "<sprite name=Arrow>";
        private const string Group = "Dialogue Settings";

        [BoxGroup(Group), SerializeField, Required, Space]
        [Tooltip("Text field that displays the dialogue content.")]
        private TextMeshProUGUI dialogueText;

        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("Background image for the dialogue box.")]
        private Image boxImage;

        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("Default behavior settings applied at startup.")]
        private DialogueBoxSetting defaultSetting;

        private string[] lines;
        private int lineIndex;

        private Coroutine dialogueCoroutine;
        private DialogueBoxSetting currentSetting;

        public event Action OnDialogueFinished;
        public event Action OnLineFinished;

        private void Awake()
        {
            ApplySetting(defaultSetting);
        }

        /// <summary>
        /// Applies a behavior setting asset to control dialogue speed and flow.
        /// </summary>
        public void ApplySetting(DialogueBoxSetting setting)
        {
            currentSetting = setting;
            boxImage.sprite = currentSetting.BoxSprite;
            dialogueText.font = currentSetting.Font;

            RectTransform rect = boxImage.GetComponent<RectTransform>();
            currentSetting.RectPadding.ApplyTo(rect);
        }

        /// <summary>
        /// Begins a new dialogue sequence with the given lines.
        /// </summary>
        public void ShowDialogue(string[] lines, bool isInstant = false)
        {
            if (lines == null || lines.Length == 0)
            {
                Log.Warning(nameof(DialogueBoxView), "Tried to show empty or null dialogue.");
                return;
            }

            this.lines = lines;
            lineIndex = 0;
            dialogueText.text = string.Empty;

            RestartCoroutine(ref dialogueCoroutine, RunDialogueSequence(isInstant));
        }

        /// <summary>
        /// Runs the dialogue sequence, displaying each line one by one.
        /// Waits for player input before advancing to the next line.
        /// </summary>
        private IEnumerator RunDialogueSequence(bool isInstant = false)
        {
            while (lineIndex < lines.Length)
            {
                AudioManager.Instance.PlaySFX(currentSetting.DialogueSfx);

                bool showArrow = lineIndex < lines.Length - 1; // show only if not last line
                yield return StartCoroutine(TypeLineCoroutine(lines[lineIndex], isInstant, showArrow));

                lineIndex++;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyBinds.Interact));
                OnLineFinished?.Invoke();
            }

            yield return null;
            OnDialogueFinished?.Invoke();

            if (currentSetting.AutoClose)
            {
                ViewManager.Instance.CloseCurrentView();
            }
        }

        /// <summary>
        /// Types out a single line of dialogue character by character using the configured delay.
        /// </summary>
        private IEnumerator TypeLineCoroutine(string line, bool isInstant = false, bool showArrow = true)
        {
            dialogueText.text = string.Empty;

            if (isInstant)
            {
                dialogueText.text = line;

                if (showArrow)
                {
                    // Append arrow icon to indicate next line
                    dialogueText.text += ArrowSpriteAsset;
                }

                yield break;
            }

            WaitForSecondsRealtime delay = new(currentSetting.CharacterDelay);

            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return delay;
            }

            if (showArrow)
            {
                // Append arrow icon to indicate next line
                dialogueText.text += ArrowSpriteAsset;
            }
        }

        /// <summary>
        /// Stops a running coroutine and restarts it with a new one.
        /// </summary>
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
