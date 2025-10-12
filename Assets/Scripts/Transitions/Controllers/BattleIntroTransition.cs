using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Pokémon-style battle intro transition.
    /// Plays a sequence of screen flashes, then closes a mask to hide the screen.
    /// After the next view loads, the mask automatically disappears.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleIntroTransition : Transition
    {
        private static readonly int FlashStrength = Shader.PropertyToID("_FlashStrength");
        private static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");
        private static readonly int FlashColorProperty = Shader.PropertyToID("_Color");

        [Title("Settings")]

        [SerializeField, Range(1, 10)]
        [Tooltip("Number of flash cycles before the mask fade begins.")]
        private int flashCount = 3;

        [SerializeField, Min(0.01f)]
        [Tooltip("Duration of a single flash cycle in seconds.")]
        private float flashDuration = 0.1f;

        [SerializeField, Min(0.05f)]
        [Tooltip("Duration of the mask fade animation in seconds.")]
        private float maskDuration = 0.8f;

        [SerializeField, Min(0f)]
        [Tooltip("How long the mask stays fully visible before it disappears.")]
        private float holdDuration = 1f;

        [SerializeField]
        [Tooltip("Color of the flash overlay. Default is light gray.")]
        private Color flashColor = Color.gray;

        [Title("Images")]

        [SerializeField, Required]
        [Tooltip("UI Image using a shader with a '_FlashStrength' property for screen flashes.")]
        private Image flashImage;

        [SerializeField, Required]
        [Tooltip("UI Image using a shader with a '_Cutoff' property for masking transitions.")]
        private Image maskImage;

        private Material flashRuntimeMaterial;
        private Material maskRuntimeMaterial;
        private Coroutine routine;

        /// <summary>
        /// Initializes runtime materials and sets default shader values.
        /// </summary>
        private void Awake()
        {
            // Duplicate materials to avoid modifying shared assets
            flashRuntimeMaterial = new Material(flashImage.material);
            maskRuntimeMaterial = new Material(maskImage.material);

            // Assign runtime instances to the UI images
            flashImage.material = flashRuntimeMaterial;
            maskImage.material = maskRuntimeMaterial;

            // Initialize shader properties
            flashRuntimeMaterial.SetFloat(FlashStrength, 0f);
            flashRuntimeMaterial.SetColor(FlashColorProperty, flashColor);
            maskRuntimeMaterial.SetFloat(CutoffProperty, 1f); // start fully hidden

            flashImage.enabled = false;
            maskImage.enabled = true;
        }

        /// <summary>
        /// Executes the "fade in" phase:
        /// screen flashes → mask closes (screen hidden).
        /// </summary>
        /// <param name="onComplete">Callback invoked when the fade completes.</param>
        protected override void FadeInInternal(Action onComplete)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(RunFadeIn(onComplete));
        }

        /// <summary>
        /// Executes the "fade out" phase:
        /// mask remains for a short hold, then disappears.
        /// </summary>
        protected override void FadeOutInternal(Action onComplete)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(RunFadeOut(onComplete));
        }

        /// <summary>
        /// Coroutine for the intro sequence:
        /// performs flashing and then fades the mask in.
        /// </summary>
        private IEnumerator RunFadeIn(Action onComplete)
        {
            flashImage.enabled = true;

            // Flash sequence
            for (int i = 0; i < flashCount; i++)
            {
                yield return AnimateMaterialFloat(flashRuntimeMaterial, FlashStrength, 0f, 1f, flashDuration * 0.5f);
                yield return AnimateMaterialFloat(flashRuntimeMaterial, FlashStrength, 1f, 0f, flashDuration * 0.5f);
            }

            flashImage.enabled = false;

            // Mask fade in (closing)
            yield return AnimateMaterialFloat(maskRuntimeMaterial, CutoffProperty, 1f, 0f, maskDuration);

            onComplete?.Invoke();
        }

        /// <summary>
        /// Coroutine for the outro sequence:
        /// waits for the hold duration, then instantly hides the mask.
        /// </summary>
        private IEnumerator RunFadeOut(Action onComplete)
        {
            onComplete?.Invoke();

            if (holdDuration > 0f)
            {
                yield return new WaitForSecondsRealtime(holdDuration);
            }

            maskRuntimeMaterial.SetFloat(CutoffProperty, 1f);
        }

        /// <summary>
        /// Linearly animates a material's float property from one value to another over the given duration.
        /// Commonly used for effects like flashes or masking transitions.
        /// </summary>
        /// <param name="material">The material whose property will be animated.</param>
        /// <param name="propertyId">The shader property ID to modify.</param>
        /// <param name="from">Starting float value.</param>
        /// <param name="to">Target float value to reach.</param>
        /// <param name="duration">Time in seconds over which the animation occurs.</param>
        private IEnumerator AnimateMaterialFloat(Material material, int propertyId, float from, float to, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float time = Mathf.Clamp01(elapsed / duration);
                material.SetFloat(propertyId, Mathf.Lerp(from, to, time));
                yield return null;
            }

            material.SetFloat(propertyId, to);
        }
    }
}
