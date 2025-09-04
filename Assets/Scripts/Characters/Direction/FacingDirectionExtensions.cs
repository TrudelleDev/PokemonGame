using UnityEngine;

namespace PokemonGame.Characters.Direction
{
    /// <summary>
    /// Provides helper methods for working with <see cref="FacingDirection"/>.
    /// </summary>
    public static class FacingDirectionExtensions
    {
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
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, "Invalid FacingDirection value.")
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
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, "Invalid FacingDirection value.")
            };
        }
    }
}
