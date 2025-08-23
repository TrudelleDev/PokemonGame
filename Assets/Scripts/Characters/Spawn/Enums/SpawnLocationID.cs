namespace PokemonGame.Characters.Spawn.Enums
{
    /// <summary>
    /// Identifiers for player spawn locations across different scenes.
    /// Used by <see cref="SpawnLocationManager"/> and scene transition triggers
    /// to position the player at the correct location after loading a new scene.
    /// </summary>
    public enum SpawnLocationID
    {
        /// <summary>
        /// Indicates no spawn location was requested.
        /// Used as a safe default; spawner may fall back to a scene’s default spawn point.
        /// </summary>
        None = 0,

        /// <summary>
        /// Entrance location of Viridian Forest (from the city side).
        /// </summary>
        ViridianForestEntrance,

        /// <summary>
        /// Exit location of Viridian Forest (deep forest to the next route).
        /// </summary>
        ViridianForestExit,

        /// <summary>
        /// Entrance location inside the Viridian Forest gatehouse.
        /// </summary>
        ViridianForestGatehouseEntrance,

        /// <summary>
        /// Exit location inside the Viridian Forest gatehouse.
        /// </summary>
        ViridianForestGatehouseExit
    }
}
