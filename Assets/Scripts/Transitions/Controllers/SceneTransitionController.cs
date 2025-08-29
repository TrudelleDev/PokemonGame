using System.Collections;
using PokemonGame.Characters.Spawn;
using PokemonGame.Characters.Spawn.Enums;
using PokemonGame.Pause;
using PokemonGame.Transitions.Constants;
using PokemonGame.Transitions.Enums;
using PokemonGame.Transitions.Extensions;
using PokemonGame.Transitions.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Handles cross-scene transitions with optional fade in/out and spawn placement.
    /// </summary>
    public class SceneTransitionController : MonoBehaviour
    {
        private bool isTransitioning;

        private void Awake()
        {
            ServiceLocator.Register(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<SceneTransitionController>();
        }

        /// <summary>
        /// Starts a transition into the given scene at the given spawn location.
        /// If transitionType is None, performs an instant swap.
        /// </summary>
        public void StartTransition(string sceneToLoadName, SpawnLocationID spawnLocation, TransitionType transitionType)
        {
            if (isTransitioning)
                return;

            ITransition transition = TransitionResolver.Resolve(transitionType);

            isTransitioning = true;
            StartCoroutine(RunTransition(sceneToLoadName, spawnLocation, transition));
        }

        private IEnumerator RunTransition(string sceneToLoadName, SpawnLocationID spawnLocation, ITransition transition)
        {
            Scene sourceScene = SceneManager.GetActiveScene();
            PauseManager.SetPaused(true);

            // Store spawn location for PlayerSpawner
            SpawnLocationManager.Instance.SetNextSpawnLocation(spawnLocation);

            // Optional fade in
            if (transition != null)
            {
                yield return transition.FadeInCoroutine();
            }

            // Load target scene
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
            yield return sceneLoadOperation;

            Scene targetScene = SceneManager.GetSceneByName(sceneToLoadName);
            if (targetScene.IsValid())
            {
                SceneManager.SetActiveScene(targetScene);
            }

            // Unload previous scene
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sourceScene);
            if (unloadSceneOperation != null)
            {
                yield return unloadSceneOperation;
            }

            // Optional fade out
            if (transition != null)
            {
                yield return new WaitForSecondsRealtime(TransitionConstants.HoldOnBlackSeconds);
                yield return transition.FadeOutCoroutine();
            }

            isTransitioning = false;
            PauseManager.SetPaused(false);
        }
    }
}
