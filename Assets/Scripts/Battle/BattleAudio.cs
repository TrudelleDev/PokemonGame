using MonsterTamer.Audio;
using MonsterTamer.Move;
using UnityEngine;

namespace MonsterTamer.Battle
{
    /// <summary>
    /// Handles playing audio cues during battle, such as move sounds.
    /// </summary>
    internal class BattleAudio : MonoBehaviour
    {
        /// <summary>
        /// Plays the sound associated with a specific move.
        /// </summary>
        /// <param name="move">The move whose sound should be played.</param>
        public void PlayMoveSound(MoveDefinition move)
        {
            if (move?.Sound == null)
            {
                return; // No sound to play
            }

            AudioManager.Instance.PlaySFX(move.Sound);
        }
    }
}
