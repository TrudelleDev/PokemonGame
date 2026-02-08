using MonsterTamer.Move;

namespace MonsterTamer.Pokemon.Components
{
    internal class MovesComponent
    {
        public MoveInstance[] Moves { get; private set; }

        public MovesComponent(MoveDefinition[] moveDefinitions)
        {
            Moves = new MoveInstance[moveDefinitions.Length];
            for (int i = 0; i < moveDefinitions.Length; i++)
            {
                Moves[i] = moveDefinitions[i] != null ? new MoveInstance(moveDefinitions[i]) : null;
            }
        }
    }
}
