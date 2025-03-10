using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame.Dialogues
{
    public class DialogueHolder : MonoBehaviour, IInteract
    {
        [SerializeField] private DialogueData data;

        public void OnInteract()
        {
            DialogueBoxController.Instance.ShowDialogue(data.Dialogues);       
        }  
    }
}
