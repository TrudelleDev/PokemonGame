using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Handles screen transitions using a shader-based cutoff effect.
    /// Designed to work with UI Images using a material that supports a _Cutoff property and optional mask texture.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class MaskedFadeController : Transition
    {
        private static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");

        [Title("Settings")]
        [SerializeField, Tooltip("Duration of the fade effect in seconds.")]
        private float duration = 1f;

        private Material runtimeMaterial;
        private Coroutine fadeRoutine;

        /// <summary>
        /// Initializes the transition by duplicating the image material and setting the initial masked state.
        /// </summary>
        private void Awake()
        {
            var image = GetComponent<Image>();

            // Duplicate the material to avoid modifying the shared one
            runtimeMaterial = new Material(image.material);
            image.material = runtimeMaterial;

            SetCutoff(1f); // Start fully masked
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Starts a transition that fades the screen in (from masked to visible).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-in completes.</param>
        public override void FadeIn(Action onComplete = null)
        {
            StartFade(1f, 0f, onComplete);
        }

        /// <summary>
        /// Starts a transition that fades the screen out (from visible to masked).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-out completes.</param>
        public override void FadeOut(Action onComplete = null)
        {
            StartFade(0f, 1f, onComplete);
        }

        /// <summary>
        /// Begins the fade process toward a target cutoff value, if no transition is currently running.
        /// </summary>
        /// <param name="startCutoff">The initial cutoff value (1 = masked, 0 = visible).</param>
        /// <param name="endCutoff">The target cutoff value to fade toward.</param>
        /// <param name="onComplete">Optional callback invoked when the transition completes.</param>
        private void StartFade(float startCutoff, float endCutoff, Action onComplete)
        {
            // Skip if another fade is running
            if (fadeRoutine != null)
                return;

            gameObject.SetActive(true);
            fadeRoutine = StartCoroutine(FadeRoutine(startCutoff, endCutoff, onComplete));
        }

        /// <summary>
        /// Coroutine that performs a timed interpolation from the starting cutoff to the target cutoff.
        /// </summary>
        /// <param name="startCutoff">The cutoff value to start fading from.</param>
        /// <param name="endCutoff">The cutoff value to fade to.</param>
        /// <param name="onComplete">Optional callback invoked when the fade completes.</param>
        /// <returns>An IEnumerator for Unity’s coroutine system.</returns>
        private IEnumerator FadeRoutine(float startCutoff, float endCutoff, Action onComplete)
        {
            float time = 0f;

            while (time < duration)
            {
                time += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(time / duration);
                SetCutoff(Mathf.Lerp(startCutoff, endCutoff, t));
                yield return null;
            }

            SetCutoff(endCutoff);

            // Disable the object if fully masked
            if (Mathf.Approximately(endCutoff, 1f))
                gameObject.SetActive(false);

            fadeRoutine = null;
            onComplete?.Invoke();
        }

        /// <summary>
        /// Applies the given cutoff value to the material's _Cutoff property.
        /// </summary>
        /// <param name="value">The cutoff value to apply (0 = visible, 1 = masked).</param>
        private void SetCutoff(float value)
        {
            runtimeMaterial.SetFloat(CutoffProperty, value);
        }
    }
}
