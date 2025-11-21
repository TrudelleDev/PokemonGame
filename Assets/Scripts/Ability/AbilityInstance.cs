namespace PokemonGame.Ability
{
    /// <summary>
    /// Represents a runetime Pokemon ability instance.
    /// </summary>
    public class AbilityInstance
    {
        public AbilityDefinition Definition { get; private set; }

        public AbilityInstance(AbilityDefinition definition)
        {
            Definition = definition;
        }
    }
}
