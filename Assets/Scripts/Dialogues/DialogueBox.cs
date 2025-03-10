using System.Collections;
using TMPro;
using UnityEngine;

namespace PokemonGame.Dialogues
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private float timeDelay;

        private int index;
        private bool isLineOver = true;

        public void ShowDialogue(string[] dialogue)
        {
            if (index < dialogue.Length)
            {
                if (isLineOver)
                {
                    StartCoroutine(ShowDelayedLine(dialogue[index]));
                }
            }
            else
            {
                index = 0;
                gameObject.SetActive(false);
            }
        }

        private IEnumerator ShowDelayedLine(string line)
        {
            WaitForSecondsRealtime delay = new WaitForSecondsRealtime(timeDelay);

            isLineOver = false;

            for (int i = 0; i < line.Length; ++i)
            {
                // Create an empty string and then add every letters over time from the current line.
                string delayedText = line.Substring(0, i + 1);

                dialogueText.text = delayedText;
                yield return delay;
            }

            isLineOver = true;
            index++;
        }
    }
}
