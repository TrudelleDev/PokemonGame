using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public class PokemonTypeSprite : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private PokemonType pokemonType;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Bind(Pokemon pokemon)
        {
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
