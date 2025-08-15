using System;
using System.Collections;
using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Transitions.Controllers;
using PokemonGame.Transitions.Extensions;
using PokemonGame.Transitions.Interfaces;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame
{
    /// <summary>
    /// Scene trigger that swaps scenes under a full-screen fade.
    /// </summary>
    public class SceneChanger : MonoBehaviour, ITrigger
    {
        private const float HoldOnBlackSeconds = 1f;

#if UNITY_EDITOR
        [Header("Scene Settings")]
        [SerializeField, Required] private SceneAsset sceneToLoad;
#endif
        private string sceneToLoadName;
        private bool isTransitioning;

#if UNITY_EDITOR
        // Syncs the name string with the scene asset in the editor
        private void OnValidate()
        {
            if (sceneToLoad != null)
            {
                sceneToLoadName = sceneToLoad.name;
            }
        }
#endif
        /// <summary>
        /// Initiates a scene change for the player, using a fade if available.
        /// </summary>
        /// <param name="character">The player triggering the scene change.</param>
        public void Trigger(Character character)
        {
            if (isTransitioning)
            {
                return;
            }

            AlphaFadeController transition = ServiceLocator.Get<AlphaFadeController>();

            if (transition == null)
            {
                StartCoroutine(LocalSwap());
                return;
            }

            isTransitioning = true;
            transition.StartCoroutine(SwapScenesCovered(transition, () => isTransitioning = false));
        }

        private IEnumerator SwapScenesCovered(ITransition transition, Action onComplete)
        {
            Scene sourceScene = gameObject.scene;

            // 1) Fade to black
            yield return transition.FadeInCoroutine();

            // 2) Load target scene additively
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);

            while (!loadSceneOperation.isDone)
            {
                yield return null;
            }

            // 3) Set new scene as active
            Scene loadedScene = SceneManager.GetSceneByName(sceneToLoadName);

            if (loadedScene.IsValid())
            {
                SceneManager.SetActiveScene(loadedScene);
            }

            // 4) Unload source scene
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sourceScene);

            while (!unloadSceneOperation.isDone)
            {
                yield return null;
            }

            // 5) Hold on black before revealing
            yield return new WaitForSecondsRealtime(HoldOnBlackSeconds);

            // 6) Fade back in
            yield return transition.FadeOutCoroutine();

            onComplete?.Invoke();
        }


        /// <summary>
        /// Fallback path if no transition exists (visible swap).
        /// </summary>
        private IEnumerator LocalSwap()
        {
            Scene sourceScene = gameObject.scene;

            // 1) Load target scene
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);

            while (!sceneLoadOperation.isDone)
            {
                yield return null;
            }

            // 2) Set loaded scene active
            Scene loadedScene = SceneManager.GetSceneByName(sceneToLoadName);

            if (loadedScene.IsValid())
            {
                SceneManager.SetActiveScene(loadedScene);
            }

            // 3) Unload source scene
            if (sourceScene.IsValid())
            {
                AsyncOperation sceneUnloadOperation = SceneManager.UnloadSceneAsync(sourceScene);
                while (!sceneUnloadOperation.isDone)
                {
                    yield return null;
                }
            }

            // 4) Mark transition complete
            isTransitioning = false;
        }
    }
}
