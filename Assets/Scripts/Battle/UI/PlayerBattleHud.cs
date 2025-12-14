using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Displays the player's active Pokémon HUD, including name, level, HP text, health bar, and back sprite.
    /// It subscribes to the bound PokemonInstance's events (Health and Experience) to update itself in real-time.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerBattleHud : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Pokémon current and maximum HP (e.g., 100/150).")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Health bar component displaying the player's Pokémon HP visually.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Experience bar component displaying the player's Pokémon current EXP progress.")]
        private ExperienceBar experienceBar;

        [SerializeField, Required]
        [Tooltip("Image showing the player's Pokémon back-facing battle sprite.")]
        private Image backSprite;

        private PokemonInstance currentPokemon;

        /// <summary>
        /// Provides access to the HealthBar component, allowing the state machine to trigger damage animations.
        /// </summary>
        public HealthBar HealthBar => healthBar;

        /// <summary>
        /// Provides access to the ExperienceBar component, allowing the VictoryState to trigger EXP fill animations.
        /// </summary>
        public ExperienceBar ExperienceBar => experienceBar;

        /// <summary>
        /// Initializes the HUD with the given Pokémon data and sets up real-time event listeners.
        /// </summary>
        /// <param name="pokemon">The player's active Pokémon instance to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            // Always unbind any previously bound Pokémon before binding a new one.
            UnsubscribeCurrentPokemon();

            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            // Storing the reference is essential for accessing Health/Experience properties in event handlers.
            currentPokemon = pokemon;

            // Initial UI update
            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = pokemon.Experience.Level.ToString();
            healthText.text = $"{pokemon.Health.CurrentHealth}/{pokemon.Health.MaxHealth}";
            backSprite.sprite = pokemon.Definition.Sprites.BackSprite;

            // Delegate binding to specialized sub-components
            healthBar.Bind(pokemon);
            experienceBar.Bind(pokemon);

            // Subscribe to events for real-time updates
            currentPokemon.Experience.OnLevelChange += OnPokemonLevelChange;
            currentPokemon.Health.OnHealthChange += OnPokemonHealthChange;
        }

        /// <summary>
        /// Clears the HUD display and unbinds the current Pokémon.
        /// </summary>
        public void Unbind()
        {
            // Ensure events are cleaned up first
            UnsubscribeCurrentPokemon();

            // Reset UI state
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthText.text = "- / -";
            backSprite.sprite = null;

            // Delegate unbinding to specialized sub-components
            healthBar.Unbind();
            experienceBar.Unbind();
        }

        private void OnPokemonHealthChange(int oldHp, int newHp)
        {
            // Update the raw HP text when health changes
            healthText.text = $"{currentPokemon.Health.CurrentHealth}/{currentPokemon.Health.MaxHealth}";
        }

        private void OnPokemonLevelChange(int newLevel)
        {
            // Update UI elements affected by a level change (level number and max health text)
            levelText.text = currentPokemon.Experience.Level.ToString();
            healthText.text = $"{currentPokemon.Health.CurrentHealth}/{currentPokemon.Health.MaxHealth}";
        }

        /// <summary>
        /// Helper to safely unsubscribe all events from the previously bound Pokémon before a new one is bound 
        /// or the HUD is fully unbound.
        /// </summary>
        private void UnsubscribeCurrentPokemon()
        {
            if (currentPokemon != null)
            {
                currentPokemon.Experience.OnLevelChange -= OnPokemonLevelChange;
                currentPokemon.Health.OnHealthChange -= OnPokemonHealthChange;
                currentPokemon = null;
            }
        }
    }
}