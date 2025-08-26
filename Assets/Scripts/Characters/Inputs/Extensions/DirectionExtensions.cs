using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Inputs.Enums;
using UnityEngine;

namespace PokemonGame.Characters.Inputs.Extensions
{
    /// <summary>
    /// Provides utility methods for converting between <see cref="InputDirection"/> and vectors.
    /// Useful for handling directional input and movement logic.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Converts a <see cref="InputDirection"/> value to a corresponding <see cref="Vector2Int"/>.
        /// </summary>
        public static Vector2Int ToVector2Int(this InputDirection direction)
        {
            return direction switch
            {
                InputDirection.Up => Vector2Int.up,
                InputDirection.Down => Vector2Int.down,
                InputDirection.Left => Vector2Int.left,
                InputDirection.Right => Vector2Int.right,
                _ => Vector2Int.zero
            };
        }

        /// <summary>
        /// Converts a <see cref="InputDirection"/> value to a corresponding <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 ToVector2(this InputDirection direction)
        {
            return direction switch
            {
                InputDirection.Up => Vector2.up,
                InputDirection.Down => Vector2.down,
                InputDirection.Left => Vector2.left,
                InputDirection.Right => Vector2.right,
                _ => Vector2.zero
            };
        }

        
        /// <summary>
        /// Converts an input direction to a facing direction.
        /// Ignores InputDirection.None by defaulting to Down.
        /// </summary>
        public static FacingDirection ToFacingDirection(this InputDirection input)
        {
            return input switch
            {
                InputDirection.Up => FacingDirection.North,
                InputDirection.Down => FacingDirection.South,
                InputDirection.Left => FacingDirection.West,
                InputDirection.Right => FacingDirection.East,
                _ => FacingDirection.South // fallback for None or unexpected values
            };
        }
        
    }
}
