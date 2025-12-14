using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace PokemonGame.Audio
{
    [DisallowMultipleComponent]
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Mixer Group")]
        [SerializeField, Required] private AudioMixerGroup bgmGroup;
        [SerializeField, Required] private AudioMixerGroup sfxGroup;

        private AudioSource bgmSource;
        private AudioSource sfxSource;
        private AudioSource sfxStoppableSource; // new source for stoppable SFX

        protected override void Awake()
        {
            base.Awake();
            CreateAudioSources();
        }

        #region BGM
        public void PlayBGM(AudioClip clip)
        {
            if (clip == null)
                return;

            if (bgmSource.clip == clip && bgmSource.isPlaying)
                return;

            clip.LoadAudioData();
            bgmSource.clip = clip;

            double startTime = AudioSettings.dspTime;
            bgmSource.PlayScheduled(startTime);
        }

        public void StopBGM()
        {
            if (bgmSource.isPlaying)
                bgmSource.Stop();
        }

        public void SetBGMStartTime(float timeSeconds)
        {
            if (bgmSource.clip == null) return;
            bgmSource.time = Mathf.Clamp(timeSeconds, 0f, bgmSource.clip.length);
        }
        #endregion

        #region SFX
        /// <summary>
        /// Plays a short one-shot SFX (cannot be stopped mid-play).
        /// </summary>
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null)
                return;

            sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Plays a stoppable SFX and waits until it finishes.
        /// Usage: yield return AudioManager.Instance.PlaySFXAndWait(clip);
        /// </summary>
        public IEnumerator PlaySFXAndWait(AudioClip clip)
        {
            if (clip == null)
                yield break;

            if (sfxStoppableSource.isPlaying)
                sfxStoppableSource.Stop();

            sfxStoppableSource.clip = clip;
            sfxStoppableSource.Play();

            // Wait until the clip finishes
            yield return new WaitUntil(() => !sfxStoppableSource.isPlaying);
        }

        /// <summary>
        /// Plays a SFX that can be stopped manually.
        /// </summary>
        public void PlaySFXStoppable(AudioClip clip)
        {
            if (clip == null)
                return;

            // Stop previous clip if playing
            if (sfxStoppableSource.isPlaying)
                sfxStoppableSource.Stop();

            sfxStoppableSource.clip = clip;
            sfxStoppableSource.Play();
        }

        /// <summary>
        /// Stops the currently playing stoppable SFX.
        /// </summary>
        public void StopSFX()
        {
            if (sfxStoppableSource.isPlaying)
                sfxStoppableSource.Stop();
        }
        #endregion

        private void CreateAudioSources()
        {
            // BGM Source
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup = bgmGroup;

            // Short SFX Source
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxGroup;

            // Stoppable SFX Source
            sfxStoppableSource = gameObject.AddComponent<AudioSource>();
            sfxStoppableSource.outputAudioMixerGroup = sfxGroup;
        }
    }
}
