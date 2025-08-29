using System.Collections;
using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Enums.Extensions;
using PokemonGame.Characters.Inputs.Enums;
using PokemonGame.Characters.Inputs.Extensions;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Walking state: moves the character one tile in the current facing direction.
    /// Triggers interactions and handles collisions before moving.
    /// </summary>
    public class CharacterWalkingState : ICharacterState
    {
        private readonly CharacterStateController controller;
        private readonly InteractionHandler interactionHandler;
        private Coroutine walkRoutine;

        public CharacterWalkingState(CharacterStateController controller)
        {
            this.controller = controller;
            interactionHandler = controller.GetComponent<InteractionHandler>();
        }

        public void Enter()
        {
            InputDirection input = controller.Input.InputDirection;
            if (input == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
                return;
            }

            FacingDirection facing = input.ToFacingDirection();
            controller.FacingDirection = facing;

            // Check triggers BEFORE walking
            if (interactionHandler != null && interactionHandler.CheckForTriggers(input.ToVector2()) == true)
            {
                controller.SetState(controller.IdleState);
                return;
            }

            // Blocked tile → collision state
            if (!controller.TileMover.CanMoveInDirection(facing))
            {
                controller.SetState(controller.CollisionState);
                return;
            }

            // Safe to move → walk coroutine
            walkRoutine = controller.StartCoroutine(Walk(facing));
        }



        public void Update() { }

        public void Exit()
        {
            if (walkRoutine != null)
            {
                controller.StopCoroutine(walkRoutine);
                walkRoutine = null;
            }
        }

        /// <summary>
        /// Executes tile movement, then decides next state based on new input.
        /// </summary>
        private IEnumerator Walk(FacingDirection direction)
        {
            Vector2Int move = direction.ToVector2Int();
            Vector3 destination = controller.transform.position +
                new Vector3(move.x * TilemapInfo.CellSize.x, move.y * TilemapInfo.CellSize.y, 0f);

            controller.AnimatorController.UpdateDirection(direction);
            controller.AnimatorController.PlayWalkStep();

            yield return controller.TileMover.MoveToTile(destination, controller.WalkDuration);

            // Decide next state
            InputDirection nextInput = controller.Input.InputDirection;
            controller.SetState(nextInput == InputDirection.None
                ? controller.IdleState
                : controller.WalkingState);
        }
    }
}
