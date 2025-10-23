﻿using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI.Health;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays the player's active Pokémon HUD, including name, level, HP text, health bar, and back sprite.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerBattleHud : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon current and maximum HP.")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Health bar component displaying the player's Pokémon HP visually.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Image showing the player's Pokémon back-facing battle sprite.")]
        private Image backSprite;

        /// <summary>
        /// Initializes the HUD with the given Pokémon data.
        /// </summary>
        /// <param name="pokemon">The player's active Pokémon to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Definition == null)
            {
                return;
            }
 
            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = $"lv{pokemon.Level}";
            healthText.text = $"{pokemon.HealthRemaining}/{pokemon.MaxHealth}";
            healthBar.Bind(pokemon);
            backSprite.sprite = pokemon.Definition.Sprites.BackSprite;
        }

        /// <summary>
        /// Clears the HUD display and unbinds the current Pokémon.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthText.text = "/";
            backSprite.sprite = null;
            healthBar.Unbind();
        }
    }
}
