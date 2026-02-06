using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Move;
using PokemonGame.Pokemon;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleAudio : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Sounds to play based on move type effectiveness.")]
        private TypeEffectivenessSounds effectivenessSounds;


        public void PlayMoveSound(MoveDefinition move)
        {

        }

        /// <summary>
        /// Plays the sound corresponding to the type effectiveness of a move.
        /// </summary>
        /// <param name="effectiveness">The effectiveness of the move.</param>
        public void PlayEffectivenessSound(TypeEffectiveness effectiveness)
        {
            AudioManager.Instance.PlaySFX(effectivenessSounds.GetEffectivenessSound(effectiveness));
        }
    }
}
