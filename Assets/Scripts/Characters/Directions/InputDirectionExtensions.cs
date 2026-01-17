using UnityEngine;

namespace PokemonGame.Characters.Directions
{
    /// <summary>
    /// Provides utility methods for converting between <see cref="InputDirection"/> 
    /// and related types. Useful for handling directional input and movement logic.
    /// </summary>
    public static class InputDirectionExtensions
    {
        private const string InvalidMessage = "Invalid InputDirection value.";

        /// <summary>
        /// Converts an <see cref="InputDirection"/> value to a corresponding <see cref="Vector2Int"/>.
        /// Returns <see cref="Vector2Int.zero"/> if the value is <see cref="InputDirection.None"/>.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="direction"/> is not a valid <see cref="InputDirection"/>.
        /// </exception>
        public static Vector2Int ToVector2Int(this InputDirection direction)
        {
            return direction switch
            {
                InputDirection.Up => Vector2Int.up,
                InputDirection.Down => Vector2Int.down,
                InputDirection.Left => Vector2Int.left,
                InputDirection.Right => Vector2Int.right,
                InputDirection.None => Vector2Int.zero,
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, InvalidMessage)
            };
        }

        /// <summary>
        /// Converts an <see cref="InputDirection"/> value to a corresponding <see cref="FacingDirection"/>.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="input"/> is <see cref="InputDirection.None"/> or invalid.
        /// </exception>
        public static FacingDirection ToFacingDirection(this InputDirection input)
        {
            return input switch
            {
                InputDirection.Up => FacingDirection.North,
                InputDirection.Down => FacingDirection.South,
                InputDirection.Left => FacingDirection.West,
                InputDirection.Right => FacingDirection.East,
                _ => throw new System.ArgumentOutOfRangeException(nameof(input), input, InvalidMessage)
            };
        }
    }
}
