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
    /// A trigger that initiates a scene change when activated by the player.
    /// </summary>
    public class SceneTrigger : MonoBehaviour, ITrigger
    {
#if UNITY_EDITOR
        [Header("Scene Settings")]
        [SerializeField, Required] private SceneAsset sceneToLoad;
#endif
        [Header("Spawn Settings")]
        [SerializeField, Required] private SpawnLocationID targetSpawnLocation;

        private string sceneToLoadName;
        private SceneTransitionController transitionController;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (sceneToLoad != null) sceneToLoadName = sceneToLoad.name;
        }
#endif

        private void Awake()
        {
            transitionController = ServiceLocator.Get<SceneTransitionController>();
        }

        public void Trigger(Character character)
        {
            transitionController = ServiceLocator.Get<SceneTransitionController>();

            transitionController.StartTransition(sceneToLoadName, targetSpawnLocation);
        }
    }
}
