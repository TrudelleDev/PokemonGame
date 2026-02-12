namespace MonsterTamer.Monster.Components
{
    /// <summary>
    /// Stores runtime metadata for a monster instance,
    /// including ownership, encounter origin, and a unique ID.
    /// </summary>
    internal sealed class MetadataComponent
    {
        internal string OwnerName { get; set; }
        internal string EncounterLocation { get; set; }

        /// <summary>
        /// Unique identifier assigned at creation.
        /// </summary>
        internal string Id { get; }

        private static readonly IDGenerator IdGenerator = new(1000, 9999);

        internal MetadataComponent()
        {
            Id = IdGenerator.GetID();
        }
    }
}
