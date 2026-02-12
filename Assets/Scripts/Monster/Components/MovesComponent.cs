using MonsterTamer.Move;

namespace MonsterTamer.Monster.Components
{
    /// <summary>
    /// Holds the runtime move slots for a monster instance.
    /// Creates move instances from their definitions.
    /// </summary>
    internal sealed class MovesComponent
    {
        /// <summary>
        /// Runtime move slots. Null entries represent empty slots.
        /// </summary>
        internal MoveInstance[] Moves { get; }

        internal MovesComponent(MoveDefinition[] moveDefinitions)
        {
            Moves = new MoveInstance[moveDefinitions.Length];

            for (int i = 0; i < moveDefinitions.Length; i++)
            {
                MoveDefinition definition = moveDefinitions[i];

                if (definition == null)
                {
                    continue;
                }

                Moves[i] = new MoveInstance(definition);
            }
        }
    }
}
