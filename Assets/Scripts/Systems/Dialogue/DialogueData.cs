using UnityEngine;

namespace PokemonGame.Systems.Dialogue
{
    /// <summary>
    /// Stores a sequence of dialogue lines to be shown during interaction.
    /// Used by DialogueHolder to display conversations or messages.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [SerializeField, TextArea(1, 5)]
        [Tooltip("The dialogue lines to display in order.")]
        private string[] dialogues;

        /// <summary>
        /// The dialogue lines contained in this data asset.
        /// </summary>
        public string[] Dialogues => dialogues;
    }
}
