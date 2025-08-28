using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Defines a set of dialogue lines for interactions.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueDefinition", menuName = "Dialogue/Dialogue Definition")]
    public class DialogueDefinition : ScriptableObject
    {
        [SerializeField, Required, TextArea(1, 5)]
        [Tooltip("Dialogue lines shown in order.")]
        private string[] dialogues;

        /// <summary>
        /// Gets the dialogue lines defined in this asset.
        /// </summary>
        public string[] Dialogues => dialogues;
    }
}
