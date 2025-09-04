using PokemonGame.Characters;
using PokemonGame.Characters.Core;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Pause;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Interactable that starts a dialogue.
    /// </summary>
    [DisallowMultipleComponent]
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required]
        [Tooltip("Dialogue shown when interacted with.")]
        private DialogueDefinition definition;

        public void Interact(Character player)
        {
            if (PauseManager.IsPaused)
                return;

            if (definition == null)
            {
                Log.Warning(nameof(DialogueInteractable), $"{gameObject.name} has no DialogueDefinition assigned.");
                return;
            }

            player.StateController.CancelToIdle();
            DialogueBox.Instance.ShowDialogue(definition.Dialogues);
        }
    }
}