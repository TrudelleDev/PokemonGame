using PokemonGame.Characters.Core;

namespace PokemonGame.Characters
{

    /// <summary>
    /// Stores a global reference to the current player character.
    /// Allows systems to safely access the player without direct scene dependencies.
    /// </summary>
    internal static class PlayerRegistry
    {
        internal static Character Player { get; private set; }

        internal static void Register(Character player)
        {
            Player = player;
        }

        internal static void Unregister(Character player)
        {
            if (Player == player)
            {
                Player = null;
            }
        }
    }
}
