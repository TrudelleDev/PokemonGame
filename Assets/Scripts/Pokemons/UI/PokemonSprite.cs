using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays a Pokémon sprite in the UI using a specific sprite type.
    /// Hides the image if no valid sprite is available.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public partial class PokemonSprite : MonoBehaviour, IBindable<PokemonInstance>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Determines which sprite variant to display (Menu, Front, or Back).")]
        private PokemonSpriteType spriteType;

        private Image image;

        /// <summary>
        /// Sets the sprite image from the given Pokémon using the configured sprite type.
        /// If the sprite is missing, the image is hidden.
        /// </summary>
        /// <param name="pokemon">The Pokémon to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            EnsureImage();

            if (pokemon?.Definition?.Sprites == null)
            {
                Unbind();
                return;
            }

            Sprite selectedSprite = GetSprite(pokemon);
            image.sprite = selectedSprite;
            image.enabled = selectedSprite != null;
        }

        /// <summary>
        /// Hides the image when the Pokémon or its sprite data is invalid or missing.
        /// </summary>
        public void Unbind()
        {
            EnsureImage();
            image.enabled = false;
        }

        private Sprite GetSprite(PokemonInstance pokemon)
        {
            var sprites = pokemon.Definition.Sprites;

            return spriteType switch
            {
                PokemonSpriteType.Menu => sprites.MenuSprite,
                PokemonSpriteType.Front => sprites.FrontSprite,
                PokemonSpriteType.Back => sprites.BackSprite,
                _ => null
            };
        }

        private void EnsureImage()
        {
            if (image == null)
                image = GetComponent<Image>();
        }
    }
}
