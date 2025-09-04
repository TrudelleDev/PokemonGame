using System.Collections;
using PokemonGame.MapEntry;
using PokemonGame.MapEntry.Enums;
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
    /// Controls scene-to-scene transitions.
    /// Supports optional fade in/out effects and positions the player
    /// at the correct map entry point after loading.
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
        /// Begins a transition into the target scene at the specified map entry point.
        /// Uses the given transition type (instant, fade, etc.).
        /// </summary>
        /// <param name="sceneToLoadName">The name of the target scene to load.</param>
        /// <param name="mapEntryID">The entry point where the player should appear.</param>
        /// <param name="transitionType">The type of transition effect to apply.</param>
        public void StartTransition(string sceneToLoadName, MapEntryID mapEntryID, TransitionType transitionType)
        {
            if (isTransitioning)
                return;

            ITransition transition = TransitionResolver.Resolve(transitionType);

            isTransitioning = true;
            StartCoroutine(RunTransition(sceneToLoadName, mapEntryID, transition));
        }

        private IEnumerator RunTransition(string sceneToLoadName, MapEntryID mapEntryID, ITransition transition)
        {
            Scene sourceScene = SceneManager.GetActiveScene();
            PauseManager.SetPaused(true);

            // Store entry point for player placement
            MapEntryRegistry.SetNextEntry(mapEntryID);

            // Fade in (optional)
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

            // Fade out (optional)
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
