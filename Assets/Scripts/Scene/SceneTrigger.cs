using PokemonGame.Audio;
using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Characters.Spawn.Enums;
using PokemonGame.Transitions.Controllers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Trigger that transitions the player between scenes (e.g., entering/exiting a building, cave, or route).
    /// </summary>
    public class SceneTrigger : MonoBehaviour, ITrigger
    {
#if UNITY_EDITOR
        [Header("Scene Settings")]
        [SerializeField, Required]
        [Tooltip("The scene to load when the trigger is activated.")]
        private SceneAsset sceneToLoad;
#endif

        [Header("Spawn Settings")]
        [SerializeField, Required]
        [Tooltip("The spawn location in the target scene where the player will appear.")]
        private SpawnLocationID targetSpawnLocation;

        [Header("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect played when transitioning (e.g., entering/exiting a building).")]
        private AudioClip transitionClip;

        private string sceneToLoadName;
        private SceneTransitionController transitionController;

#if UNITY_EDITOR
        private void OnValidate()
        {
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
            transitionController = ServiceLocator.Get<SceneTransitionController>();

            AudioManager.Instance.PlaySFX(transitionClip);
            transitionController.StartTransition(sceneToLoadName, targetSpawnLocation);
        }
    }
}
