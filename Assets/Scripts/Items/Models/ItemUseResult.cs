namespace PokemonGame.Items.Models
{
    /// <summary>
    /// Represents the outcome of using an item.
    /// Indicates whether the item was successfully used and provides
    /// any dialogue messages to display to the player.
    /// </summary>
    public readonly struct ItemUseResult
    {
        /// <summary>
        /// True if the item was successfully used; false otherwise.
        /// </summary>
        public bool Used { get; }

        /// <summary>
        /// Dialogue messages generated when the item was used.
        /// </summary>
        public string[] Messages { get; }

        /// <summary>
        /// Creates a new ItemUseResult.
        /// </summary>
        /// <param name="used">Whether the item was successfully used.</param>
        /// <param name="messages">Dialogue messages to show to the player.</param>
        public ItemUseResult(bool used, string[] messages)
        {
            Used = used;
            Messages = messages;
        }
    }
}
