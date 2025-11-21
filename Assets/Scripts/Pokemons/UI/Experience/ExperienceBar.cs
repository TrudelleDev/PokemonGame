using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.Experience
{
    /// <summary>
    /// Displays the Pokémon's EXP bar and smoothly animates when EXP changes.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Image that fills the EXP bar visually (assign from the Slider's Fill child).")]
        private Image fillImage;

        [SerializeField, Tooltip("Seconds between each EXP tick during animation.")]
        private float tickDelay = 0.01f;

        private Slider slider;
        private PokemonInstance boundPokemon;
        private Coroutine animateExpCoroutine;

        public event Action OnExpAnimationFinished;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void EnsureSlider()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
        }

        /// <summary>
        /// Binds this EXP bar to the given Pokémon and subscribes to EXP changes.
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

            int minExp = pokemon.GetExpForCurrentLevel();
            int maxExp = pokemon.GetExpForNextLevel();

            slider.minValue = minExp;
            slider.maxValue = maxExp;
            slider.value = pokemon.CurrentExp;

            pokemon.OnExperienceChange += OnPokemonExpChange;
            pokemon.OnLevelChange += OnPokemonLevelChange;
        }

        /// <summary>
        /// Unbinds the Pokémon and resets the bar.
        /// </summary>
        public void Unbind()
        {
            if (boundPokemon != null)
            {
                boundPokemon.OnExperienceChange -= OnPokemonExpChange;
                boundPokemon.OnLevelChange -= OnPokemonLevelChange;
                boundPokemon = null;
            }

            if (animateExpCoroutine != null)
            {
                StopCoroutine(animateExpCoroutine);
                animateExpCoroutine = null;
            }

            slider.value = 0;
            slider.maxValue = 0;
        }

        private void OnPokemonLevelChange(int newLevel)
        {
            int minExp = boundPokemon.GetExpForCurrentLevel();
            int maxExp = boundPokemon.GetExpForNextLevel();

            slider.minValue = minExp;
            slider.maxValue = maxExp;
            slider.value = boundPokemon.CurrentExp;
        }

        /// <summary>
        /// Called when the bound Pokémon's EXP changes.
        /// Starts a smooth animation to update the slider value.
        /// </summary>
        private void OnPokemonExpChange(int oldExp, int newExp)
        {
            if (animateExpCoroutine != null)
                StopCoroutine(animateExpCoroutine);

            animateExpCoroutine = StartCoroutine(AnimateExpChange(oldExp, newExp));
        }

        /// <summary>
        /// Smoothly animates the EXP slider between two values.
        /// </summary>
        private IEnumerator AnimateExpChange(int startValue, int endValue)
        {
            if (startValue == endValue)
            {
                slider.value = endValue;
                OnExpAnimationFinished?.Invoke();
                yield break;
            }

            int step = endValue > startValue ? 1 : -1;

            for (int exp = startValue; exp != endValue + step; exp += step)
            {
                slider.value = exp;
                yield return new WaitForSeconds(tickDelay);
            }

            slider.value = endValue;
            animateExpCoroutine = null;
            OnExpAnimationFinished?.Invoke();
        }
    }
}
