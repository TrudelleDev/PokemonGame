using MonsterTamer.Characters.Config;
using MonsterTamer.Characters.Directions;
using UnityEngine;

namespace MonsterTamer.Characters.Player
{
    /// <summary>
    /// Handles player input for character movement using WASD or Arrow Keys.
    /// </summary>
    public sealed class PlayerInput : CharacterInput
    {
        protected override void ReadInput()
        {
            // Check vertical input
            if (Input.GetKey(KeyBinds.Up))
            {
                CurrentDirection = InputDirection.Up;
            }
            else if (Input.GetKey(KeyBinds.Down))
            {
                CurrentDirection = InputDirection.Down;
            }
            // Check horizontal input
            else if (Input.GetKey(KeyBinds.Left))
            {
                CurrentDirection = InputDirection.Left;
            }
            else if (Input.GetKey(KeyBinds.Right))
            {
                CurrentDirection = InputDirection.Right;
            }
            // No key pressed go to idle
            else
            {
                CurrentDirection = InputDirection.None;
            }

            // --- Interaction ---
            InteractPressed = Input.GetKeyDown(KeyBinds.Interact);
        }
    }
}
