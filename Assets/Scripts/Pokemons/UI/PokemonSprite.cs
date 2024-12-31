using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public partial class PokemonSprite : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private PokemonSpriteType spriteType;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Bind(Pokemon pokemon)
        {
            if(spriteType == PokemonSpriteType.MenuSprite)
                image.sprite = pokemon.Data.Sprites.MenuSprite;
            if (spriteType == PokemonSpriteType.FrontSprite)
                image.sprite = pokemon.Data.Sprites.FrontSprite;
            if (spriteType == PokemonSpriteType.BackSprite)
                image.sprite = pokemon.Data.Sprites.BackSprite;
        }
    }
}
