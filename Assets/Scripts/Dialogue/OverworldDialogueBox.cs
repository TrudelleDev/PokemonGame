using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Provides global access to the overworld dialogue box instance.
    /// Acts as a singleton wrapper for the main <see cref="DialogueBox"/> used
    /// during overworld interactions such as NPC conversations or sign reading.
    /// </summary>
    public sealed class OverworldDialogueBox : Singleton<OverworldDialogueBox>
    {
        [SerializeField, Required]
        [Tooltip("Reference to the dialogue box used for overworld dialogue interactions.")]
        private DialogueBox dialogueBox;

        /// <summary>
        /// Gets the dialogue box instance used for overworld dialogue.
        /// </summary>
        public DialogueBox Dialogue => dialogueBox;
    }
}
