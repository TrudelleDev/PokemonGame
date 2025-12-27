using PokemonGame.Characters.Core;

namespace PokemonGame.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be triggered by a <see cref="Character"/>.
    /// Implementations define what happens when the trigger is activated.
    /// </summary>
    internal interface ITriggerable
    {
        /// <summary>
        /// Activates the trigger's effect for the specified <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The character interacting with or activating the trigger.</param>
        void Trigger(Character character);
    }
}
