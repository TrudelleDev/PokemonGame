using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace PokemonGame.Audio
{
    /// <summary>
    /// Central audio manager that handles background music (BGM) 
    /// and sound effects (SFX).
    /// Uses AudioMixer groups for volume control and routing.
    /// </summary>
    [DisallowMultipleComponent]
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Mixer Group")]
        [SerializeField, Required]
        [Tooltip("Audio mixer group for background music.")]
        private AudioMixerGroup bgmGroup;

        [SerializeField, Required]
        [Tooltip("Audio mixer group for sound effects (includes UI sounds).")]
        private AudioMixerGroup sfxGroup;

        private AudioSource bgmSource;
        private AudioSource sfxSource;

        protected override void Awake()
        {
            base.Awake();
            CreateAudioSources();
        }

        /// <summary>
        /// Plays background music. 
        /// If the given clip is already playing, nothing changes.
        /// </summary>
        /// <param name="clip">The music clip to play.</param>
        public void PlayBGM(AudioClip clip)
        {
            if (clip == null || bgmSource.clip == clip)
            {
                return;
            }

            bgmSource.clip = clip;
            bgmSource.Play();
        }

        /// <summary>
        /// Stops the currently playing background music, if any.
        /// </summary>
        public void StopBGM()
        {
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }
        }

        /// <summary>
        /// Plays a one-shot sound effect (includes UI sounds).
        /// </summary>
        /// <param name="clip">The sound effect clip to play.</param>
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            sfxSource.PlayOneShot(clip);
        }

        private void CreateAudioSources()
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup = bgmGroup;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxGroup;
        }
    }
}
