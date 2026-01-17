using System;
using System.Collections;
using PokemonGame.Characters.Directions;
using UnityEngine;

namespace PokemonGame.Tile
{
    /// <summary>
    /// Provides smooth, grid-based movement and directional path checking.
    /// Can be used by any object that moves in discrete steps, such as characters, NPCs, or tiles.
    /// Integrates with <see cref="TileRaycaster"/> to validate movement paths.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TileRaycaster))]
    public class TileMover : MonoBehaviour
    {
        private TileRaycaster raycaster;

        /// <summary>
        /// Event triggered when movement begins.
        /// </summary>
        public event Action OnMoveStart;

        /// <summary>
        /// Event triggered when movement is completed.
        /// </summary>
        public event Action MoveCompleted;

        /// <summary>
        /// Indicates whether the object is currently moving.
        /// </summary>
        public bool IsMoving { get; private set; }

        private void Awake()
        {
            raycaster = GetComponent<TileRaycaster>();
        }

        /// <summary>
        /// Moves the object smoothly to the specified world position over a given duration.
        /// Triggers movement-related events and interpolates between positions.
        /// </summary>
        /// <param name="destination">Target world position to move to.</param>
        /// <param name="animationDuration">Duration of the movement animation in seconds.</param>
        /// <returns>An enumerator used for coroutine execution.</returns>
        public IEnumerator MoveToTile(Vector3 destination, float animationDuration)
        {
            Vector3 startPosition = transform.position;
            IsMoving = true;
            float elapsed = 0f;

            OnMoveStart?.Invoke();

            while (elapsed < animationDuration)
            {
                float t = Mathf.Clamp01(elapsed / animationDuration);
                transform.position = Vector3.Lerp(startPosition, destination, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = destination;
            IsMoving = false;
            MoveCompleted?.Invoke();
        }

        /// <summary>
        /// Checks whether the path is clear in the given direction using raycasting.
        /// </summary>
        /// <param name="direction">Direction to test for obstacles.</param>
        /// <returns>True if the path is clear; otherwise, false.</returns>
        public bool CanMoveInDirection(FacingDirection direction)
        {
            return raycaster.IsPathClear(direction);
        }
    }
}
