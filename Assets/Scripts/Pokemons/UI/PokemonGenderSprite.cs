using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public class PokemonGenderSprite : MonoBehaviour, IPokemonBind, IComponentInitialize
    {
        [SerializeField] private Sprite maleSprite;
        [SerializeField] private Sprite femaleSprite;

        private Image genderImage;

        public void Initialize()
        {
            genderImage = GetComponent<Image>();
        }

        public void Bind(Pokemon pokemon)
        {
            if (pokemon.Gender == Gender.Male)
            {
                genderImage.sprite = maleSprite;
            }
            else
            {
                genderImage.sprite = femaleSprite;
            }
        }
    }
}
