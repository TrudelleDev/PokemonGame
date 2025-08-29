using PokemonGame.Characters.Enums.Extensions;
using UnityEngine;

namespace PokemonGame.Characters
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(InteractionHandler))]
    public class PlayerInteraction : MonoBehaviour
    {
        private Character character;
        private InteractionHandler interactionHandler;

        private void Awake()
        {
            character = GetComponent<Character>();
            interactionHandler = GetComponent<InteractionHandler>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Accept))
            {
                Vector2 dir = character.StateController.FacingDirection.ToVector2Int();

                if (interactionHandler.CheckForInteractables(dir))
                {
                    // Only cancel to Idle if something was actually interacted with
                    character.StateController.CancelToIdle();
                }
            }
        }
    }
}
