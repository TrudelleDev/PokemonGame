using PokemonGame.Transitions.Controllers;
using PokemonGame.Transitions.Enums;
using PokemonGame.Transitions.Interfaces;
using UnityEngine;

namespace PokemonGame.Transitions
{
    /// <summary>
    /// Resolves a TransitionType enum into the actual transition implementation.
    /// Keeps all mapping logic in one place.
    /// </summary>
    public static class TransitionResolver
    {
        public static ITransition Resolve(TransitionType type)
        {
            switch (type)
            {
                case TransitionType.AlphaFade:
                    return ServiceLocator.Get<AlphaFadeController>();

                case TransitionType.MaskedFade:
                    return ServiceLocator.Get<MaskedFadeController>();

                case TransitionType.None:
                    return null;

                default:
                    Debug.LogWarning($"[TransitionResolver] Unsupported transition type: {type}");
                    return null;
            }
        }
    }
}
