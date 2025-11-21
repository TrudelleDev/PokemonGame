using PokemonGame.Audio;
using PokemonGame.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleAudio : MonoBehaviour
    {
        [SerializeField, Required]
        private AudioClip victorySfx;

        [SerializeField, Required]
        private AudioClip runAwaySfx;

        [SerializeField, Required]
        private AudioClip levelUpSfx;

        [SerializeField, Required]
        private AudioClip lowHealthSfx;

        [SerializeField, Required]
        private AudioClip doDamageNormalSfx;

        [SerializeField, Required]
        private AudioClip gainExperienceSfx;

        [SerializeField, Required]
        private AudioClip openPokeballSfx;

        public void PlayDoDamageNomral() => AudioManager.Instance.PlaySFX(doDamageNormalSfx);
        public void PlayVictory() => AudioManager.Instance.PlayBGM(victorySfx);
        public void PlayRunAwaySfx() => AudioManager.Instance.PlaySFX(runAwaySfx);
        public void PlayLevelUpSfx() => AudioManager.Instance.PlaySFX(levelUpSfx);
        public void PlayLowHealthSfx() => AudioManager.Instance.PlaySFX(lowHealthSfx);
        public void PlayGainExperienceSfx() => AudioManager.Instance.PlaySFXStoppable(gainExperienceSfx);
        public void PlayOpenPokeballSfx() => AudioManager.Instance.PlaySFX(openPokeballSfx);

        public void PlayPokemonCry(PokemonInstance pokemon)
        {
            AudioManager.Instance.PlaySFX(pokemon.Definition.CryClip);
        }

    }
}
