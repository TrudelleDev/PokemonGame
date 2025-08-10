using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Manages UI elements displaying a Pokémon's identity, including name, gender, sprite, and types.
    /// </summary>
    public class PokemonIdentityUI : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's menu sprite.")]
        private PokemonSprite pokemonSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's primary type icon.")]
        private PokemonTypeIcon primaryType;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's secondary type icon.")]
        private PokemonTypeIcon secondaryType;

        /// <summary>
        /// Binds the given Pokémon data to identity-related UI elements.
        /// </summary>
        /// <param name="pokemon">The Pokémon to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = pokemon.Definition.name;
            genderIcon.Bind(pokemon);
            pokemonSprite.Bind(pokemon);
            primaryType.Bind(pokemon);
            secondaryType.Bind(pokemon);
        }

        /// <summary>
        /// Clears all UI elements related to the Pokémon's identity.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            genderIcon.Unbind();
            pokemonSprite.Unbind();
            primaryType.Unbind();
            secondaryType.Unbind();
        }
    }
}
