using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Dialogue
{
    /// <summary>
    /// Holds dialogue data for an interactable object such as an NPC or sign.
    /// When interacted with, it triggers the dialogue box.
    /// Also defines the object's facing direction.
    /// </summary>
    public class DialogueHolder : MonoBehaviour, IInteract
    {
        [SerializeField, Required]
        [Tooltip("The dialogue content to display when interacted with.")]
        private DialogueData data;

        /// <summary>
        /// Called when a character interacts with this object.
        /// Triggers the dialogue box with the assigned dialogue data.
        /// </summary>
        /// <param name="character">The character interacting with this object.</param>
        public void Interact(Character character)
        {
            DialogueBox.Instance.ShowDialogue(data.Dialogues);
        }
    }
}
