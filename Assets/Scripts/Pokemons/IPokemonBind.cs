namespace PokemonGame.Pokemons
{
    /// <summary>
    /// Defines a contract for binding a Pokémon from UI or data.
    /// </summary>
    public interface IPokemonBind
    {
        /// <summary>
        /// Binds data or references from the Pokémon UI element.
        /// </summary>
        void Bind(Pokemon pokemon);
    }
}
