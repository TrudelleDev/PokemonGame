using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Manages the UI elements for the Pok�mon summary header.
    /// Displays the Pok�mon's name, level, gender, and sprite.
    /// Safely handles null or incomplete Pok�mon data by clearing the UI.
    /// </summary>
    public class SummaryHeader : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's level.")]
        private TextMeshProUGUI levelText;

        [Title("Sprites")]
        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's front-facing sprite.")]
        private PokemonSprite frontSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        /// <summary>
        /// Binds the given Pok�mon data to the name, level, gender icon, and sprite.
        /// Clears the UI if <paramref name="pokemon"/> is null or missing required data.
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
            levelText.text = $"<size=12>Lv</size>{pokemon.Level}";
            genderIcon.Bind(pokemon);
            frontSprite.Bind(pokemon);
        }

        /// <summary>
        /// Clears the UI elements by removing all Pok�mon-related data.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            genderIcon.Unbind();
            frontSprite.Unbind();
        }
    }
}
