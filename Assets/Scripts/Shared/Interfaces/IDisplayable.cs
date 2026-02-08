using UnityEngine;

namespace PokemonGame.Shared.Interfaces
{
    /// <summary>
    /// Represents something that can be displayed in the UI with an icon and description.
    /// </summary>
    internal interface IDisplayable
    {
        /// <summary>
        /// Gets the display name shown in the UI.
        /// </summary>
        string DisplayName { get; }

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
