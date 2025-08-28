using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Audio
{
    /// <summary>
    /// Plays background music automatically when a scene is loaded.
    /// </summary>
    [DisallowMultipleComponent]
    public class SceneBGMPlayer : MonoBehaviour
    {
        [Header("BGM Settings")]
        [SerializeField, Required]
        [Tooltip("The background music that should play when this scene is loaded.")]
        AudioClip bgmClip;

        private void Start()
        {
            if (bgmClip != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayBGM(bgmClip);
            }
        }     
    }
}
