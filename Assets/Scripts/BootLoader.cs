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
        [SerializeField, Required] private SceneAsset gameCoreScene;

        [Tooltip("Initial map scene to load after the core scene.")]
        [SerializeField, Required] private SceneAsset viridianForestScene;
#endif

        [Header("Transition")]
        [Tooltip("Fade controller used for scene transitions.")]
        [SerializeField, Required] private AlphaFadeController sceneFadeController;

        private async void Start()
        {
            // Load all core definitions sequentially
            await PokemonDefinitionLoader.LoadAllAsync();
            await AbilityDefinitionLoader.LoadAllAsync();
            await NatureDefinitionLoader.LoadAllAsync();
            await MoveDefinitionLoader.LoadAllAsync();
            await ItemDefinitionLoader.LoadAllAsync();

            // Fade to black before loading scenes
            await sceneFadeController.FadeInAsync();

            // Load scenes
            await LoadAdditiveAsync(GetSceneName(gameCoreScene));
            await LoadAdditiveAsync(GetSceneName(viridianForestScene));

            // Set GameCore as active scene
            Scene core = SceneManager.GetSceneByName(GetSceneName(gameCoreScene));
            SceneManager.SetActiveScene(core);

            // Fade out to reveal gameplay
            await sceneFadeController.FadeOutAsync();

            // Unload Boot scene
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        private static async Task LoadAdditiveAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                return;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!operation.isDone)
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
