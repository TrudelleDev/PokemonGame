using MonsterTamer.Characters;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Pause;
using MonsterTamer.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Dialogue
{
    /// <summary>
    /// An interactable object that triggers a dialogue sequence
    /// when the player interacts with it.
    /// </summary>
    [DisallowMultipleComponent]
    internal class DialogueInteractable : MonoBehaviour, IInteractable
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
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(definition.Lines);
        }
    }
}
