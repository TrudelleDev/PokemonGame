using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays the Pokémon type icon (Primary or Secondary) in a UI Image.
    /// Hides the icon if the type is not available.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class PokemonTypeIcon : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Select whether to show the Pokémon's primary or secondary type icon.")]
        private PokemonTypeSlot slot;

        private Image typeImage;

        /// <summary>
        /// Shows the Pokémon's type icon if available, otherwise hides it.
        /// </summary>
        public void Bind(Pokemon pokemon)
        {
            EnsureImage();

            if (pokemon == null)
            {
                Unbind();
                return;
            }

            Sprite typeSprite = GetTypeSprite(pokemon);

            // No type sprite available for the selected slot
            if (typeSprite == null)
            {
                Unbind();
                return;
            }

            typeImage.sprite = typeSprite;
            typeImage.enabled = true;
        }

        /// <summary>
        /// Hides the icon from the UI.
        /// </summary>
        public void Unbind()
        {
            EnsureImage();
            typeImage.enabled = false;
        }

        private Sprite GetTypeSprite(Pokemon pokemon)
        {
            return slot switch
            {
                PokemonTypeSlot.Primary => pokemon.Definition?.Types?.FirstType?.Sprite,
                PokemonTypeSlot.Secondary => pokemon.Definition?.Types?.SecondType?.Sprite,
                _ => null
            };
        }

        private void EnsureImage()
        {
            if (typeImage == null)
                typeImage = GetComponent<Image>();
        }
    }
}
