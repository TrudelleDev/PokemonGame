using PokemonGame;
using PokemonGame.Encyclopedia.UI;
using UnityEngine;

namespace PokemonGame.Views
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
