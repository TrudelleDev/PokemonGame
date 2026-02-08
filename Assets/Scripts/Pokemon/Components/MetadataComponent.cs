namespace MonsterTamer.Pokemon.Components
{
    public class MetadataComponent
    {
        public string OwnerName { get; set; } = "RED";
        public string LocationEncounter { get; set; } = "Pallet Town";
        public string ID { get; private set; }

        private static readonly IDGenerator idGenerator = new IDGenerator(1000, 9999);

        public MetadataComponent()
        {
            ID = idGenerator.GetID();
        }
    }
}
