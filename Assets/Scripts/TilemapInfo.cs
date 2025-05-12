using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Stores global tilemap configuration information used across the game.
    /// Provides shared access to values like cell size for grid-based calculations.
    /// </summary>
    public static class TilemapInfo
    {
        /// <summary>
        /// The size of a single tile cell in world units.
        /// This value should be initialized during game setup.
        /// </summary>
        public static Vector3 CellSize;
    }
}
