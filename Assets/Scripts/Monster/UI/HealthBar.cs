using System;
using System.Collections;
using MonsterTamer.Monster.Enums;
using MonsterTamer.Monster.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Monster.UI
{
    /// <summary>
    /// Displays and animates a monster's health using a Slider.
    /// Updates the fill sprite based on current health percentage.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    internal sealed class HealthBar : MonoBehaviour
    {
        private const float HighHealthThreshold = 0.5f;
        private const float MidHealthThreshold = 0.25f;

        [SerializeField, Required]
        [Tooltip("Sprites used for high, moderate, and low health states.")]
        private HealthSpriteSettings healthSpriteSettings;

        [SerializeField, Required]
        [Tooltip("Controls animation speed and tick delay for health changes.")]
        private HealthBarAnimationSettings healthBarAnimationSettings;

        [SerializeField, Required]
        [Tooltip("Image used to visually fill the health bar (Slider > Fill).")]
        private Image fillImage;

        private Slider slider;
        private MonsterInstance boundMonster;
        private Coroutine animateHealthCoroutine;

        /// <summary>
        /// Raised when the health bar animation finishes.
        /// </summary>
        internal event Action OnHealthAnimationFinished;

        private void OnEnable()
        {
            EnsureSlider();
        }

        /// <summary>
        /// Binds the health bar to a monster instance and listens for health changes.
        /// </summary>
        /// <param name="monster">Monster instance to display health for.</param>
        internal void Bind(MonsterInstance monster)
        {
            EnsureSlider();
            Unbind();

            if (monster == null)
            {
                return;
            }

            boundMonster = monster;
            slider.maxValue = boundMonster.Stats.Core.HealthPoint;
            slider.value = boundMonster.Health.CurrentHealth;

            UpdateFillImage(slider.value);
            boundMonster.Health.HealthChanged += HandleHealthChange;
        }

        /// <summary>
        /// Unbinds the current monster and resets the health bar state.
        /// </summary>
        internal void Unbind()
        {
            EnsureSlider();

            if (boundMonster != null)
            {
                boundMonster.Health.HealthChanged -= HandleHealthChange;
                boundMonster = null;
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
        /// Waits until the current health animation completes.
        /// </summary>
        internal IEnumerator WaitForHealthAnimationComplete()
        {
            bool completed = false;

            void OnComplete()
            {
                completed = true;
                OnHealthAnimationFinished -= OnComplete;
            }

            OnHealthAnimationFinished += OnComplete;
            yield return new WaitUntil(() => completed);
        }

        /// <summary>
        /// Handles health value changes from the bound monster.
        /// </summary>
        /// <param name="oldHealth">Previous health value.</param>
        /// <param name="newHealth">New health value.</param>
        private void HandleHealthChange(int oldHealth, int newHealth)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            if (animateHealthCoroutine != null)
            {
                StopCoroutine(animateHealthCoroutine);
            }

            animateHealthCoroutine = StartCoroutine(AnimateHealthChange(oldHealth, newHealth));
        }

        /// <summary>
        /// Smoothly animates the health bar from one value to another.
        /// </summary>
        /// <param name="startValue">Initial health value.</param>
        /// <param name="endValue">Target health value.</param>
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
                healthBarAnimationSettings.HealSpeedFactor / slider.maxValue,
                healthBarAnimationSettings.MinTickDelay,
                healthBarAnimationSettings.MaxTickDelay);

            float tickDelay = endValue < startValue ? damageDelay : healDelay;

            int current = startValue;
            while (current != endValue)
            {
                current = (int)Mathf.MoveTowards(current, endValue, 1);
                slider.value = current;
                UpdateFillImage(current);
                yield return new WaitForSeconds(tickDelay);
            }

            slider.value = endValue;
            UpdateFillImage(endValue);

            animateHealthCoroutine = null;
            OnHealthAnimationFinished?.Invoke();
        }

        /// <summary>
        /// Updates the fill sprite based on the current health percentage.
        /// </summary>
        /// <param name="currentHealth">Current health value.</param>
        private void UpdateFillImage(float currentHealth)
        {
            if (fillImage == null || slider.maxValue <= 0)
            {
                return;
            }

            float percent = currentHealth / slider.maxValue;
            HealthState state = GetHealthState(percent);

            fillImage.sprite = state switch
            {
                HealthState.High => healthSpriteSettings.HighHealthSprite,
                HealthState.Moderate => healthSpriteSettings.ModerateHealthSprite,
                HealthState.Low => healthSpriteSettings.LowHealthSprite,
                _ => null
            };
        }

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
            else
            {
                return HealthState.Low;
            }      
        }

        private void EnsureSlider()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
        }
    }
}
