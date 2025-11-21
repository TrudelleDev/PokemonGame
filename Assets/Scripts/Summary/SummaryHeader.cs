using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Manages the UI elements for the Pokémon summary header.
    /// Displays the Pokémon's name, level, gender, and sprite.
    /// Safely handles null or incomplete Pokémon data by clearing the UI.
    /// </summary>
    public class SummaryHeader : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's level.")]
        private TextMeshProUGUI levelText;

        [Title("Sprites")]
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's front-facing sprite.")]
        private PokemonSprite frontSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's gender icon.")]
        private PokemonGenderSymbol genderIcon;

        /// <summary>
        /// Binds the given Pokémon data to the name, level, gender icon, and sprite.
        /// Clears the UI if <paramref name="pokemon"/> is null or missing required data.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display, or null to clear the UI.</param>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = $"Lv{pokemon.Level}";
            genderIcon.Bind(pokemon);
            frontSprite.Bind(pokemon);
        }

        /// <summary>
        /// Clears the UI elements by removing all Pokémon-related data.
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
