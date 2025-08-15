namespace PokemonGame.Transitions.Enums
{
    /// <summary>
    /// Defines the available transition effect types for views and scene changes.
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// No transition effect; content appears instantly.
        /// </summary>
        None = 0,

        /// <summary>
        /// Simple fade transition that adjusts alpha transparency over time.
        /// </summary>
        AlphaFade,

        /// <summary>
        /// Fade transition that uses a mask texture for custom shapes or patterns.
        /// </summary>
        MaskedFade
    }
}
