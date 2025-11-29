namespace PokemonGame.Move
{
    public class MoveInstance
    {
        public int PowerPointRemaining { get; private set; }

        public MoveDefinition Definition { get; private set; }

        public MoveInstance(MoveDefinition definition)
        {
            Definition = definition;
            PowerPointRemaining = definition.MoveInfo.PowerPoint;
        }

        public void UsePP()
        {
            PowerPointRemaining--;
        }
    }
}
