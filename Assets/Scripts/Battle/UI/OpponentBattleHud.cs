using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays the opponent Pokémon's battle HUD, including name, level, health, and front sprite.
    /// </summary>
    [DisallowMultipleComponent]
    public class OpponentBattleHud : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field showing the opponent Pokémon's display name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text field showing the opponent Pokémon's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Health bar component displaying the opponent Pokémon's current HP.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Image component showing the opponent Pokémon's front-facing battle sprite.")]
        private Image frontSprite;

        public HealthBar HealthBar => healthBar;

        /// <summary>
        /// Initializes the opponent battle HUD with the given Pokémon data.
        /// </summary>
        /// <param name="pokemon">The opponent Pokémon to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = pokemon.Level.ToString();
            healthBar.Bind(pokemon);
            frontSprite.sprite = pokemon.Definition.Sprites.FrontSprite;
        }

        /// <summary>
        /// Clears all displayed information and unbinds the current Pokémon.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            frontSprite.sprite = null;
            healthBar.Unbind();
        }
    }
}
