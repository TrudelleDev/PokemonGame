namespace PokemonGame.Party.Enums
{
    /// <summary>
    /// Defines the context in which the player is interacting with the party menu.
    /// Determines how selections are handled (e.g., normal selection, swapping, or item use).
    /// </summary>
    internal enum PartySelectionMode
    {
        Overworld,
        Battle,
        UseItem
    }
}
