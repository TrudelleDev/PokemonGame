using System.Collections;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Handles walking behavior, moving the character one tile in the input direction.
    /// Transitions to the next state after movement completes based on updated input.
    /// </summary>
    public class CharacterWalkingState : ICharacterState
    {
        private const float WalkingAnimationLenght = 0.25f;
        private readonly CharacterStateController controller;
        private Coroutine walkRoutine;

        public CharacterWalkingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Called when entering the walking state. Begins tile movement.
        /// </summary>
        public void Enter()
        {
            walkRoutine = controller.StartCoroutine(Walk());
        }

        /// <summary>
        /// No per-frame logic needed during walking.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Called when exiting the walking state. Stops the walk coroutine if it's running.
        /// </summary>
        public void Exit()
        {
            if (walkRoutine != null)
            {
                controller.StopCoroutine(walkRoutine);
                walkRoutine = null;
            }
        }

        /// <summary>
        /// Moves the character and determines the next state based on updated input.
        /// </summary>
        private IEnumerator Walk()
        {
            Direction direction = controller.Input.CurrentDirection;
            Vector2Int move = direction.ToVector();
            Vector3 destination = controller.transform.position + new Vector3(move.x * TilemapInfo.CellSize.x, move.y * TilemapInfo.CellSize.y, 0f);

            controller.FacingDirection = direction;
            controller.AnimatorController.PlayWalkStep();

            yield return controller.TileMover.MoveToTile(destination, WalkingAnimationLenght);

            Direction newDirection = controller.Input.CurrentDirection;

            if (newDirection == Direction.None)
            {
                controller.SetState(controller.IdleState);
            }
            else if (controller.TileMover.CanMoveInDirection(newDirection))
            {
                controller.SetState(controller.WalkingState);
            }
            else
            {
                controller.SetState(controller.CollisionState);
            }
        }
    }
}
