using PokemonGame.Utilities;

namespace PokemonGame.Characters.Player
{
    /// <summary>
    /// Stores a global reference to the active player character.
    /// </summary>
    internal static class PlayerRegistry
    {
        /// <summary>
        /// The currently registered player character.
        /// Null when no player is active.
        /// </summary>
        public static Character Player { get; private set; }

        /// <summary>
        /// Registers the given character as the active player.
        /// </summary>
        /// <param name="player">The player character to register.</param>
        public static void Register(Character player)
        {
            if (Player != null)
            {
                Log.Warning(nameof(PlayerRegistry), "Player already registered. Overwriting.");
            }

            Player = player;
        }

        /// <summary>
        /// Unregisters the player if it matches the currently registered instance.
        /// </summary>
        /// <param name="player">The player character being unregistered.</param>
        public static void Unregister(Character player)
        {
            if (Player == player)
            {
                Player = null;
            }
        }
    }
}
