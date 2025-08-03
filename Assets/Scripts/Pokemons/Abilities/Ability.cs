namespace PokemonGame.Pokemons.Abilities
{
    /// <summary>
    /// Represents a runtime instance of a Pokémon ability.
    /// </summary>
    public class Ability
    {
        /// <summary>
        /// The ability definition associated with this instance.
        /// </summary>
        public AbilityDefinition Definition { get; }

        /// <summary>
        /// Creates a new ability instance from the given definition.
        /// </summary>
        /// <param name="definition">The ability definition to use.</param>
        public Ability(AbilityDefinition definition)
        {
            Definition = definition;
        }
    }
}
