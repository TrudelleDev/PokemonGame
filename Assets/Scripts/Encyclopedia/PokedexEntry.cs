using PokemonGame.Pokemons.Data;

namespace PokemonGame.Encyclopedia
{
    public class PokedexEntry
    {
        public bool IsOwn { get; private set; }
        public PokemonData Data { get; private set; }

        public PokedexEntry(bool isOwn, PokemonData data)
        {
            IsOwn = isOwn;
            Data = data;
        }
    }
}
