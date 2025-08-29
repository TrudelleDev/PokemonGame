using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Enums.Extensions;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Characters.States;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Ensures this NPC refaces toward the player when interacted with.
    /// Attach alongside any IInteract component.
    /// </summary>
    [RequireComponent(typeof(CharacterStateController))]
    public class NPCRefacingOnInteract : MonoBehaviour, IInteract
    {
        private CharacterStateController npcController;

        private void Awake()
        {
            npcController = GetComponent<CharacterStateController>();
        }

        public void Interact(Character player)
        {
            FacingDirection npcFacing = player.StateController.FacingDirection.Opposite();
            npcController.FacingDirection = npcFacing;
            npcController.AnimatorController.PlayRefacing(npcFacing);
        }
    }
}
