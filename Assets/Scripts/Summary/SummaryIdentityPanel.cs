using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Controls the Pok�mon identity panel UI within the summary screen.
    /// Displays the Pok�mon's name, gender, menu sprite, and type icons.
    /// Supports dynamic data binding and clears the UI when data is missing or invalid.
    /// </summary>
    public class SummaryIdentityPanel : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's name.")]
        private TextMeshProUGUI nameText;

        [Title("Sprites")]
        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's menu sprite.")]
        private PokemonSprite menuSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's primary type icon.")]
        private PokemonTypeIcon primaryType;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's secondary type icon.")]
        private PokemonTypeIcon secondaryType;

        /// <summary>
        /// Binds the given Pok�mon data to identity-related UI elements.
        /// Clears the UI if pokemon is null or missing required data.
        /// </summary>
        /// <param name="pokemon">The Pok�mon instance to display, or null to clear the UI.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = pokemon.Definition.DisplayName;
            genderIcon.Bind(pokemon);
            menuSprite.Bind(pokemon);
            primaryType.Bind(pokemon);
            secondaryType.Bind(pokemon);
        }

        /// <summary>
        /// Clears all UI elements related to the Pok�mon's identity.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            genderIcon.Unbind();
            menuSprite.Unbind();
            primaryType.Unbind();
            secondaryType.Unbind();
        }
    }
}
