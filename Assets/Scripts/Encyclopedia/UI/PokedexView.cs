using PokemonGame;
using UnityEngine;

namespace PokemonGame.Encyclopedia.UI
{
    public class PokedexView : View
    {
        [SerializeField] private PokedexContent pokedexContent;

        public override void Initialize()
        {
            pokedexContent.Initialize();
        }
    }

}
