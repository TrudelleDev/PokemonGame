namespace PokemonGame.Characters.Interfaces
{
    /// <summary>
    /// Defines an object that can be triggered by a <see cref="Character"/>.
    /// Implementations specify what happens when the trigger is activated.
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// Activates the trigger's effect for the specified <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The character interacting with or activating the trigger.</param>
        void Trigger(Character character);
    }
}
