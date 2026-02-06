namespace PokemonGame.Move
{
    /// <summary>
    /// Represents an in-battle instance of a Monster move,
    /// tracking remaining Power Points (PP) and linking to its definition.
    /// </summary>
    internal sealed class MoveInstance
    {
        /// <summary>
        /// Remaining Power Points for this move.
        /// Decreases each time the move is used.
        /// </summary>
        internal int PowerPointRemaining { get; private set; }

        /// <summary>
        /// Reference to the move's definition (stats, type, effect, etc.).
        /// </summary>
        internal MoveDefinition Definition { get; private set; }

        /// <summary>
        /// Creates a new move instance from a definition and sets full PP.
        /// </summary>
        /// <param name="definition">The move definition.</param>
        internal MoveInstance(MoveDefinition definition)
        {
            Definition = definition;
            PowerPointRemaining = definition.MoveInfo.PowerPoint;
        }

        /// <summary>
        /// Consumes one Power Point (PP) when the move is used.
        /// </summary>
        internal void UsePP()
        {
            PowerPointRemaining--;
        }
    }
}
