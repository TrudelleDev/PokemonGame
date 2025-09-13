using System.Threading.Tasks;
using PokemonGame.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame.SceneManagement
{
    /// <summary>
    /// Centralized utility for scene management.
    /// Wraps Unity’s SceneManager with async/await helpers 
    /// for loading, unloading, and activating scenes by name.
    /// </summary>
    public static class SceneLoader
    {
        /// <summary>
        /// Loads a scene additively if it is not already loaded.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <returns>
        /// A task that completes once the scene has finished loading.
        /// If the scene is already loaded or the name is invalid, the task finishes immediately.
        /// </returns>
        public static async Task LoadAdditiveAsync(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Log.Warning(nameof(SceneLoader), "LoadAdditiveAsync called with null or empty scene name.");
                return;
            }

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                return;
            }

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!loadSceneOperation.isDone)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Unloads a scene if it is currently loaded.
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload.</param>
        /// <returns>
        /// A task that completes once the scene has finished unloading.
        /// If the scene is not loaded or the name is invalid, the task finishes immediately.
        /// </returns>
        public static async Task UnloadAsync(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Log.Warning(nameof(SceneLoader), "UnloadAsync called with null or empty scene name.");
                return;
            }

            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                return;
            }

            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sceneName);

            while (!unloadSceneOperation.isDone)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Sets a loaded scene as the active scene, if valid.
        /// Logs a warning if the scene is not loaded.
        /// </summary>
        /// <param name="sceneName">The name of the scene to activate.</param>
        public static void SetActive(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Log.Warning(nameof(SceneLoader), "SetActive called with null or empty scene name.");
                return;
            }

            Scene scene = SceneManager.GetSceneByName(sceneName);

            if (scene.IsValid() && scene.isLoaded)
            {
                SceneManager.SetActiveScene(scene);
            }
            else
            {
                Log.Warning(nameof(SceneLoader), $"Cannot set active: {sceneName} not loaded.");
            }
        }
    }
}
