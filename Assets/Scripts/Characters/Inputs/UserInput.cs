using UnityEngine;
using PokemonGame.Characters.Inputs.Enums;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Handles player input for character movement using WASD or Arrow Keys.
    /// </summary>
    public class UserInput : CharacterInput
    {
        protected override void Update()
        {
            // Check vertical input
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                InputDirection = InputDirection.Up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                InputDirection = InputDirection.Down;
            }
            // Check horizontal input
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                InputDirection = InputDirection.Left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                InputDirection = InputDirection.Right;
            }
            // No key pressed go to idle
            else
            {
                InputDirection = InputDirection.None;
            }
        }
    }
}
