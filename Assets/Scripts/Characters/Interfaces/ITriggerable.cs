using MonsterTamer.Characters.Core;

namespace MonsterTamer.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be triggered by a character.
    /// </summary>
    internal interface ITriggerable
    {
        /// <summary>
        /// Activates the trigger for the given character.
        /// </summary>
        void Trigger(Character initiator);
    }
}
