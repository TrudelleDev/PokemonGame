using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Audio
{
    [CreateAssetMenu(menuName = "MonsterTamer/Settings/Audio Setting")]
    public class AudioSetting : ScriptableObject
    {
        [SerializeField, Required]
        private AudioClip uiSelectClip;

        [SerializeField, Required]
        private AudioClip uiConfirmClip;

        [SerializeField, Required]
        private AudioClip uiReturnClip;

        public AudioClip UISelectClip => uiSelectClip;
        public AudioClip UIConfirmClip => uiConfirmClip;
        public AudioClip UIReturnClip => uiReturnClip;
    }
}
