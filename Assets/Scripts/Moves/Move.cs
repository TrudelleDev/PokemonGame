using PokemonGame.Moves.Definition;

namespace PokemonGame.Moves
{
    /// <summary>
    /// Represents a Pokémon move instance that uses a definition as its static data.
    /// </summary>
    public class Move
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
        public Move(MoveDefinition definition)
        {
            Definition = definition;
            PowerPointRemaining = definition.PowerPoint;
        }
    }
}
