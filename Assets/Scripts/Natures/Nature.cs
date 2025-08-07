namespace PokemonGame.Pokemons.Natures
{
    /// <summary>
    /// Represents a Pokémon nature using a definition.
    /// </summary>
    public class Nature
    {
        /// <summary>
        /// The associated nature definition.
        /// </summary>
        public NatureDefinition Definition { get; private set; }

        /// <summary>
        /// Creates a new nature instance.
        /// </summary>
        /// <param name="definition">The nature definition.</param>
        public Nature(NatureDefinition definition)
        {
            Definition = definition;
        }
    }
}
