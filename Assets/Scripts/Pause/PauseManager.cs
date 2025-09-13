namespace PokemonGame.Pause
{
    /// <summary>
    /// Global pause manager for the game.
    /// Tracks whether the game is currently paused and provides
    /// a central point for other systems to query or update pause state.
    /// </summary>
    public static class PauseManager
    {
        public static bool IsPaused { get; private set; } = false;

        /// <summary>
        /// Updates the pause state of the game.
        /// Does nothing if the state is already set to the requested value.
        /// </summary>
        /// <param name="paused">If true, sets the game to paused; otherwise unpauses.</param>
        public static void SetPaused(bool paused)
        {
            if (IsPaused == paused)
            {
                return;
            }

            IsPaused = paused;
        }
    }
}
