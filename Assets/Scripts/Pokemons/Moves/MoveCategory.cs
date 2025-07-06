namespace PokemonGame.Pokemons.Moves
{
    /// <summary>
    /// Represents the category of a Pokémon move.
    /// </summary>
    public enum MoveCategory
    {
        /// <summary>
        /// Physical moves use the user's Attack stat and the target's Defense stat.
        /// </summary>
        Physical,

        /// <summary>
        /// Special moves use the user's Special Attack and the target's Special Defense.
        /// </summary>
        Special,

        /// <summary>
        /// Status moves do not deal damage, instead inflict conditions or change stats.
        /// </summary>
        Status
    }
}
