using PokemonGame.Characters.Inputs.Enums;
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
        public InputDirection InputDirection { get; protected set; }

        /// <summary>
        /// Updates input each frame.
        /// </summary>
        protected abstract void Update();
    }
}
