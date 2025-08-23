using System.Threading.Tasks;
using PokemonGame.Abilities.Definition;
using PokemonGame.Items.Definition;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame
{
    /// <summary>
    /// Loads core game data, then loads GameCore and Initial map scenes additively.
    /// Sets the Initial map as the active scene and unloads Boot.
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

            // Load GameCore and Initial map scenes
            await LoadAdditiveAsync(GetSceneName(transitionScene));
            await LoadAdditiveAsync(GetSceneName(gameCoreScene));
            await LoadAdditiveAsync(GetSceneName(initialScene));

            // Set Initial map as active scene
            Scene map = SceneManager.GetSceneByName(GetSceneName(initialScene));
            SceneManager.SetActiveScene(map);

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
