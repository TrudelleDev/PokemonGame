using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PokemonGame.Battle
{
    [RequireComponent(typeof(Image))]
    public class CoverOffsetAnimator : MonoBehaviour
    {
        public Image image;
        public Material material; // assign in inspector

        private Material originalMaterial;
        private Material runtimeMaterial;

        void Awake()
        {
            image ??= GetComponent<Image>();
            originalMaterial = image.material; // save original UI material
        }

        /// <summary>
        /// Set shader offset on the runtime material
        /// </summary>
        private void SetOffset(float x, float y)
        {
            if (runtimeMaterial != null)
            {
                runtimeMaterial.SetFloat("_CoverOffsetX", x);
                runtimeMaterial.SetFloat("_CoverOffsetY", y);
            }
        }

        /// <summary>
        /// Start an offset animation (non-yield version)
        /// </summary>
        public void AnimateOffset(Vector2 start, Vector2 end, float duration)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateOffsetCoroutine(start, end, duration));
        }

        /// <summary>
        /// Animate offset over time, yielding until finished
        /// </summary>
        public IEnumerator AnimateOffsetCoroutine(Vector2 start, Vector2 end, float duration)
        {
            // Assign temporary runtime material
            runtimeMaterial = new Material(material);
            image.material = runtimeMaterial;

            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(timer / duration);
                Vector2 current = Vector2.Lerp(start, end, t);
                SetOffset(current.x, current.y);
                yield return null;
            }

            // Ensure final value
            SetOffset(end.x, end.y);

            // Cleanup
            image.material = originalMaterial;
            Destroy(runtimeMaterial);
            runtimeMaterial = null;
        }
    }
}
