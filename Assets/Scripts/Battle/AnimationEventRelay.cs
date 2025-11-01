using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Relays animation events from Animator clips to the <see cref="BattleAnimation"/> system.
    /// Used to trigger logical flags (e.g., Pokéball throw) during battle animations.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AnimationEventRelay : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the active BattleAnimation sequence.")]
        private BattleAnimation sequence;

        /// <summary>
        /// Called by an animation event during the Pokéball throw animation.
        /// Sets a flag in <see cref="BattleAnimation"/> indicating that the throw has occurred.
        /// </summary>
        public void OnThrowEvent()
        {
            if (sequence != null)
            {
                sequence.ThrowPokeball = true;
            }
        }
    }
}
