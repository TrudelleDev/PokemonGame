using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Audio
{
    /// <summary>
    /// Defines sound effects used by the UI.
    /// Centralizes UI selection, confirmation, and return audio
    /// for consistent feedback and easy tuning.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Settings/UI Audio Settings")]
    internal sealed class UIAudioSettings : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Sound played when navigating or selecting a UI element.")]
        private AudioClip selectSfx;

        [SerializeField, Required]
        [Tooltip("Sound played when confirming an action in the UI.")]
        private AudioClip confirmSfx;

        [SerializeField, Required]
        [Tooltip("Sound played when cancelling or returning from a UI screen.")]
        private AudioClip returnSfx;

        /// <summary>
        /// Sound effect used when selecting or highlighting a UI element.
        /// </summary>
        internal AudioClip SelectSfx => selectSfx;

        /// <summary>
        /// Sound effect used when confirming a UI action.
        /// </summary>
        internal AudioClip ConfirmSfx => confirmSfx;

        /// <summary>
        /// Sound effect used when cancelling or returning from a UI screen.
        /// </summary>
        internal AudioClip ReturnSfx => returnSfx;
    }
}
