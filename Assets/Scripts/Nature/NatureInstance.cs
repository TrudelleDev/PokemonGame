namespace MonsterTamer.Nature
{
    /// <summary>
    /// Represents a runtime instance of a Pokemon nature.
    /// </summary>
    public class NatureInstance
    {
        public NatureDefinition Definition { get; private set; }

        public NatureInstance(NatureDefinition definition)
        {
            Definition = definition;
        }
    }
}
