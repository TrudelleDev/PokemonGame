using PokemonGame.Characters;
using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Enums.Extensions;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Characters.States;
using PokemonGame.Pause;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Dialogue
{
    /// <summary>
    /// Holds dialogue data and triggers interaction.
    /// If attached to an NPC (i.e., has a <see cref="CharacterStateController"/>),
    /// the NPC will reface the player before dialogue starts.
    /// </summary>
    [DisallowMultipleComponent]
    public class DialogueHolder : MonoBehaviour, IInteract
    {
        [SerializeField, Required]
        [Tooltip("The dialogue script to display when interacted with.")]
        private DialogueData data;

        /// <summary>
        /// Called when a character interacts with this dialogue holder.
        /// </summary>
        public void Interact(Character interactor)
        {
            if (PauseManager.IsPaused || data == null)        
                return;         

            // If this holder is an NPC, reface toward the player
            if (TryGetComponent(out CharacterStateController controller))
            {
                FacingDirection opposite = interactor.StateController.FacingDirection.Opposite();
                controller.FacingDirection = opposite;
                controller.AnimatorController.PlayRefacing(opposite);
            }

            DialogueBox.Instance.ShowDialogue(data.Dialogues);
        }
    }
}
