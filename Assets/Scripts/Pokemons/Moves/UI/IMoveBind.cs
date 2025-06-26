namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// Interface for UI components that can bind and display information about a Pokémon move.
    /// </summary>
    public interface IMoveBind
    {
        /// <summary>
        /// Binds the given move data to the implementing UI component.
        /// </summary>
        /// <param name="move">The move to bind and display.</param>
        void Bind(Move move);
    }
}
