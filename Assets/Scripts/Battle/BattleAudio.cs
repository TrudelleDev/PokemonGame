using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Pokemon;
using PokemonGame.Type;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Centralized manager for playing all battle-related audio, including
    /// sound effects, background music, and Pokémon cries.
    /// </summary>
    public class BattleAudio : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Clip played when the player wins a battle.")]
        private AudioClip victoryClip;

        [SerializeField, Required, Tooltip("Clip played when the player runs away from battle.")]
        private AudioClip runAwayClip;

        [SerializeField, Required, Tooltip("Clip played when a Pokémon levels up.")]
        private AudioClip levelUpClip;

        [SerializeField, Required, Tooltip("Clip played when a Pokémon has low health.")]
        private AudioClip lowHealthClip;

        [SerializeField, Required, Tooltip("Clip played when gaining experience.")]
        private AudioClip gainExperienceClip;

        [SerializeField, Required, Tooltip("Clip played when opening a Pokéball.")]
        private AudioClip openPokeballClip;

        [SerializeField, Required, Tooltip("Sounds to play based on move type effectiveness.")]
        private TypeEffectivenessSounds effectivenessSounds;

        /// <summary>
        /// Plays the victory background music.
        /// </summary>
        public void PlayVictory() => AudioManager.Instance.PlayBGM(victoryClip);

        /// <summary>
        /// Plays the run-away sound effect.
        /// </summary>
        public void PlayRunAway() => AudioManager.Instance.PlaySFX(runAwayClip);

        /// <summary>
        /// Plays the level-up sound effect.
        /// </summary>
        public void PlayLevelUp() => AudioManager.Instance.PlaySFX(levelUpClip);

        /// <summary>
        /// Plays the low-health warning sound effect.
        /// </summary>
        public void PlayLowHealth() => AudioManager.Instance.PlaySFX(lowHealthClip);

        /// <summary>
        /// Plays the experience gain sound effect (stoppable).
        /// </summary>
        public void PlayGainExperience() => AudioManager.Instance.PlaySFXStoppable(gainExperienceClip);

        /// <summary>
        /// Plays the Pokéball opening sound effect, then waits briefly.
        /// </summary>
        public IEnumerator PlayOpenPokeball()
        {
            const float delay = 0.1f;
            AudioManager.Instance.PlaySFX(openPokeballClip);
            yield return new WaitForSecondsRealtime(delay);
        }

        /// <summary>
        /// Plays the cry of the specified Pokémon.
        /// </summary>
        /// <param name="pokemon">The Pokémon whose cry to play.</param>
        public void PlayPokemonCry(PokemonInstance pokemon) =>
            AudioManager.Instance.PlaySFX(pokemon.Definition.CryClip);

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
