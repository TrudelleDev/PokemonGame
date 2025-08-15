using System.Threading.Tasks;
using PokemonGame.Abilities.Definition;
using PokemonGame.Items.Definition;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Natures;
using PokemonGame.Transitions.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame
{
    /// <summary>
    /// Loads core game data, fades in, loads main and map scenes, then fades out.
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Scenes")]
        [Tooltip("Persistent gameplay systems scene. Will be set active after loading.")]
        [SerializeField, Required]
        private SceneAsset gameCoreScene;

        [Tooltip("Initial map scene to load after the core scene.")]
        [SerializeField, Required]
        private SceneAsset initialScene;

        [SerializeField, Required]
        private SceneAsset transitionScene;
#endif

        private async void Start()
        {          
            // Load all core definitions sequentially
            await PokemonDefinitionLoader.LoadAllAsync();
            await AbilityDefinitionLoader.LoadAllAsync();
            await NatureDefinitionLoader.LoadAllAsync();
            await MoveDefinitionLoader.LoadAllAsync();
            await ItemDefinitionLoader.LoadAllAsync();

            await LoadAdditiveAsync(GetSceneName(transitionScene));
            AlphaFadeController transition = ServiceLocator.Get<AlphaFadeController>();
            
            // Fade to black before loading scenes
            await transition.FadeInAsync();

            // Load scenes
            await LoadAdditiveAsync(GetSceneName(gameCoreScene));
            await LoadAdditiveAsync(GetSceneName(initialScene));

            // Set GameCore as active scene
            Scene core = SceneManager.GetSceneByName(GetSceneName(gameCoreScene));
            SceneManager.SetActiveScene(core);

            await Task.Delay(TimeSpan.FromSeconds(1f));

            // Fade out to reveal gameplay
            await transition.FadeOutAsync();

            // Unload Boot scene
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        private static async Task LoadAdditiveAsync(string sceneName)
        {
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

#if UNITY_EDITOR
        private static string GetSceneName(SceneAsset sceneAsset)
        {
            if (sceneAsset != null)
            {
                return sceneAsset.name;
            }

            return string.Empty;
        }
#endif
    }
}
