using System.Collections;
using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Utilities;
using UnityEngine;

namespace MonsterTamer.Characters.States
{
    /// <summary>
    /// Walking state: the character moves one tile in the current facing direction.
    /// Triggers are checked before movement, collisions are handled, and movement
    /// is executed via coroutine with animation.
    /// </summary>
    internal sealed class CharacterWalkingState : ICharacterState
    {
        private readonly CharacterStateController controller;
        private Coroutine walkRoutine;

        public CharacterWalkingState(CharacterStateController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Enters the walking state and attempts to move one tile.
        /// Handles idle fallback, triggers, and collisions before starting movement.
        /// </summary>
        public void Enter()
        {
            InputDirection input = controller.Input.CurrentDirection;

            if (input == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
                return;
            }

            FacingDirection facing = input.ToFacingDirection();
            controller.FacingDirection = facing;

            // Check triggers BEFORE walking
            if (controller.TriggerHandler != null && controller.TriggerHandler.TryTrigger(input.ToVector2Int()))
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

        /// <summary>
        /// Exits the walking state and stops any active walk coroutine.
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
        /// Executes tile movement, then decides the next state based on new input.
        /// </summary>
        /// <param name="direction">The direction to walk in.</param>
        private IEnumerator Walk(FacingDirection direction)
        {
            Vector2Int move = direction.ToVector2Int();
            Vector3 destination = controller.transform.position +
                new Vector3(move.x * TilemapInfo.CellSize.x, move.y * TilemapInfo.CellSize.y, 0f);

            controller.AnimatorController.UpdateDirection(direction);
            controller.AnimatorController.PlayWalkStep();

            yield return controller.TileMover.MoveToTile(destination, controller.WalkDuration);

            // Decide next state after movement
            InputDirection nextInput = controller.Input.CurrentDirection;

            if (nextInput == InputDirection.None)
            {
                controller.SetState(controller.IdleState);
            }
            else
            {
                controller.SetState(controller.WalkingState); // Will re-run checks in Enter()
            }
        }
    }
}
