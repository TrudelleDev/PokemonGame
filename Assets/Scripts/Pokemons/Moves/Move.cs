namespace PokemonGame.Pokemons.Moves
{
    public class Move
    {
        public int PowerPointRemaining { get; private set; }

        public MoveData Data { get; private set; }

        public Move(MoveData data)
        {
            Data = data;
            PowerPointRemaining = data.PowerPoint;
        }
    }
}
