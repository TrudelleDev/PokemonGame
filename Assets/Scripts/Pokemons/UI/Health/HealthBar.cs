using System;
using System.Collections;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.Health
{
    /// <summary>
    /// Displays a health bar with a fill sprite that changes based on the Pokémon's current health.
    /// Smoothly animates when the health changes.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour, IBindable<PokemonInstance>, IUnbind
    {
        private const float HighHealthThreshold = 0.5f;
        private const float MidHealthThreshold = 0.25f;

        [SerializeField, Required]
        [Tooltip("Sprites used for different health states.")]
        private HealthSpriteSettings healthSpriteSettings;

        [SerializeField, Required]
        [Tooltip("Animation speed and delay settings for health bar transitions.")]
        private HealthBarAnimationSettings healthBarAnimationSettings;

        [SerializeField, Required]
        [Tooltip("Image that fills the health bar visually (assign from the Slider's Fill child).")]
        private Image fillImage;

        private Slider slider;
        private PokemonInstance boundPokemon;
        private Coroutine animateHealthCoroutine;

        public event Action OnHealthAnimationFinished;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void EnsureSlider()
        {
            if(slider == null)
                slider = GetComponent<Slider>();
        }

        /// <summary>
        /// Binds this health bar to the given Pokémon and subscribes to health changes.
        /// </summary>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            EnsureSlider();
            Unbind();

            boundPokemon = pokemon;

            slider.maxValue = pokemon.CoreStat.HealthPoint;
            slider.value = pokemon.HealthRemaining;

            UpdateFillImage(pokemon.HealthRemaining);

            pokemon.OnHealthChange += OnPokemonHealthChange;
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

            if (animateHealthCoroutine != null)
            {
                StopCoroutine(animateHealthCoroutine);
                animateHealthCoroutine = null;
            }

            slider.value = 0;
            slider.maxValue = 0;

            if (fillImage != null)
            {
                fillImage.sprite = null;
            }
        }

        /// <summary>
        /// Called when the bound Pokémon's HP changes.
        /// Starts an animation to update the slider and sprite.
        /// </summary>
        /// <param name="oldHealth">The previous HP value before the change.</param>
        /// <param name="newHealth">The updated HP value after the change.</param>
        private void OnPokemonHealthChange(int oldHealth, int newHealth)
        {
            if (!isActiveAndEnabled)
                return;

            if (animateHealthCoroutine != null)
            {
                StopCoroutine(animateHealthCoroutine);
            }

            animateHealthCoroutine = StartCoroutine(AnimateHealthChange(oldHealth, newHealth));
        }

        /// <summary>
        /// Smoothly animates the slider value between two health values.
        /// Uses slower tick speed for damage and faster for healing.
        /// </summary>
        private IEnumerator AnimateHealthChange(int startValue, int endValue)
        {
            if (startValue == endValue)
            {
                slider.value = endValue;
                UpdateFillImage(endValue);
                OnHealthAnimationFinished?.Invoke();
                yield break;
            }

            float damageDelay = Mathf.Clamp(
                healthBarAnimationSettings.DamageSpeedFactor / slider.maxValue,
                healthBarAnimationSettings.MinTickDelay,
                healthBarAnimationSettings.MaxTickDelay);

            float healDelay = Mathf.Clamp(
                healthBarAnimationSettings.HealthSpeedFactor / slider.maxValue,
                healthBarAnimationSettings.MinTickDelay,
                healthBarAnimationSettings.MaxTickDelay);

            float tickDelay = endValue < startValue ? damageDelay : healDelay;
            int step = endValue > startValue ? 1 : -1;

            for (int hp = startValue; hp != endValue + step; hp += step)
            {
                slider.value = hp;

                UpdateFillImage(hp);

                yield return new WaitForSeconds(tickDelay);
            }

            // Final update just to ensure precision
            slider.value = endValue;
            UpdateFillImage(endValue);
            animateHealthCoroutine = null;
            OnHealthAnimationFinished?.Invoke();
        }

        /// <summary>
        /// Updates the fill sprite based on the current health percentage.
        /// </summary>
        private void UpdateFillImage(float currentHealth)
        {
            if (fillImage == null || slider.maxValue <= 0)
            {
                return;
            }

            float percent = currentHealth / slider.maxValue;

            HealthState state = GetHealthState(percent);

            Sprite sprite = state switch
            {
                HealthState.High => healthSpriteSettings.HighHealthSprite,
                HealthState.Moderate => healthSpriteSettings.ModerateHealthSprite,
                HealthState.Low => healthSpriteSettings.LowHealthSprite,
                _ => null
            };

            fillImage.sprite = sprite;
        }

        /// <summary>
        /// Gets the health state (High, Moderate, or Low) based on a percentage.
        /// </summary>
        private HealthState GetHealthState(float percent)
        {
            if (percent > HighHealthThreshold)
            {
                return HealthState.High;
            }

            if (percent > MidHealthThreshold)
            {
                return HealthState.Moderate;
            }

            return HealthState.Low;
        }
    }
}
