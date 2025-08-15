using System;
using System.Collections;
using System.Threading.Tasks;
using PokemonGame.Transitions.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Controls full-screen fade effects for both scene and UI view transitions.
    /// Supports async/await and callback usage, fading in (to black) or out (to reveal).
    /// Works with UI Images whose materials use a _Color property for alpha.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class AlphaFadeController : MonoBehaviour, ITransition
    {
        private const float FullyOpaque = 1f;
        private const float FullyTransparent = 0f;
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");

        [Title("Settings")]
        [SerializeField, Tooltip("Duration of the fade effect in seconds.")]
        private float duration = 1f;

        private Material runtimeMaterial;
        private Coroutine fadeRoutine;
        private TaskCompletionSource<bool> fadeCompletionSource;

        private void Awake()
        {
            var image = GetComponent<Image>();

            // Duplicate the material to avoid modifying the shared one
            runtimeMaterial = new Material(image.material);
            image.material = runtimeMaterial;

            SetAlpha(FullyTransparent);

            ServiceLocator.Register<AlphaFadeController>(this);
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
        /// Immediately begins a fade-in (transparent to black/opaque) using a callback on completion.
        /// </summary>
        /// <param name="onComplete">Invoked after the fade finishes.</param>
        public void FadeIn(Action onComplete = null)
        {
            StartFade(FullyOpaque, onComplete);
        }

        /// <summary>
        /// Immediately begins a fade-out (black/opaque to transparent) using a callback on completion.
        /// </summary>
        /// <param name="onComplete">Invoked after the fade finishes.</param>
        public void FadeOut(Action onComplete = null)
        {
            StartFade(FullyTransparent, onComplete);
        }

        /// <summary>
        /// Starts an asynchronous fade-in (transparent to black/opaque).
        /// Use when transitioning into a new scene or UI state.
        /// </summary>
        public Task FadeInAsync()
        {
            TaskCompletionSource<bool> fadeCompletion = PrepareFadeCompletionSource();
            FadeIn(() => fadeCompletion.TrySetResult(true));
            return fadeCompletion.Task;
        }

        /// <summary>
        /// Starts an asynchronous fade-out (black/opaque to transparent).
        /// Use when revealing gameplay or a new UI view.
        /// </summary>
        public Task FadeOutAsync()
        {
            TaskCompletionSource<bool> fadeCompletion = PrepareFadeCompletionSource();
            FadeOut(() => fadeCompletion.TrySetResult(true));
            return fadeCompletion.Task;
        }

        private void StartFade(float targetAlpha, Action onComplete)
        {
            // Stops any ongoing fade transition before starting a new one.

            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }

            gameObject.SetActive(true);
            fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, onComplete));
        }

        private IEnumerator FadeRoutine(float targetAlpha, Action onComplete)
        {
            float startAlpha = runtimeMaterial.GetColor(ColorProperty).a;

            // Instant if duration <= 0
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
                float progressTime = Mathf.Clamp01(elapsedTime / duration);
                SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, progressTime));
                yield return null;
            }

            SetAlpha(targetAlpha);

            fadeRoutine = null;
            onComplete?.Invoke();
            fadeCompletionSource?.TrySetResult(true);
        }

        private TaskCompletionSource<bool> PrepareFadeCompletionSource()
        {
            // Complete any previous waiter to avoid dangling tasks
            fadeCompletionSource?.TrySetResult(true);
            fadeCompletionSource = new TaskCompletionSource<bool>();
            return fadeCompletionSource;
        }

        private void SetAlpha(float alpha)
        {
            Color color = runtimeMaterial.GetColor(ColorProperty);
            color.a = alpha;
            runtimeMaterial.SetColor(ColorProperty, color);
        }
    }
}
