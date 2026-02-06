using PokemonGame.Audio;
using PokemonGame.Move;
using UnityEngine;

namespace PokemonGame.Battle
{
    internal class BattleAudio : MonoBehaviour
    {
        public void PlayMoveSound(MoveDefinition move)
        {
            if (move.Sound == null)
            {
                return;
            }

            AudioManager.Instance.PlaySFX(move.Sound);
        }
    }
}
