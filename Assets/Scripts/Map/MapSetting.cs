using PokemonGame.Audio;
using PokemonGame.Battle;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Map
{
    /// <summary>
    /// Defines the map's audio and dialogue configuration.
    /// Plays the assigned BGM when the scene loads and reapplies
    /// the map's dialogue style after battles.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MapSetting : MonoBehaviour
    {
        [SerializeField, Required]
        [LabelText("Background Music")]
        [Tooltip("Background music to play when this map is loaded.")]
        private AudioClip bgmClip;

        [SerializeField, Required]
        [Tooltip("Dialogue box style used on this map.")]
        private DialogueBoxTheme dialogueSetting;

        private BattleView battleView;

        private void Start()
        {
            ApplyMapSetting();

            battleView = ViewManager.Instance.Get<BattleView>();

            if (battleView != null)
            {
                battleView.OnBattleViewClose += ApplyMapSetting;
            }
        }

        private void OnDestroy()
        {
            if (battleView != null)
            {
                battleView.OnBattleViewClose -= ApplyMapSetting;
            }
        }

        private void ApplyMapSetting()
        {
            if (AudioManager.Instance != null && bgmClip != null)
            {
                AudioManager.Instance.PlayBGM(bgmClip);
            }

            if (OverworldDialogueBox.Instance.Dialogue != null && dialogueSetting != null)
            {
                OverworldDialogueBox.Instance.Dialogue.ApplyTheme(dialogueSetting);
            }
        }
    }
}
