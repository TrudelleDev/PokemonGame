using MonsterTamer.Audio;
using MonsterTamer.Move;
using UnityEngine;

namespace MonsterTamer.Battle
{
    /// <summary>
    /// Plays battle-related audio cues, such as move sounds.
    /// </summary>
    internal sealed class BattleAudio : MonoBehaviour
    {
        /// <summary>
        /// Plays the sound for the specified move, if any.
        /// </summary>
        internal void PlayMoveSound(MoveDefinition move)
        {
            if (move != null && move.Sound != null)
            {
                AudioManager.Instance.PlaySFX(move.Sound);
            }
        }
    }
}
