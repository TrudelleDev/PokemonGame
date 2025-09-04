namespace PokemonGame.MapEntry.Enums
{
    /// <summary>
    /// Identifiers for map entry points used to place the player
    /// at the correct location when loading a new map.
    /// </summary>
    public enum MapEntryID
    {
        /// <summary>
        /// No entry requested. Acts as a safe default.
        /// </summary>
        None = 0,

        /// <summary>
        /// Entrance of Viridian Forest (from the city side).
        /// </summary>
        ViridianForestEntrance,

        /// <summary>
        /// Exit of Viridian Forest (toward the next route).
        /// </summary>
        ViridianForestExit,

        /// <summary>
        /// Entrance inside the Viridian Forest gatehouse.
        /// </summary>
        ViridianForestGatehouseEntrance,

        /// <summary>
        /// Exit inside the Viridian Forest gatehouse.
        /// </summary>
        ViridianForestGatehouseExit
    }
}
