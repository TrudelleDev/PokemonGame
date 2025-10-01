namespace PokemonGame.Items
{
    /// <summary>
    /// Result of attempting to use an item.
    /// Contains both whether it was actually used and the dialogue messages.
    /// </summary>
    public struct ItemUseResult
    {
        public bool Used { get; }
        public string[] Messages { get; }

        public ItemUseResult(bool used, string[] messages)
        {
            Used = used;
            Messages = messages;
        }
    }
}
