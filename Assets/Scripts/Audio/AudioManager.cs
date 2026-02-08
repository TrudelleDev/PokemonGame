using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace MonsterTamer.Audio
{
    /// <summary>
    /// Central audio controller responsible for playing background music (BGM),
    /// sound effects (SFX), and UI sound effects.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class AudioManager : Singleton<AudioManager>
    {
        [SerializeField, Required]
        [Tooltip("Audio mixer group used for background music.")]
        private AudioMixerGroup bgmMixerGroup;

        [SerializeField, Required]
        [Tooltip("Audio mixer group used for world and gameplay sound effects.")]
        private AudioMixerGroup sfxMixerGroup;

        [SerializeField, Required]
        [Tooltip("Audio mixer group used for UI sound effects.")]
        private AudioMixerGroup uiMixerGroup;

        private AudioSource bgmSource;
        private AudioSource sfxSource;
        private AudioSource uiSource;

        /// <summary>
        /// Initializes the AudioManager and creates the required audio sources.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            CreateAudioSources();
        }

        /// <summary>
        /// Plays the specified background music clip.
        /// If the same clip is already playing, the call is ignored.
        /// </summary>
        /// <param name="clip">The audio clip to play as background music.</param>
        internal void PlayBGM(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            if (bgmSource.clip == clip && bgmSource.isPlaying)
            {
                return;
            }

            bgmSource.clip = clip;

            double startTime = global::UnityEngine.AudioSettings.dspTime;
            bgmSource.PlayScheduled(startTime);
        }

        /// <summary>
        /// Plays a one-shot gameplay sound effect.
        /// </summary>
        /// <param name="clip">The audio clip to play as a sound effect.</param>
        internal void PlaySFX(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Plays a one-shot UI sound effect.
        /// </summary>
        /// <param name="clip">The audio clip to play for a UI action.</param>
        internal void PlayUISFX(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            uiSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Creates and configures the audio sources used for playback.
        /// </summary>
        private void CreateAudioSources()
        {
            // Background music source
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup = bgmMixerGroup;

            // Gameplay SFX source
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;

            // UI SFX source
            uiSource = gameObject.AddComponent<AudioSource>();
            uiSource.outputAudioMixerGroup = uiMixerGroup;
        }
    }
}
