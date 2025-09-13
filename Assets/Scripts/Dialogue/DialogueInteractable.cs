using PokemonGame.Characters.Core;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Pause;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// An interactable object that triggers a dialogue sequence
    /// when the player interacts with it.
    /// </summary>
    [DisallowMultipleComponent]
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required]
        [Tooltip("Dialogue definition to play when this object is interacted with.")]
        private DialogueDefinition definition;

        /// <summary>
        /// Called when the player interacts with this object.
        /// Cancels player movement and starts the assigned dialogue.
        /// </summary>
        public void Interact(Character player)
        {
            if (PauseManager.IsPaused)
            {
                return;
            }

            if (definition == null)
            {
                Log.Warning(nameof(DialogueInteractable), $"{gameObject.name} has no DialogueDefinition assigned.");
                return;
            }

            player.StateController.CancelToIdle();

            ViewManager.Instance.Show<DialogueBoxView>();
            ViewManager.Instance.Get<DialogueBoxView>().ShowDialogue(definition.Lines);
        }
    }
}
