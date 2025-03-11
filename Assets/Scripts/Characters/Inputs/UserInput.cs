using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    public class UserInput : CharacterInput
    {
        public override void HandleInputs()
        {
            if (inputDirection.y == 0) // Disable diagolal movement
            {
                inputDirection.x = Input.GetAxisRaw("Horizontal");
            }
            if (inputDirection.x == 0)  // Disable diagolal movement
            {
                inputDirection.y = Input.GetAxisRaw("Vertical");
            }
        }
    }
}
