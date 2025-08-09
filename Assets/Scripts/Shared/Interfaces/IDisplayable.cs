using UnityEngine;

namespace PokemonGame.Shared.Interfaces
{
    /// <summary>
    /// Represents something that can be displayed in the UI with an icon and description.
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// The textual description to display.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The icon sprite to display.
        /// </summary>
        Sprite Icon { get; }
    }
}
