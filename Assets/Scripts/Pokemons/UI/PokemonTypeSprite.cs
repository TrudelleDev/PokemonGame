using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public class PokemonTypeSprite : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private PokemonType pokemonType;

        public void Bind(Pokemon pokemon)
        {
            Image  image = GetComponent<Image>();

            if(pokemonType == PokemonType.FirstType)
            {
                image.sprite = pokemon.Data.Types.FirstType.Sprite;
            }
            else
            {
                if (pokemon.Data.Types.HasSecondType)
                {
                    image.sprite = pokemon.Data.Types.SecondType.Sprite;
                    image.enabled = true;
                }
                else
                {
                    image.enabled = false;
                }
            }
        }
    }
}
