using System.Collections;
using PokemonGame.Characters.Core;
using PokemonGame.Characters.Direction;
using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Walking state: the character moves one tile in the current facing direction.
    /// Triggers are checked before movement, collisions are handled, and movement
    /// is executed via coroutine with animation.
    /// </summary>
    public class CharacterWalkingState : ICharacterState
    {
        private readonly CharacterStateController controller;
        private readonly CharacterTriggerHandler triggerHandler;
        private Coroutine walkRoutine;

        /// <summary>
        /// Creates a new walking state for the given controller.
        /// Caches the <see cref="CharacterTriggerHandler"/> if present.
        /// </summary>
        /// <param name="controller">The character controller that owns this state.</param>
        public CharacterWalkingState(CharacterStateController controller)
        {
            this.controller = controller;
            triggerHandler = controller.GetComponent<CharacterTriggerHandler>();
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
            if (triggerHandler != null && triggerHandler.CheckForTriggers(input.ToVector2Int()))
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

        /// <summary>
        /// No update logic required for walking state.
        /// </summary>
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
