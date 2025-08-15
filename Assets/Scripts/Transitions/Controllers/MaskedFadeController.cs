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
    /// Handles screen transitions using a shader-based cutoff effect.
    /// Designed to work with UI Images using a material that supports a _Cutoff property and optional mask texture.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class MaskedFadeController : MonoBehaviour, ITransition
    {
        private const float CutoffHidden = 1f;   // Fully hidden (mask covers everything)
        private const float CutoffVisible = 0f;  // Fully revealed (mask shows content)
        private static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");

        [Title("Settings")]
        [SerializeField, Tooltip("Duration of the fade effect in seconds.")]
        private float duration = 1f;

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

            ServiceLocator.Register<MaskedFadeController>(this);
        }

        /// <summary>
        /// Starts a transition that fades the screen in (from masked to visible).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-in completes.</param>
        public void FadeIn(Action onComplete = null)
        {
            StartFade(CutoffVisible, CutoffHidden, onComplete);
        }

        /// <summary>
        /// Starts a transition that fades the screen out (from visible to masked).
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the fade-out completes.</param>
        public void FadeOut(Action onComplete = null)
        {
            StartFade(CutoffHidden, CutoffVisible, onComplete);
        }

        private void StartFade(float startCutoff, float endCutoff, Action onComplete)
        {
            // Skip if another fade is running
            if (fadeRoutine != null)
            {
                return;
            }
              
            fadeRoutine = StartCoroutine(FadeRoutine(startCutoff, endCutoff, onComplete));
        }

        private IEnumerator FadeRoutine(float startCutoff, float endCutoff, Action onComplete)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float progressTime = Mathf.Clamp01(elapsedTime / duration);
                SetCutoff(Mathf.Lerp(startCutoff, endCutoff, progressTime));
                yield return null;
            }

            SetCutoff(endCutoff);
            fadeRoutine = null;
            onComplete?.Invoke();
        }

        private void SetCutoff(float value)
        {
            runtimeMaterial.SetFloat(CutoffProperty, value);
        }
    }
}
