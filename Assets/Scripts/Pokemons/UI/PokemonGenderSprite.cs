using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Image))]
    public class PokemonGenderSprite : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private Sprite maleSprite;
        [SerializeField] private Sprite femaleSprite;

        public void Bind(Pokemon pokemon)
        {
            Image genderImage = GetComponent<Image>();

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
