using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    public abstract class CharacterInput : MonoBehaviour
    {
        protected Vector3 inputDirection = Vector3.zero;

        public Vector3 InputDirection => inputDirection;

        public abstract void HandleInputs();
    }
}
