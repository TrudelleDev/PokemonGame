namespace PokemonGame.Abilities.Interfaces
{
    /// <summary>
    /// Binds and displays ability information.
    /// </summary>
    public interface IAbilityBindable
    {
        /// <summary>
        /// Binds the ability to the UI.
        /// </summary>
        /// <param name="ability">The ability to display.</param>
        void Bind(Ability ability);
    }
}
