using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Monster.UI
{
    /// <summary>
    /// Displays a Monster's EXP bar and animates it smoothly as EXP changes.
    /// Can be bound to a <see cref="MonsterInstance"/> to track EXP and level updates automatically.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    internal sealed class ExperienceBar : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Image that fills the EXP bar visually (assign from the Slider's Fill child).")]
        private Image fillImage;

        [SerializeField, Tooltip("Seconds between each EXP tick during animation.")]
        private float tickDelay = 0.01f;

        private Slider slider;
        private MonsterInstance boundMonster;
        private Coroutine animateExpCoroutine;

        /// <summary>
        /// Raised when the EXP animation finishes.
        /// </summary>
        internal event Action ExpAnimationFinished;

        private void Awake()
        {
            EnsureSlider();
        }

        private void EnsureSlider()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
        }

        /// <summary>
        /// Binds this EXP bar to a Monster instance, subscribing to its EXP and level events.
        /// </summary>
        internal void Bind(MonsterInstance monster)
        {
            if (monster == null)
            {
                Unbind();
                return;
            }

            EnsureSlider();
            Unbind();
            boundMonster = monster;

            UpdateSliderRange();

            boundMonster.Experience.ExperienceChanged += HandleExperienceChanged;
            boundMonster.Experience.LevelChanged += HandleLevelChanged;
        }

        /// <summary>
        /// Unbinds the currently bound Monster and resets the EXP bar.
        /// </summary>
        internal void Unbind()
        {
            if (boundMonster != null)
            {
                boundMonster.Experience.ExperienceChanged -= HandleExperienceChanged;
                boundMonster.Experience.LevelChanged -= HandleLevelChanged;
                boundMonster = null;
            }

            if (animateExpCoroutine != null)
            {
                StopCoroutine(animateExpCoroutine);
                animateExpCoroutine = null;
            }

            slider.value = 0;
            slider.maxValue = 0;
        }

        /// <summary>
        /// Waits for the current EXP animation to complete.
        /// </summary>
        internal IEnumerator WaitForAnimationComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                ExpAnimationFinished -= OnComplete;
            }

            ExpAnimationFinished += OnComplete;
            yield return new WaitUntil(() => done);
        }

        private void HandleLevelChanged(int newLevel)
        {
            UpdateSliderRange();
        }

        private void HandleExperienceChanged(int oldExp, int newExp)
        {
            if (animateExpCoroutine != null)
            {
                StopCoroutine(animateExpCoroutine);
            }

            animateExpCoroutine = StartCoroutine(AnimateExpChange(oldExp, newExp));
        }

        private IEnumerator AnimateExpChange(int startValue, int endValue)
        {
            if (startValue == endValue)
            {
                slider.value = endValue;
                ExpAnimationFinished?.Invoke();
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
            ExpAnimationFinished?.Invoke();
        }

        private void UpdateSliderRange()
        {
            if (boundMonster == null) return;

            slider.minValue = boundMonster.Experience.GetExpForCurrentLevel();
            slider.maxValue = boundMonster.Experience.GetExpForNextLevel();
            slider.value = boundMonster.Experience.TotalExperience;
        }
    }
}
