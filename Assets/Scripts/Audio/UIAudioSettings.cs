using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Audio
{
    /// <summary>
    /// Holds the sound effects used for UI interactions, 
    /// including selection, confirmation, and back.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Settings/UI Audio Settings")]
    internal sealed class UIAudioSettings : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Sound played when navigating or selecting a UI element.")]
        private AudioClip selectSfx;

        [SerializeField, Required, Tooltip("Sound played when confirming an action in the UI.")]
        private AudioClip confirmSfx;

        [SerializeField, Required, Tooltip("Sound played when cancelling or going back in a UI screen.")]
        private AudioClip backSfx;

        internal AudioClip SelectSfx => selectSfx;
        internal AudioClip ConfirmSfx => confirmSfx;
        internal AudioClip BackSfx => backSfx;
    }
}
