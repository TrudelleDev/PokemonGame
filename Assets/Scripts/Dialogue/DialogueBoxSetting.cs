using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Dialogue
{
    /// <summary>
    /// Defines configurable visual, audio, and behavioral settings 
    /// for a dialogue box instance.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueBoxSetting", menuName = "PokemonGame/Dialogue/Setting")]
    public class DialogueBoxSetting : ScriptableObject
    {
        private const string GroupVisuals = "Visuals";
        private const string GroupAudio = "Audio";
        private const string GroupSettings = "Settings";

        [BoxGroup(GroupVisuals), SerializeField, Required]
        [Tooltip("Background sprite for the dialogue box.")]
        private Sprite boxSprite;

        [BoxGroup(GroupVisuals), SerializeField, Required]
        [Tooltip("Font used for the dialogue text in this theme.")]
        private TMP_FontAsset font;

        [BoxGroup(GroupVisuals), SerializeField]
        [Tooltip("Custom padding applied to the dialogue box RectTransform.")]
        private RectPadding rectPadding;

        [BoxGroup(GroupAudio), SerializeField, Required]
        [Tooltip("Sound effect played when a new dialogue line starts.")]
        private AudioClip dialogueSfx;

        [BoxGroup(GroupSettings), SerializeField, MinValue(0.01f)]
        [Tooltip("Delay between characters during the typewriter effect (in seconds).")]
        private float characterDelay = 0.05f;

        [BoxGroup(GroupSettings), SerializeField]
        [Tooltip("If true, the dialogue box automatically closes after the final line.")]
        private bool autoClose = true;

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

        /// <summary>
        /// Gets the sound effect played when a new dialogue line begins.
        /// </summary>
        public AudioClip DialogueSfx => dialogueSfx;

        /// <summary>
        /// Gets the delay (in seconds) between each character appearing on screen.
        /// </summary>
        public float CharacterDelay => characterDelay;

        /// <summary>
        /// Determines whether the dialogue box closes automatically after finishing all lines.
        /// </summary>
        public bool AutoClose => autoClose;
    }
}
