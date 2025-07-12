using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Provides utility methods for converting between <see cref="Direction"/> and <see cref="Vector2Int"/>.
    /// Useful for handling directional input and movement logic.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Converts a <see cref="Direction"/> value to a corresponding <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>A Vector2Int representing the direction vector.</returns>
        public static Vector2Int ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2Int.up;
                case Direction.Down:
                    return Vector2Int.down;
                case Direction.Left:
                    return Vector2Int.left;
                case Direction.Right:
                    return Vector2Int.right;
                default:
                    return Vector2Int.zero;
            }
        }

        /// <summary>
        /// Converts a <see cref="Vector2Int"/> to a corresponding <see cref="Direction"/>.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <returns>The matching Direction, or <see cref="Direction.None"/> if no match is found.</returns>
        public static Direction FromVector(Vector2Int vector)
        {
            if (vector == Vector2Int.up) return Direction.Up;
            if (vector == Vector2Int.down) return Direction.Down;
            if (vector == Vector2Int.left) return Direction.Left;
            if (vector == Vector2Int.right) return Direction.Right;

            return Direction.None;
        }

        /// <summary>
        /// Converts a <see cref="Direction"/> value to a corresponding <see cref="Vector2"/>.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>A Vector2 representing the direction vector.</returns>
        public static Vector2 ToVector2(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.up;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }
    }
}
