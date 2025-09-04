using PokemonGame.Characters.Direction;
using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Base class for handling character input.
    /// </summary>
    public abstract class CharacterInput : MonoBehaviour
    {
        /// <summary>
        /// Current movement direction, set by derived classes.
        /// </summary>
        public InputDirection CurrentDirection { get; protected set; }

        /// <summary>
        /// True only on the frame the interact button was pressed.
        /// </summary>
        public bool InteractPressed { get; protected set; }

        private void Update()
        {
            ReadInput();
        }

        /// <summary>
        /// Reads input values each frame. Must be implemented by subclasses.
        /// </summary>
        protected abstract void ReadInput();
    }
}
