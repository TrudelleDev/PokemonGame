using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PokemonGame
{
    public class DialogBox : Singleton<DialogBox>
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [Space]
        [SerializeField] private float timeDelay;
   
        private int index;
        private bool isLineOver = true;

        public bool IsDialogOver { get; private set; }

        public void DisplayText(string text)
        {
            IsDialogOver = false;

            // Show the content of the dialog box.
            transform.GetChild(0).gameObject.SetActive(true);

            // Create an array that contain every lines of the dialog.
            string[] lines = text.Split(Environment.NewLine.ToCharArray());

            if (index < lines.Length)
            {
                if (isLineOver)
                {
                    StartCoroutine(DisplayLine(lines[index]));
                }
            }

            // Close the dialog box if every line has been displayed.
            else
            {
                index = 0;
                IsDialogOver = true;

                // Hide the content of the dialog box.
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        private IEnumerator DisplayLine(string line)
        {
            isLineOver = false;
            WaitForSeconds delay = new WaitForSeconds(timeDelay);

            for (int i = 0; i < line.Length; ++i)
            {
                // Create an empty string and then add every letters over time from the current line.
                string delayedText = line.Substring(0, i + 1);

                textMesh.text = delayedText;
                yield return delay;
            }

            isLineOver = true;
            index++;

            // Add arrow sprite (sprite asset) at the end of the line.
            // This is used as a indicator to show the player the dialog is not over yet.
            textMesh.text += "<sprite name=\"Arrow>";
        }
    }
}
