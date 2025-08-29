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
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Scenes (Editor Only)")]

        [Tooltip("Persistent gameplay systems scene (e.g., managers, UI roots). Drag the scene asset here.")]
        [SerializeField, Required] private SceneAsset gameCoreScene;

        [Tooltip("First map scene that the player should spawn into after boot.")]
        [SerializeField, Required] private SceneAsset initialScene;

        [Tooltip("Optional transition scene (e.g., fade/black screen) shown before loading completes.")]
        [SerializeField, Required] private SceneAsset transitionScene;
#endif

        [Header("Scenes (Runtime)")]

        [Tooltip("Runtime name of the GameCore scene (auto-populated from GameCore SceneAsset).")]
        [SerializeField, ReadOnly] private string gameCoreSceneName;

        [Tooltip("Runtime name of the Initial scene (auto-populated from Initial SceneAsset).")]
        [SerializeField, ReadOnly] private string initialSceneName;

        [Tooltip("Runtime name of the Transition scene (auto-populated from Transition SceneAsset).")]
        [SerializeField, ReadOnly] private string transitionSceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            gameCoreSceneName = gameCoreScene ? gameCoreScene.name : string.Empty;
            initialSceneName = initialScene ? initialScene.name : string.Empty;
            transitionSceneName = transitionScene ? transitionScene.name : string.Empty;
        }
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
            await LoadAdditiveAsync(transitionSceneName);
            await LoadAdditiveAsync(gameCoreSceneName);
            await LoadAdditiveAsync(initialSceneName);

            // Set Initial map as active scene
            Scene map = SceneManager.GetSceneByName(initialSceneName);
            SceneManager.SetActiveScene(map);

            // Unload Boot scene
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        private static async Task LoadAdditiveAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!sceneLoadOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
