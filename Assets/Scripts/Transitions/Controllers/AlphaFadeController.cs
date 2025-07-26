using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Handles screen transitions using a simple alpha fade effect.
    /// Designed to work with UI Images using a material that respects the _Color.a value.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class AlphaFadeController : Transition
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");

        [Title("Settings")]
        [SerializeField, Tooltip("Duration of the fade effect in seconds.")]
        private float duration = 1f;

        private Material runtimeMaterial;
        private Coroutine fadeRoutine;

        /// <summary>
        /// Initializes the transition by duplicating the image material and setting the initial transparent state.
        /// </summary>
        private void Awake()
        {
            var image = GetComponent<Image>();

            // Duplicate the material to avoid modifying the shared one
            runtimeMaterial = new Material(image.material);
            image.material = runtimeMaterial;

            SetAlpha(0f); // Start fully transparent
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Starts a transition that fades the screen in (alpha from 0 to 1).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-in completes.</param>
        public override void FadeIn(Action onComplete = null)
        {
            StartFade(1f, onComplete);
        }

        /// <summary>
        /// Starts a transition that fades the screen out (alpha from 1 to 0).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-out completes.</param>
        public override void FadeOut(Action onComplete = null)
        {
            StartFade(0f, onComplete);
        }

        /// <summary>
        /// Begins the fade process toward a target alpha value.
        /// Stops any ongoing fade transition before starting a new one.
        /// </summary>
        /// <param name="targetAlpha">The alpha value to fade to (0 = transparent, 1 = opaque).</param>
        /// <param name="onComplete">Optional callback invoked when the fade completes.</param>
        private void StartFade(float targetAlpha, Action onComplete)
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            gameObject.SetActive(true);
            fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, onComplete));
        }

        /// <summary>
        /// Coroutine that interpolates the material alpha over time.
        /// </summary>
        /// <param name="targetAlpha">The final alpha value to fade to.</param>
        /// <param name="onComplete">Optional callback invoked when the fade completes.</param>
        /// <returns>An IEnumerator for Unity’s coroutine system.</returns>
        private IEnumerator FadeRoutine(float targetAlpha, Action onComplete)
        {
            float startAlpha = runtimeMaterial.GetColor(ColorProperty).a;
            float time = 0f;

            while (time < duration)
            {
                time += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(time / duration);
                SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, t));
                yield return null;
            }

            SetAlpha(targetAlpha);

            // Disable the object if fully transparent
            if (Mathf.Approximately(targetAlpha, 0f))
                gameObject.SetActive(false);

            fadeRoutine = null;
            onComplete?.Invoke();
        }

        /// <summary>
        /// Applies the given alpha value to the material's _Color property.
        /// </summary>
        /// <param name="alpha">The alpha value to apply (0 = transparent, 1 = opaque).</param>
        private void SetAlpha(float alpha)
        {
            Color color = runtimeMaterial.GetColor(ColorProperty);
            color.a = alpha;
            runtimeMaterial.SetColor(ColorProperty, color);
        }
    }
}
