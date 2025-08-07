using PokemonGame.Shared.Interfaces;
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
    public class HealthBar : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        private const float ExcellentThreshold = 0.5f;
        private const float GoodThreshold = 0.25f;

        [Title("Health Sprites")]
        [SerializeField, Required]
        [Tooltip("Sprite for high health state.\nUsed when HP is above 50%.")]
        private Sprite excellentHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite for moderate health state.\nUsed when HP is between 25% and 50%.")]
        private Sprite goodHealthSprite;

        [SerializeField, Required]
        [Tooltip("Sprite for low health state.\nUsed when HP falls below 25%.")]
        private Sprite criticalHealthSprite;

        [Title("Slider Fill")]
        [SerializeField, Required]
        [Tooltip("Image that fills the health bar visually.\nShould be assigned from the Slider's Fill child.")]
        private Image fillImage;

        private Slider slider;
        private Pokemon boundPokemon;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        /// <summary>
        /// Binds this health bar to the given Pokémon and subscribes to health changes.
        /// </summary>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            Unbind();

            boundPokemon = pokemon;

            slider.minValue = 0;
            slider.maxValue = pokemon.CoreStat.HealthPoint;
            slider.value = pokemon.HealthRemaining;

            UpdateFillImage(pokemon.HealthRemaining);

            pokemon.OnHealthChange += OnPokemonHealthChange;
        }

        private void OnPokemonHealthChange(float currentHealth)
        {
            slider.value = currentHealth;
            UpdateFillImage(currentHealth);
        }

        private void UpdateFillImage(float currentHealth)
        {
            if (fillImage == null || slider.maxValue <= 0)
                return;

            float percent = currentHealth / slider.maxValue;

            var sprite = GetHealthState(percent) switch
            {
                HealthState.Excellent => excellentHealthSprite,
                HealthState.Good => goodHealthSprite,
                HealthState.Critical => criticalHealthSprite,
                _ => null
            };

            fillImage.sprite = sprite;
        }

        /// <summary>
        /// Unbinds the Pokémon and resets the UI.
        /// </summary>
        public void Unbind()
        {
            if (boundPokemon != null)
            {
                boundPokemon.OnHealthChange -= OnPokemonHealthChange;
                boundPokemon = null;
            }

            slider.value = 0;

            if (fillImage != null)
                fillImage.sprite = null;
        }

        /// <summary>
        /// Gets the health state (Excellent, Good, or Critical) based on a percentage.
        /// </summary>
        private HealthState GetHealthState(float percent)
        {
            if (percent > ExcellentThreshold)
                return HealthState.Excellent;
            if (percent > GoodThreshold)
                return HealthState.Good;
            return HealthState.Critical;
        }
    }
}
