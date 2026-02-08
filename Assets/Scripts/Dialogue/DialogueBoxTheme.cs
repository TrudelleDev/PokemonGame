using MonsterTamer.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Dialogue
{
    /// <summary>
    /// Defines the visual and layout properties used to style a <see cref="DialogueBox"/>.
    /// Contains configurable assets such as the box background, font, and layout padding.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueBoxTheme", menuName = "PokemonGame/Dialogue/Theme")]
    public class DialogueBoxTheme : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Background sprite for the dialogue box.")]
        private Sprite boxSprite;

        [SerializeField, Required]
        [Tooltip("Font used for the dialogue text in this theme.")]
        private TMP_FontAsset font;

        [SerializeField]
        [Tooltip("Custom padding applied to the dialogue box RectTransform.")]
        private RectPadding rectPadding;

        /// <summary>
        /// Gets the background sprite used by this dialogue box theme.
        /// </summary>
        public Sprite BoxSprite => boxSprite;

        /// <summary>
        /// Gets the font used by this dialogue box theme.
        /// </summary>
        public TMP_FontAsset Font => font;

        /// <summary>
        /// Gets the padding applied to the dialogue box RectTransform.
        /// </summary>
        public RectPadding RectPadding => rectPadding;
    }
}
