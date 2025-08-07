using PokemonGame.Abilities.Definition;

namespace PokemonGame.Abilities
{
    /// <summary>
    /// Represents a Pokémon ability using a definition.
    /// </summary>
    public class Ability
    {
        /// <summary>
        /// The associated ability definition.
        /// </summary>
        public AbilityDefinition Definition { get; }

        /// <summary>
        /// Creates a new ability instance.
        /// </summary>
        /// <param name="definition">The ability definition.</param>
        public Ability(AbilityDefinition definition)
        {
            Definition = definition;
        }
    }
}
