using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays the gender icon of a Pokémon using predefined male or female sprites.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class PokemonGenderSprite : MonoBehaviour, IPokemonBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Icon to display for male Pokémon.")]
        private Sprite maleIcon;

        [SerializeField, Required]
        [Tooltip("Icon to display for female Pokémon.")]
        private Sprite femaleIcon;

        private Image genderImage;

        /// <summary>
        /// Binds the gender sprite based on the provided Pokémon's gender.
        /// Hides the icon if gender is unknown or no sprite assigned.
        /// </summary>
        /// <param name="pokemon">The Pokémon to display the gender for.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            EnsureImage();

            Sprite sprite = GetGenderSprite(pokemon.Gender);
            genderImage.sprite = sprite;
            genderImage.enabled = sprite != null;
        }

        /// <summary>
        /// Hides the gender icon.
        /// </summary>
        public void Unbind()
        {
            EnsureImage();
            genderImage.enabled = false;
        }

        private Sprite GetGenderSprite(PokemonGender gender)
        {
            return gender switch
            {
                PokemonGender.Male => maleIcon,
                PokemonGender.Female => femaleIcon,
                _ => null
            };
        }

        private void EnsureImage()
        {
            if (genderImage == null)
                genderImage = GetComponent<Image>();
        }
    }
}
