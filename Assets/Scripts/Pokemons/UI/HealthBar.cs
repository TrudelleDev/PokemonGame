using PokemonGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays a health bar with a fill sprite that changes based on the Pokémon's current health.
    /// Automatically updates when the Pokémon's health changes.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour, IPokemonBind, IUnbind
    {
        private const float ExcellentThreshold = 0.5f;
        private const float GoodThreshold = 0.25f;

        [SerializeField, Required]
        [Tooltip("Sprite used when health is above 50%.")]
        private Sprite excellentHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite used when health is between 25% and 50%.")]
        private Sprite goodHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite used when health is below 25%.")]
        private Sprite criticalHealthSprite;

        private Image fillImage;
        private Slider slider;
        private Pokemon boundPokemon;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            fillImage = GetComponent<Image>();
        }

        /// <summary>
        /// Binds this health bar to the given Pokémon and listens for health changes.
        /// </summary>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            slider.minValue = 0;
            slider.maxValue = pokemon.CoreStat.HealthPoint;
            slider.value = slider.maxValue;

            boundPokemon = pokemon;
            pokemon.OnHealthChange += OnPokemonHealthChange;
        }

        /// <summary>
        /// Updates the fill sprite based on the current health percentage.
        /// </summary>
        private void OnPokemonHealthChange(float currentHealth)
        {
            slider.value = currentHealth;

            // Just in case maxValue is ever 0 (e.g., before full init):
            float percent = slider.maxValue > 0 ? currentHealth / slider.maxValue : 0f;
            var state = GetHealthState(percent);

            switch (state)
            {
                case HealthState.Excellent:
                    fillImage.sprite = excellentHealthSprite;
                    break;
                case HealthState.Good:
                    fillImage.sprite = goodHealthSprite;
                    break;
                case HealthState.Critical:
                    fillImage.sprite = criticalHealthSprite;
                    break;
            }
        }

        /// <summary>
        /// Unbinds from the current Pokémon and clears the UI.
        /// </summary>
        public void Unbind()
        {
            if (boundPokemon != null)
            {
                boundPokemon.OnHealthChange -= OnPokemonHealthChange;
                boundPokemon = null;
            }

            slider.value = 0;
            fillImage.sprite = null;
        }

        /// <summary>
        /// Returns the appropriate health state based on the given percentage.
        /// </summary>
        private HealthState GetHealthState(float healthPercent)
        {
            if (healthPercent > ExcellentThreshold)
                return HealthState.Excellent;
            if (healthPercent > GoodThreshold)
                return HealthState.Good;
            return HealthState.Critical;
        }
    }
}
