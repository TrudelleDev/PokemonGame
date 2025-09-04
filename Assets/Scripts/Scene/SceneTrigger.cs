using PokemonGame.Audio;
using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Transitions.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using PokemonGame.Transitions.Enums;
using PokemonGame.MapEntry.Enums;
using PokemonGame.Characters.Core;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame
{
    /// <summary>
    /// Trigger that transitions the player between scenes (e.g., entering/exiting a building, cave, or route).
    /// </summary>
    public class SceneTrigger : MonoBehaviour, ITriggerable
    {
#if UNITY_EDITOR
        [Header("Scenes (Editor Only)")]
        [SerializeField, Required]
        [Tooltip("Scene asset to load when the trigger is activated.")]
        private SceneAsset sceneToLoad;
#endif

        [Header("Spawn")]
        [SerializeField, Required]
        [Tooltip("The spawn location in the target scene where the player will appear.")]
        private MapEntryID targetSpawnLocation;

        [Header("Transition")]
        [SerializeField, Required]
        [Tooltip("Type of transition effect used when changing scenes.")]
        private TransitionType transitionType;

        [Header("Audio")]
        [SerializeField]
        private bool hasAudio;

        [SerializeField, ShowIf("hasAudio")]
        [Tooltip("Sound effect played when transitioning.")]
        private AudioClip transitionClip;

        private string sceneToLoadName;
        private SceneTransitionController transitionController;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Auto-populate scene name whenever SceneAsset is changed
            if (sceneToLoad != null)
            {
                sceneToLoadName = sceneToLoad.name;
            }
        }
#endif

        private void Awake()
        {
            transitionController = ServiceLocator.Get<SceneTransitionController>();
        }

        /// <summary>
        /// Called when a character interacts with the trigger. 
        /// Plays audio and transitions to the target scene at the defined spawn point.
        /// </summary>
        /// <param name="character">The character activating the trigger (usually the player).</param>
        public void Trigger(Character character)
        {
            if (hasAudio && transitionClip != null)
            {
                AudioManager.Instance.PlaySFX(transitionClip);
            }

            transitionController.StartTransition(sceneToLoadName, targetSpawnLocation, transitionType);
        }
    }
}
