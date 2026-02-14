using MonsterTamer.Characters.Core;

namespace MonsterTamer.Characters.Interfaces
{
    /// <summary>
    /// Defines an object that can be interacted with by a character.
    /// </summary>
    internal interface IInteractable
    {
        /// <summary>
        /// Called when a character interacts with this object.
        /// </summary>
        void Interact(Character initiator);
    }
}
