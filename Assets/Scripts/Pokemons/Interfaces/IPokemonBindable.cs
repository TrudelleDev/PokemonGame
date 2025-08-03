namespace PokemonGame.Pokemons.Interfaces
{
    /// <summary>
    /// Binds a Pokémon instance.
    /// </summary>
    public interface IPokemonBindable
    {
        /// <summary>
        /// Binds the given Pokémon instance.
        /// </summary>
        /// <param name="pokemon">The instance to bind.</param>
        void Bind(Pokemon pokemon);
    }
}
