namespace PokemonGame.Pokemons.Natures
{
    public class Nature
    {
        public NatureData Data { get; private set; }

        public Nature(NatureData data)
        {
            Data = data;
        }
    }
}
