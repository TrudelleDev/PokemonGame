using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Handles user input for character movement using the keyboard (WASD or Arrow Keys).
    /// Updates the character's direction based on the user's input.
    /// </summary>
    public class UserInput : CharacterInput
    {
        /// <summary>
        /// Called every frame to process input and update the character's movement direction.
        /// </summary>
        protected override void Update()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                CurrentDirection = Direction.Up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                CurrentDirection = Direction.Down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                CurrentDirection = Direction.Left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                CurrentDirection = Direction.Right;
            }
            else
            {
                CurrentDirection = Direction.None;
            }
        }
    }
}
