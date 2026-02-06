namespace PokemonGame.Characters.Interfaces
{
    /// <summary>
    /// Represents an object that can be triggered by a <see cref="Character"/>.
    /// Implementations define what happens when the trigger is activated.
    /// </summary>
    internal interface ITriggerable
    {
        /// <summary>
        /// Activates this trigger for the given <see cref="Character"/>.
        /// </summary>
        /// <param name="character">The character interacting with or activating the trigger.</param>
        void Trigger(Character character);
    }
}
