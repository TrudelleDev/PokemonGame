using PokemonGame.Pokemons.Interfaces;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays the Pokémon's name, level, gender icon, and front-facing sprite
    /// in header-style UI elements. Supports safe data binding and clearing.
    /// </summary>
    public class PokemonHeaderUI : MonoBehaviour, IPokemonBindable, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's front-facing sprite.")]
        private PokemonSprite frontSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        /// <summary>
        /// Binds the given Pokémon data to the name, level, gender icon, and sprite.
        /// Clears the UI if the Pokémon or its core data is missing.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                Unbind();
                return;
            }

            nameText.text = pokemon.Data.DisplayName;
            levelText.text = $"Lv {pokemon.Level}";
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
