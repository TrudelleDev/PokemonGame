namespace PokemonGame.Move
{
    /// <summary>
    /// Represents a Pokémon move instance that uses a definition as its static data.
    /// </summary>
    public class MoveInstance
    {
        /// <summary>
        /// The number of remaining Power Points (PP) for this move.
        /// </summary>
        public int PowerPointRemaining { get; private set; }

        /// <summary>
        /// The static data definition for this move.
        /// </summary>
        public MoveDefinition Definition { get; private set; }

        /// <summary>
        /// Creates a new instance of a move using the given definition.
        /// </summary>
        /// <param name="definition">The move's static definition.</param>
        public MoveInstance(MoveDefinition definition)
        {
            Definition = definition;
            PowerPointRemaining = definition.MoveInfo.PowerPoint;
        }
    }
}
