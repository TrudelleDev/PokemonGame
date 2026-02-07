using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Audio
{
    /// <summary>
    /// Contains sound effects used by the UI.
    /// Centralizes UI audio clips for easy reuse and balancing.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Settings/Audio Settings")]
    internal sealed class AudioSettings : ScriptableObject
    {
        [Title("UI Sound Effects")]
        [SerializeField, Required]
        [Tooltip("Sound played when navigating or selecting a UI element.")]
        private AudioClip uiSelectSfx;

        [SerializeField, Required]
        [Tooltip("Sound played when confirming an action in the UI.")]
        private AudioClip uiConfirmSfx;

        [SerializeField, Required]
        [Tooltip("Sound played when cancelling or returning from a UI screen.")]
        private AudioClip uiReturnSfx;

        /// <summary>
        /// Sound effect used when selecting or highlighting a UI element.
        /// </summary>
        internal AudioClip UISelectSfx => uiSelectSfx;

        /// <summary>
        /// Sound effect used when confirming a UI action.
        /// </summary>
        internal AudioClip UIConfirmSfx => uiConfirmSfx;

        /// <summary>
        /// Sound effect used when cancelling or returning from a UI screen.
        /// </summary>
        internal AudioClip UIReturnSfx => uiReturnSfx;
    }
}
