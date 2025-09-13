using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Defines one or more dialogue lines
    /// to be displayed in sequence during an interaction.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueDefinition",menuName = "Dialogue/Dialogue Definition")]
    public class DialogueDefinition : ScriptableObject
    {
        [Title("Dialogue")]
        [SerializeField, Required, TextArea(1, 5)]
        [Tooltip("Dialogue lines shown sequentially when this definition is used.")]
        private string[] lines;

        /// <summary>
        /// The dialogue lines to display, in sequential order.
        /// </summary>
        public string[] Lines => lines;
    }
}
