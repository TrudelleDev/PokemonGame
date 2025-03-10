using UnityEngine;

namespace PokemonGame.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue Data", menuName = "ScriptableObjects/Dialogue Data")]
    public class DialogueData: ScriptableObject
    {
        [SerializeField, TextArea(2, 5)] private string[] dialogues;

        public string[] Dialogues => dialogues;
    }
}
