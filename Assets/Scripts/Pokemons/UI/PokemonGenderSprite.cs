using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public class PokemonGenderSprite : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private Sprite maleSprite;
        [SerializeField] private Sprite femaleSprite;

        private Image genderImage;

        private void Awake()
        {
            genderImage = GetComponent<Image>();
        }

        public void Bind(Pokemon pokemon)
        {         
            if (pokemon.Gender == PokemonGender.Male)
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
