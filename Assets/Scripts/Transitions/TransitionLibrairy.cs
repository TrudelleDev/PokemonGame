using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Transitions
{
    /// <summary>
    /// Central registry for all transitions (scene + UI).
    /// Provides lookup by <see cref="TransitionType"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class TransitionLibrary : Singleton<TransitionLibrary>
    {
        [Title("Transition Controllers")]
        [SerializeField, Required] private Transition alphaFadeController;
        [SerializeField, Required] private Transition battleFlashController;

        /// <summary>
        /// Resolves a TransitionType into its corresponding Transition instance.
        /// </summary>
        public Transition Resolve(TransitionType type)
        {
            return type switch
            {
                TransitionType.AlphaFade => alphaFadeController,
                TransitionType.BattleIntro  => battleFlashController,
                _ => null
            };
        }
    }
}
