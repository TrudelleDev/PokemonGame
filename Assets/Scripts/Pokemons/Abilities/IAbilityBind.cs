namespace PokemonGame.Pokemons.Abilities
{
    /// <summary>
    /// Interface for UI components that can bind and display data from an <see cref="Ability"/>.
    /// </summary>
    internal interface IAbilityBind
    {
        /// <summary>
        /// Binds the given ability data to the implementing component.
        /// </summary>
        /// <param name="ability">The ability to bind.</param>
        void Bind(Ability ability);
    }
}
