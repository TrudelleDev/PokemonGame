using System.Collections;
using PokemonGame.Characters.Spawn;
using PokemonGame.Characters.Spawn.Enums;
using PokemonGame.Pause;
using PokemonGame.Transitions.Constants;
using PokemonGame.Transitions.Extensions;
using PokemonGame.Transitions.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame.Transitions.Controllers
{
    /// <summary>
    /// Handles cross-scene transitions with fade in/out and spawn placement.
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
        /// Chooses either a fade-covered transition or a direct swap depending on availability of AlphaFadeController.
        /// </summary>
        public void StartTransition(string sceneToLoadName, SpawnLocationID spawnLocation)
        {
            // Ignore if a transition is already in progress
            if (isTransitioning)
            {
                return;
            }

            AlphaFadeController fade = ServiceLocator.Get<AlphaFadeController>();

            if (fade == null)
            {
                // No fade system available fall back to a direct swap
                StartCoroutine(TransitionWithoutFade(sceneToLoadName, spawnLocation));
                return;
            }

            // Begin full fade-covered transition
            isTransitioning = true;
            StartCoroutine(TransitionWithFade(fade, sceneToLoadName, spawnLocation));
        }

        private IEnumerator TransitionWithFade(ITransition transition, string sceneToLoadName, SpawnLocationID spawnLocation)
        {
            // Remember current active world scene so we can unload it after swap
            Scene sourceScene = SceneManager.GetActiveScene();

            PauseManager.SetPaused(true);

            // Store the target spawn location in SpawnManager so PlayerSpawner knows where to place the player
            SpawnLocationManager.Instance.SetNextSpawnLocation(spawnLocation);

            yield return transition.FadeInCoroutine();

            // Load the new scene additively (so old one still exists until we swap)
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
            yield return sceneLoadOperation;

            // Get the scene we just loaded
            Scene targetScene = SceneManager.GetSceneByName(sceneToLoadName);

            if (targetScene.IsValid())
            {
                SceneManager.SetActiveScene(targetScene);
            }

            // Unload the old scene (the one that was active before)
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sourceScene);

            if (unloadSceneOperation != null)
            {
                yield return unloadSceneOperation;
            }

            yield return new WaitForSecondsRealtime(TransitionConstants.HoldOnBlackSeconds);
            yield return transition.FadeOutCoroutine();

            // Transition finished allow new transitions and resume gameplay
            isTransitioning = false;
            PauseManager.SetPaused(false);
        }

        private IEnumerator TransitionWithoutFade(string sceneToLoadName, SpawnLocationID spawnLocation)
        {
            // Remember the current active world scene
            Scene sourceScene = SceneManager.GetActiveScene();

            // Set target spawn location for PlayerSpawner
            SpawnLocationManager.Instance.SetNextSpawnLocation(spawnLocation);

            // Load the new scene additively
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
            yield return sceneLoadOperation;

            Scene targetScene = SceneManager.GetSceneByName(sceneToLoadName);

            if (targetScene.IsValid())
            {
                SceneManager.SetActiveScene(targetScene);
            }

            // Unload the old active world scene
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sourceScene);

            if (unloadSceneOperation != null)
            {
                yield return unloadSceneOperation;
            }

            // Transition finished allow new transitions and resume gameplay
            isTransitioning = false;
            PauseManager.SetPaused(false);
        }
    }
}
