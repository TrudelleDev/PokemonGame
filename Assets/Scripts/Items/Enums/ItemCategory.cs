namespace PokemonGame.Items.Enums
{
    /// <summary>
    /// Defines the category or pocket an item belongs to in the player's inventory.
    /// Used for organizing items and determining where they are stored.
    /// </summary>
    public enum ItemCategory
    {
        /// <summary>
        /// No category assigned.
        /// </summary>
        None,

        /// <summary>
        /// Standard items such as healing or battle-use items.
        /// </summary>
        General,

        /// <summary>
        /// Important items that cannot be discarded or sold.
        /// </summary>
        KeyItem,

        /// <summary>
        /// Items used to catch Pokémon.
        /// </summary>
        Pokeball
    }
}
