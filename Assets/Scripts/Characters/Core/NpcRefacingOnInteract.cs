using PokemonGame.Characters.Direction;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Characters.States;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Makes an NPC automatically face the player when interacted with.
    /// Attach alongside any component that implements IInteract.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterStateController))]
    public class NpcRefacingOnInteract : MonoBehaviour, IInteractable
    {
        private CharacterStateController npcController;

        private void Awake()
        {
            npcController = GetComponent<CharacterStateController>();
        }

        /// <summary>
        /// Called when the player interacts with this NPC.
        /// Rotates the NPC to face the player by setting its FacingDirection
        /// to the opposite of the player's and plays the refacing animation.
        /// </summary>
        /// <param name="player">The player character that initiated the interaction.</param>
        public void Interact(Character player)
        {
            npcController.Reface(player.StateController.FacingDirection.Opposite());
        }
    }
}
