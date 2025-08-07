namespace PokemonGame.Moves.Enums
{
    /// <summary>
    /// Defines the category of a Pokémon move.
    /// </summary>
    public enum MoveCategory
    {
        /// <summary>
        /// Physical moves use the user's Attack stat and the target's Defense stat.
        /// </summary>
        Physical,

        /// <summary>
        /// Special moves use the user's Special Attack and the target's Special Defense stat.
        /// </summary>
        Special,

        /// <summary>
        /// Status moves do not deal direct damage. They may inflict conditions or alter stats.
        /// </summary>
        Status
    }
}
