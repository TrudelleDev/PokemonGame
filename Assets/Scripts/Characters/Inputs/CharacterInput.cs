using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Abstract base class for handling character input.
    /// </summary>
    public abstract class CharacterInput : MonoBehaviour
    {
        /// <summary>
        /// Gets the current movement direction of the character.
        /// This is updated based on input in derived classes.
        /// </summary>
        public Direction CurrentDirection { get; protected set; }

        /// <summary>
        /// Called every frame to handle input updates.
        /// </summary>
        protected abstract void Update();
    }
}
