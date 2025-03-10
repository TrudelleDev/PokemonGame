using UnityEngine;

namespace PokemonGame.Dialogues
{
    public class DialogueBoxController : Singleton<DialogueBoxController>
    {
        [SerializeField] private DialogueBox dialogueBox;

        public void ShowDialogue(string[] dialogue)
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueBox.ShowDialogue(dialogue);
        }

        public bool IsDialogueBoxOpen()
        {
            return dialogueBox.gameObject.activeInHierarchy; 
        }
    }
}
