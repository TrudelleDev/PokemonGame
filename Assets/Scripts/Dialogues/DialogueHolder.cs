using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame.Dialogues
{
    public class DialogueHolder : MonoBehaviour, IInteract
    {
        [SerializeField] private DialogueData data;

        public void OnInteract(Character character)
        {
            DialogueBoxController.Instance.ShowDialogue(data.Dialogues);       
        }  
    }
}
