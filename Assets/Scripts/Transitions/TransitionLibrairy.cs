using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Transitions
{
    /// <summary>
    /// Central registry for all transitions (scene + UI).
    /// Provides lookup by <see cref="TransitionType"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class TransitionLibrary : Singleton<TransitionLibrary>
    {
        [Header("Transition Controllers")]
        [SerializeField, Required] private Transition alphaFadeController;
        [SerializeField, Required] private Transition maskedFadeController;

        /// <summary>
        /// Resolves a TransitionType into its corresponding Transition instance.
        /// </summary>
        public Transition Resolve(TransitionType type)
        {
            return type switch
            {
                TransitionType.AlphaFade => alphaFadeController,
                TransitionType.MaskedFade => maskedFadeController,
                _ => null
            };
        }
    }
}
