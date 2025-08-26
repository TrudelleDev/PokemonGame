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
    /// Transitions to the next state after movement completes based on updated input.
    /// </summary>
    public class CharacterWalkingState : ICharacterState
    {
        private readonly CharacterStateController controller;
        private Coroutine walkRoutine;

        public CharacterWalkingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        public void Enter()
        {
            InputDirection inputDirection = controller.Input.InputDirection;
            FacingDirection facingDirection = inputDirection.ToFacingDirection();

            // Update facing before moving
            controller.FacingDirection = facingDirection;

            // Blocked, switch to collision
            if (!controller.TileMover.CanMoveInDirection(facingDirection))
            {
                controller.SetState(controller.CollisionState);
                return;
            }

            // Safe to move, start walking coroutine
            walkRoutine = controller.StartCoroutine(Walk(facingDirection));
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
        /// Executes tile movement, then determines the next state from updated input.
        /// </summary>
        private IEnumerator Walk(FacingDirection direction)
        {
            Vector2Int move = direction.ToVector2Int();
            Vector3 destination = controller.transform.position +
                new Vector3(move.x * TilemapInfo.CellSize.x, move.y * TilemapInfo.CellSize.y, 0f);

            controller.AnimatorController.UpdateDirection(direction);
            controller.AnimatorController.PlayWalkStep();

            yield return controller.TileMover.MoveToTile(destination, controller.WalkDuration);

            InputDirection nextInputDirection = controller.Input.InputDirection;

            if (nextInputDirection == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
            }
            else
            {
                // Re-check CanMove in Enter()
                controller.SetState(controller.WalkingState);
            }
        }
    }
}
