using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Handles screen transitions using a shader-based cutoff effect.
    /// Works with UI Images that have a material supporting a <c>_Cutoff</c> property.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class MaskedFadeController : Transition
    {
        private const float CutoffHidden = 1f;
        private const float CutoffVisible = 0f;
        private static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");

        private Material runtimeMaterial;
        private Coroutine fadeRoutine;

        private void Awake()
        {
            var image = GetComponent<Image>();

            // Duplicate the material to avoid modifying the shared one
            runtimeMaterial = new Material(image.material);
            image.material = runtimeMaterial;

            // Start fully masked
            SetCutoff(CutoffHidden);
        }

        /// <summary>
        /// Fades the screen in (masked to visible).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade completes.</param>
        protected override void FadeInInternal(Action onComplete = null)
        {
            StartFade(CutoffVisible, CutoffHidden, onComplete);
        }

        /// <summary>
        /// Fades the screen out (visible to masked).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade completes.</param>
        protected override void FadeOutInternal(Action onComplete = null)
        {
            StartFade(CutoffHidden, CutoffVisible, onComplete);
        }

        /// <summary>
        /// Starts a fade routine from one cutoff value to another.
        /// </summary>
        /// <param name="startCutoff">Initial cutoff value.</param>
        /// <param name="endCutoff">Target cutoff value.</param>
        /// <param name="onComplete">Optional callback invoked after completion.</param>
        private void StartFade(float startCutoff, float endCutoff, Action onComplete)
        {
            if (fadeRoutine != null)
            {
                return; // Already running
            }

            fadeRoutine = StartCoroutine(FadeRoutine(startCutoff, endCutoff, onComplete));
        }

        /// <summary>
        /// Coroutine that interpolates the cutoff over time.
        /// </summary>
        /// <param name="startCutoff">Initial cutoff value.</param>
        /// <param name="endCutoff">Target cutoff value.</param>
        /// <param name="onComplete">Optional callback invoked after completion.</param>
        private IEnumerator FadeRoutine(float startCutoff, float endCutoff, Action onComplete)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                SetCutoff(Mathf.Lerp(startCutoff, endCutoff, t));
                yield return null;
            }

            SetCutoff(endCutoff);
            fadeRoutine = null;
            onComplete?.Invoke();
        }

        /// <summary>
        /// Applies a cutoff value to the runtime material.
        /// </summary>
        /// <param name="value">Cutoff value between 0 (visible) and 1 (hidden).</param>
        private void SetCutoff(float value)
        {
            runtimeMaterial.SetFloat(CutoffProperty, value);
        }
    }
}
