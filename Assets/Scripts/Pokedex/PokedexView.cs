using PokemonGame;
using UnityEngine;

public class PokedexView : View
{
    [SerializeField] private PokedexContent pokedexContent;

    public override void Initialize()
    {
        pokedexContent.Initialize();
    }
}
