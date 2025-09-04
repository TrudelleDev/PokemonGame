using PokemonGame.Characters.Core;

namespace PokemonGame.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be interacted with by a <see cref="Character"/>.
    /// Typical examples include NPCs, signs, or items in the overworld.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Performs an interaction for the specified <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The character initiating the interaction.</param>
        void Interact(Character character);
    }
}
