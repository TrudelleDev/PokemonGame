using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu.UI
{
    /// <summary>
    /// A menu button that tints its text and/or image
    /// based on selection and interactable state.
    /// </summary>
    public class TintMenuButton : MenuButton
    {
        [Title("Targets")]
        [Tooltip("Optional: Text to tint when selected.")]
        [SerializeField] private TextMeshProUGUI targetText;

        [Tooltip("Optional: Image to tint when selected.")]
        [SerializeField] private Graphic targetImage;

        [Title("Colors")]
        [SerializeField, ColorUsage(false)]
        [Tooltip("Color used when button is not selected.")]
        private Color normalColor = Color.white;

        [SerializeField, ColorUsage(false)]
        [Tooltip("Color used when button is selected.")]
        private Color highlightedColor = Color.white;

        [SerializeField, ColorUsage(false)]
        [Tooltip("Color used when button is disabled.")]
        private Color disabledColor = Color.white;

        private void Awake()
        {
            ApplyColor(normalColor);
        }

        /// <summary>
        /// Applies tint color based on selection and interactable state.
        /// </summary>
        protected override void RefreshVisual()
        {
            if (!IsInteractable)
            {
                ApplyColor(disabledColor);
                return;
            }

            ApplyColor(IsSelected ? highlightedColor : normalColor);
        }

        /// <summary>
        /// Applies the given color to the assigned text and image.
        /// </summary>
        /// <param name="color">The color to apply.</param>
        private void ApplyColor(Color color)
        {
            if (targetText != null)
            {
                targetText.color = color;
            }

            if (targetImage != null)
            {
                targetImage.color = color;
            }
        }
    }
}
