using System.Threading.Tasks;
using PokemonGame.MapEntry;
using PokemonGame.Pause;
using PokemonGame.Transitions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame.SceneManagement
{
    /// <summary>
    /// Manages scene-to-scene transitions.
    /// Coordinates fades, scene loading/unloading, active scene switching,
    /// and relocating the player via <see cref="MapEntryRegistry"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class SceneTransitionManager : Singleton<SceneTransitionManager>
    {
        [SerializeField] private float blackScreenHoldDuration = 1f;

        /// <summary>
        /// Whether a transition is currently running.
        /// </summary>
        public bool IsTransitioning { get; private set; }

        /// <summary>
        /// Starts a transition into the specified scenes.
        /// Applies the given transition effect and positions the player
        /// at the provided map entry point.
        /// </summary>
        /// <param name="scenesToLoad">Scenes to load additively (Player, Core, Map).</param>
        /// <param name="spawnLocationId">Map entry where the player should appear.</param>
        /// <param name="transitionType">Type of transition animation to use.</param>
        public void StartTransition(string[] scenesToLoad, MapEntryID spawnLocationId, TransitionType transitionType)
        {
            if (IsTransitioning)
            {
                return;
            }

            if (scenesToLoad == null || scenesToLoad.Length == 0)
            {
                Log.Warning(nameof(SceneTransitionManager), "No scenes provided for transition.");
                return;
            }

            IsTransitioning = true;
            Transition transition = TransitionLibrary.Instance.Resolve(transitionType);
            _ = RunTransitionMulti(scenesToLoad, spawnLocationId, transition);
        }

        /// <summary>
        /// Executes the transition sequence:
        /// pauses gameplay, runs fade-in, loads target scenes,
        /// switches the active scene, unloads the source scene,
        /// and finally runs fade-out.
        /// </summary>
        private async Task RunTransitionMulti(string[] scenesToLoad, MapEntryID spawnLocationId, Transition transition)
        {
            Scene sourceScene = SceneManager.GetActiveScene();

            PauseManager.SetPaused(true);
            MapEntryRegistry.SetNextEntry(spawnLocationId);

            if (transition != null)
            {
                await transition.FadeInAsync();
            }

            // Load all target scenes
            foreach (string sceneName in scenesToLoad)
            {
                await SceneLoader.LoadAdditiveAsync(sceneName);
            }

            // Last loaded = map; make it active
            string lastSceneName = scenesToLoad[scenesToLoad.Length - 1];
            SceneLoader.SetActive(lastSceneName);

            // Unload previous scene
            await SceneLoader.UnloadAsync(sourceScene.name);

            if (transition != null)
            {
                await Task.Delay((int)(blackScreenHoldDuration * 1000));
                await transition.FadeOutAsync();
            }

            IsTransitioning = false;
            PauseManager.SetPaused(false);
        }
    }
}
