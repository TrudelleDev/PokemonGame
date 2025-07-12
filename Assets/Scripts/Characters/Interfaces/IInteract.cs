namespace PokemonGame.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be interacted with by a character, such as signs, NPCs, or items. 
    /// </summary>
    public interface IInteract
    {
        /// <summary>
        /// Called when a character interacts with this object.
        /// </summary>
        /// <param name="character">The character initiating the interaction.</param>
        void Interact(Character character);
    }
}
