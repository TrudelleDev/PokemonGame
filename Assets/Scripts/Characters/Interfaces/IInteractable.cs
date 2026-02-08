namespace MonsterTamer.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be interacted with by a <see cref="Character"/>.
    /// Typical examples include NPCs, signs, or items in the overworld.
    /// </summary>
    internal interface IInteractable
    {
        /// <summary>
        /// Called when a <see cref="Character"/> interacts with this object.
        /// </summary>
        /// <param name="character">The character initiating the interaction.</param>
        void Interact(Character character);
    }
}
