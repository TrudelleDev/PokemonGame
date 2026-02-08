using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Transitions.Controllers
{
    /// <summary>
    /// Controls full-screen fade effects for scene and UI transitions.
    /// Uses a UI Image material with a <c>_Color</c> property to animate alpha.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class AlphaFadeController : Transition
    {
        [SerializeField]
        [Tooltip("Duration of the fade effect in seconds.")]
        private float duration = 1f;

        private const float FullyOpaque = 1f;
        private const float FullyTransparent = 0f;
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");

        private Material runtimeMaterial;
        private Coroutine fadeRoutine;
        private TaskCompletionSource<bool> fadeCompletionSource;

        private void Awake()
        {
            var image = GetComponent<Image>();

            // Duplicate the material to avoid modifying the shared one
            runtimeMaterial = new Material(image.material);
            image.material = runtimeMaterial;

            // Start fully transparent (no fade applied yet)
            SetAlpha(FullyTransparent);
        }

        private void OnDestroy()
        {
            if (runtimeMaterial != null)
            {
                Destroy(runtimeMaterial);
                runtimeMaterial = null;
            }
        }

        /// <summary>
        /// Starts a fade-out (opaque to transparent).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked after the fade completes.</param>
        protected override void FadeInInternal(Action onComplete)
        {
            StartFade(FullyOpaque, onComplete);
        }

        /// <summary>
        /// Starts a fade-out (opaque to transparent).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked after the fade completes.</param>
        protected override void FadeOutInternal(Action onComplete)
        {
            StartFade(FullyTransparent, onComplete);
        }

        /// <summary>
        /// Begins a fade toward the given alpha target.
        /// Stops any existing fade before starting a new one.
        /// </summary>
        /// <param name="targetAlpha">The final alpha value.</param>
        /// <param name="onComplete">Optional callback invoked after the fade completes.</param>
        private void StartFade(float targetAlpha, Action onComplete)
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }

            gameObject.SetActive(true);
            fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, onComplete));
        }

        /// <summary>
        /// Coroutine that interpolates alpha from the current value to the target.
        /// </summary>
        /// <param name="targetAlpha">The final alpha value.</param>
        /// <param name="onComplete">Optional callback invoked after the fade completes.</param>
        private IEnumerator FadeRoutine(float targetAlpha, Action onComplete)
        {
            float startAlpha = runtimeMaterial.GetColor(ColorProperty).a;

            // Instant fade if duration is zero or negative
            if (duration <= 0f)
            {
                SetAlpha(targetAlpha);
                fadeRoutine = null;
                onComplete?.Invoke();
                fadeCompletionSource?.TrySetResult(true);
                yield break;
            }

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, t));
                yield return null;
            }

            SetAlpha(targetAlpha);
            fadeRoutine = null;
            onComplete?.Invoke();
            fadeCompletionSource?.TrySetResult(true);
        }

        /// <summary>
        /// Applies the given alpha value to the runtime material.
        /// </summary>
        /// <param name="alpha">Alpha between 0 (transparent) and 1 (opaque).</param>
        private void SetAlpha(float alpha)
        {
            Color color = runtimeMaterial.GetColor(ColorProperty);
            color.a = alpha;
            runtimeMaterial.SetColor(ColorProperty, color);
        }
    }
}