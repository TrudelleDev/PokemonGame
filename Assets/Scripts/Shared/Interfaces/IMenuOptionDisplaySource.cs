namespace PokemonGame.Shared.Interfaces
{
    /// <summary>
    /// Provides a displayable object for a menu option.
    /// </summary>
    public interface IMenuOptionDisplaySource
    {
        /// <summary>
        /// The displayable content associated with this menu option.
        /// </summary>
        IDisplayable Displayable { get; }
    }
}
