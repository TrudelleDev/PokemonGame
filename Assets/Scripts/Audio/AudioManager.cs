using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace MonsterTamer.Audio
{
    /// <summary>
    /// Manages all game audio: BGM, SFX, and UI sounds.
    /// Creates audio sources and routes them through the correct mixer groups.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class AudioManager : Singleton<AudioManager>
    {
        [SerializeField, Required, Tooltip("Audio mixer group used for background music.")]
        private AudioMixerGroup bgmMixerGroup;

        [SerializeField, Required, Tooltip("Audio mixer group used for world and gameplay sound effects.")]
        private AudioMixerGroup sfxMixerGroup;

        [SerializeField, Required, Tooltip("Audio mixer group used for UI sound effects.")]
        private AudioMixerGroup uiMixerGroup;

        private AudioSource bgmSource;
        private AudioSource sfxSource;
        private AudioSource uiSource;

        protected override void Awake()
        {
            base.Awake();
            CreateAudioSources();
        }

        /// <summary>
        /// Plays the specified background music clip. 
        /// If the same clip is already playing, it will be ignored.
        /// </summary>
        internal void PlayBGM(AudioClip clip)
        {
            if (clip == null) return;
            if (bgmSource.clip == clip && bgmSource.isPlaying) return;

            bgmSource.clip = clip;
            double startTime = AudioSettings.dspTime;
            bgmSource.PlayScheduled(startTime);
        }

        /// <summary>
        /// Plays a one-shot gameplay sound effect.
        /// </summary>
        internal void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;

            sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Plays a one-shot UI sound effect.
        /// </summary>
        internal void PlayUISFX(AudioClip clip)
        {
            if (clip == null) return;

            uiSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Creates and configures audio sources for BGM, SFX, and UI.
        /// </summary>
        private void CreateAudioSources()
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup = bgmMixerGroup;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;

            uiSource = gameObject.AddComponent<AudioSource>();
            uiSource.outputAudioMixerGroup = uiMixerGroup;
        }
    }
}
