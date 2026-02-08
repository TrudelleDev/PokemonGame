using UnityEngine;

namespace MonsterTamer.Characters.Directions
{
    /// <summary>
    /// Provides helper methods for working with <see cref="FacingDirection"/>.
    /// </summary>
    public static class FacingDirectionExtensions
    {
        private const string InvalidMessage = "Invalid FacingDirection value.";

        /// <summary>
        /// Converts a <see cref="FacingDirection"/> into a 2D integer unit vector.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="direction"/> is not a valid <see cref="FacingDirection"/>.
        /// </exception>
        public static Vector2Int ToVector2Int(this FacingDirection direction)
        {
            return direction switch
            {
                FacingDirection.North => Vector2Int.up,
                FacingDirection.South => Vector2Int.down,
                FacingDirection.West => Vector2Int.left,
                FacingDirection.East => Vector2Int.right,
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, InvalidMessage)
            };
        }

        /// <summary>
        /// Converts an <see cref="FacingDirection"/> value to a corresponding <see cref="InputDirection"/>.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="facing"/> is <see cref="FacingDirection.None"/> or invalid.
        /// </exception>
        public static InputDirection ToInputDirection(this FacingDirection facing)
        {
            return facing switch
            {
                FacingDirection.North => InputDirection.Up,
                FacingDirection.South => InputDirection.Down,
                FacingDirection.West => InputDirection.Left,
                FacingDirection.East => InputDirection.Right,
                _ => throw new System.ArgumentOutOfRangeException(nameof(facing), facing, InvalidMessage)
            };
        }

        /// <summary>
        /// Gets the opposite of a given <see cref="FacingDirection"/>.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="direction"/> is not a valid <see cref="FacingDirection"/>.
        /// </exception>
        public static FacingDirection Opposite(this FacingDirection direction)
        {
            return direction switch
            {
                FacingDirection.North => FacingDirection.South,
                FacingDirection.South => FacingDirection.North,
                FacingDirection.West => FacingDirection.East,
                FacingDirection.East => FacingDirection.West,
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, InvalidMessage)
            };
        }
    }
}
