using PokemonGame.Abilities.Definition;

namespace PokemonGame.Abilities
{
    /// <summary>
    /// A runtime instance of a Pokémon ability, constructed from a predefined ability definition.
    /// </summary>
    public class Ability
    {
        /// <summary>
        /// The ability definition associated with this instance.
        /// </summary>
        public AbilityDefinition Definition { get; }

        /// <summary>
        /// Initializes a new ability instance from the given definition.
        /// </summary>
        /// <param name="definition">The ability definition to use.</param>
        public Ability(AbilityDefinition definition)
        {
            Definition = definition;
        }
    }
}
